﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Git.hub;
using GitCommands;
using GitExtUtils;
using GitExtUtils.GitUI;
using GitUIPluginInterfaces;
using ResourceManager;
using ShiftFlow.Properties;

namespace ShiftFlow
{
    public partial class ShiftFlowForm : GitExtensionsFormBase
    {
        public string GitHubApiEndpoint => "https://api.github.com/";
        public string GitHubEndpoint => "https://github.com/";

        private Client _gitHub;
        public static string GitHubAuthorizationRelativeUrl = "authorizations";

        private Client GitHub => _gitHub ?? (_gitHub = new Client(GitHubApiEndpoint));

        private readonly TranslationString _ShiftFlowTooltip = new TranslationString("A good branch model for your project with Git...");
        private readonly TranslationString _loading = new TranslationString("Loading...");
        private readonly TranslationString _noBranchExist = new TranslationString("No {0} branches exist.");

        private const string ProductionNamingConvention = "(luke|force|projectname)_(tenant|target)[_v{version}]";
        private const string HotfixNamingConvention = "(luke|force|projectname)_(context)";
        private const string ReleaseNamingConvention = "(luke|force|projectname)_(tenant|target)_(YYYYmmdd)";
        private const string MasterNamingConvention = "(forcev2)";
        private const string StagingNamingConvention = "(luke|force|projectname)_(context)[_v{version}]";
        private const string SupportNamingConvention = "(luke|force|projectname)_(context)";

        private readonly TranslationString _ProductionBranchContext = new TranslationString("A production branch corresponds to a client.\r\nYou may need to create this branch when working on a long period for a given customer.\r\nYou can suffix the branch with a version if needed.");
        private readonly TranslationString _HotfixBranchContext = new TranslationString("A hotfix originates from a production branch. It must be a short lived branch.\r\nYou may use this branch for correcting a bug in production.\r\nYou may also want to work on senarios.");
        private readonly TranslationString _ReleaseBranchContext = new TranslationString("A release branch originates from the main branch. It must be a short lived branch.\r\nYou may use this branch when preparing for a new release.");
        private readonly TranslationString _MasterBranchContext = new TranslationString("The main branch corresponds to the framework.\r\nYou can suffix the branch with a version if needed. ex: forcev2");
        private readonly TranslationString _SupportBranchContext = new TranslationString("A support originates from the main branch. It must be a short lived branch.\r\nYou may use this branch for correcting a bug in the main branch.");
        private readonly TranslationString _StagingBranchContext = new TranslationString("A staging branch originates from develop. It must be a short lived branch.\r\nYou may use this branch when preparing for a new release of the core framework.");

        private readonly TranslationString _RoleDataScientist = new TranslationString("Use the role datascientist when you want to work on your customer code.");
        private readonly TranslationString _RoleDeveloper = new TranslationString("Use the role developer when you want to work on the core code and prepare for release to the main branch.\r\nYou can also create a branch for correcting a bug in the main branch.");

        private readonly GitUIEventArgs _gitUiCommands;

        private readonly string _MasterBranches = $"{Branch.masters:G}";
        private readonly string _DevelopBranch = $"develop";
        private static readonly string _ForceGoldenBranch = $"main";
        private static readonly string _GearsGoldenBranch = $"main_gears";
        private static readonly string _LukeGoldenBranch = $"main_luke";
        private readonly string _ShiftIntegrationBranchNamespace = "integrations";
        private readonly string _ShiftRc2MainBranchNameTemplate = "rc2main_int_";
        private readonly string _ShiftSup2DevBranchNameTemplate = "sup2dev_int_";
        private readonly string _StagingBranches = $"{Branch.releaseMain:G}";
        private readonly string _SupportBranches = $"{Branch.support:G}";
        private readonly string _ReleaseBranches = $"{Branch.releaseProd:G}";
        private readonly string _HotfixBranches = $"{Branch.hotfix:G}";
        private readonly string _ProductionBranches = $"{Branch.production:G}";

        private readonly string _MergeOngoing = "MERGE ONGOING";

        private Dictionary<string, IReadOnlyList<string>> Branches { get; } = new Dictionary<string, IReadOnlyList<string>>();

        private Dictionary<string, Repository> Repositories { get; } = new Dictionary<string, Repository>();

        private readonly AsyncLoader _task = new AsyncLoader();

        public bool IsRefreshNeeded { get; set; }

        private string CurrentBranch { get; set; }

        private List<PullRequest> _PullRequests = null;

        private Task _LoadPullRequestsTask = null;

        private enum Role
        {
            datascientist,
            developer
        }

        private enum Branch
        {
            releaseProd,
            hotfix,
            production,
            support,
            masters,
            releaseMain
        }

        private static List<string> Roles
        {
            get { return Enum.GetValues(typeof(Role)).Cast<object>().Select(e => e.ToString()).ToList(); }
        }

        private static List<string> BranchTypes
        {
            get { return Enum.GetValues(typeof(Branch)).Cast<object>().Select(e => e.ToString()).ToList(); }
        }

        private static string GoldenBranch
        {
            get
            {
                var environment = ShiftFlowPlugin.Instance.EnvironmentSetting.ValueOrDefault(ShiftFlowPlugin.Instance.Settings);
                switch (environment)
                {
                    case ShiftFlowPlugin.ForceEnvironment:
                        return _ForceGoldenBranch;
                    case ShiftFlowPlugin.GearsEnvironment:
                        return _GearsGoldenBranch;
                    case ShiftFlowPlugin.LukeEnvironment:
                        return _LukeGoldenBranch;
                    default:
                        {
                            throw new Exception("Environment not defined");
                        }
                }
            }
        }

        private List<string> GetBranchTypes()
        {
            var role = comboBox2.SelectedValue?.ToString();

            if (role == Role.developer.ToString())
            {
                return new List<string>
                {
                    // Branch.masters.ToString(),
                    Branch.support.ToString(),
                    Branch.releaseMain.ToString()
                };
            }

            return new List<string>
                {
                    Branch.hotfix.ToString(),
                    Branch.releaseProd.ToString(),
                    Branch.production.ToString()
                };
        }

        public ShiftFlowForm(GitUIEventArgs gitUiCommands)
        {
            InitializeComponent();
            InitializeComplete();

            _gitUiCommands = gitUiCommands;

            if (_gitUiCommands != null)
            {
                Init();
            }
        }

        private void Init()
        {
            gbStart.Enabled = true;
            gbManage.Enabled = true;

            cbType.DataSource = GetBranchTypes();
            var types = new List<string>();
            types.AddRange(GetBranchTypes());
            cbManageType.DataSource = types;

            cbBaseBranch.Enabled = false;

            LoadBaseBranches();

            DisplayHead();

            LoadBranches();

            LoadPullRequests();

            UpdatePullRequestsValues();

            comboBox2.DataSource = Roles;
            comboBox2.SelectedItem = Roles.First();
        }

        private async Task LoadPullRequestsTask()
        {
            await Task.Yield();
            List<PullRequest> GetPullRequests()
            {
                if (!Repositories.Any())
                {
                    try
                    {
                        GetRepositories();
                    }
                    catch
                    {
                        OAuthToken = null;
                        AskForCredentials();
                        GetRepositories();
                    }
                }

                var repository = RetrieveRepository();

                if (repository == null)
                {
                    return null;
                }

                var prs = repository.GetPullRequests();
                return prs.ToList();
            }

            var prs = GetPullRequests();
            if (prs == null)
            {
                return;
            }

            _PullRequests = prs;
            UpdatePullRequestsValues();
            _LoadPullRequestsTask = null;
        }

        private void LoadPullRequests()
        {
            _LoadPullRequestsTask = Task.Run(() => LoadPullRequestsTask());
        }

        private static bool TryExtractBranchFromHead(string currentRef, out string branchType, out string branchName)
        {
            foreach (Branch branch in Enum.GetValues(typeof(Branch)))
            {
                var startRef = branch + "/";
                if (currentRef.StartsWith(startRef))
                {
                    branchType = branch.ToString();
                    branchName = currentRef.Substring(startRef.Length);
                    return true;
                }
            }

            branchType = null;
            branchName = null;
            return false;
        }

        #region Loading Branches
        private void LoadBranches()
        {
            try
            {
                LoadBranchesCore();
            }
            catch
            {
                // TODO
            }
        }

        private void LoadBranchesCore()
        {
            cbManageType.Enabled = false;
            cbBranches.DataSource = new List<string> { _loading.Text };
            comboBox1.DataSource = new List<string> { _loading.Text };
            comboBox3.DataSource = new List<string> { _loading.Text };

            if (!Branches.Any())
            {
                _task.LoadAsync(() => GetRemoteBranches(_ProductionBranches).Union(GetRemoteBranches(_MasterBranches)).Union(GetBranches(_SupportBranches)).Union(GetBranches(_HotfixBranches)).Union(GetBranches(_ReleaseBranches)).Union(GetBranches(_StagingBranches)), branches =>
                {
                    Branches.Add(_ProductionBranches, branches.Where(b => b.StartsWith(_ProductionBranches)).ToList());
                    Branches.Add(_ReleaseBranches, branches.Where(b => b.StartsWith(_ReleaseBranches)).ToList());
                    Branches.Add(_HotfixBranches, branches.Where(b => b.StartsWith(_HotfixBranches)).ToList());
                    Branches.Add(_StagingBranches, branches.Where(b => b.StartsWith(_StagingBranches)).ToList());
                    Branches.Add(_SupportBranches, branches.Where(b => b.StartsWith(_SupportBranches)).ToList());
                    Branches.Add(_MasterBranches, branches.Where(b => b.StartsWith(_MasterBranches)).ToList());
                    DisplayBranchData();
                });
            }
            else
            {
                DisplayBranchData();
            }

            if (!Repositories.Any())
            {
                try
                {
                    GetRepositories();
                }
                catch
                {
                    OAuthToken = null;
                    AskForCredentials();
                    GetRepositories();
                }
            }
        }

        private void GetRepositories()
        {
            if (string.IsNullOrEmpty(OAuthToken))
            {
                AskForCredentials();
            }

            GitHub.setOAuth2Token(OAuthToken);

            foreach (var repository in GitHub.getRepositories())
            {
                if (!string.IsNullOrEmpty(repository.SshUrl))
                {
                    Repositories[repository.SshUrl] = repository;
                }

                if (!string.IsNullOrEmpty(repository.CloneUrl))
                {
                    Repositories[repository.CloneUrl] = repository;
                }

                if (!string.IsNullOrEmpty(repository.GitUrl))
                {
                    Repositories[repository.GitUrl] = repository;
                }
            }
        }

        private IReadOnlyList<string> GetBranches(string typeBranch)
        {
            var args = new GitArgumentBuilder("branch")
            {
                "--list",
                $"{typeBranch}/*"
            };
            var result = _gitUiCommands.GitModule.GitExecutable.Execute(args);

            if (result.ExitCode != 0 || result.StandardOutput == null)
            {
                return Array.Empty<string>();
            }

            return result.StandardOutput
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim('*', ' ', '\n', '\r'))
                .ToList();
        }

        private void DisplayBranchData()
        {
            var branchType = cbManageType.SelectedValue.ToString();

            var branchTypes = GetBranchTypes();

            if (!branchTypes.Contains(branchType))
            {
                cbManageType.DataSource = branchTypes;
                branchType = branchTypes.First();
                cbManageType.SelectedItem = branchType;
            }

            var branches = Branches.ContainsKey(branchType) ? Branches[branchType] : new List<string> { };
            var isThereABranch = Branches.ContainsKey(branchType) && branches.Any();

            var role = comboBox2.SelectedValue.ToString();

            cbManageType.Enabled = true;
            cbBranches.DataSource = isThereABranch ? branches : new[] { string.Format(_noBranchExist.Text, branchType) };
            comboBox1.Enabled = true;
            comboBox3.Enabled = true;

            if (role == $"{Role.datascientist:G}")
            {
                comboBox1.DataSource = Branches.ContainsKey(_ProductionBranches) ? Branches[_ProductionBranches] : new[] { string.Format(_noBranchExist.Text, _ProductionBranches) };

                // comboBox3.DataSource = Branches.ContainsKey(_MasterBranches) ? Branches[_MasterBranches] : new[] { string.Format(_noBranchExist.Text, _MasterBranches) };
                comboBox3.DataSource = new[] { GoldenBranch };
                comboBox3.SelectedItem = GoldenBranch;
                comboBox3.Enabled = false;
            }
            else
            {
                // comboBox1.DataSource = Branches.ContainsKey(_MasterBranches) ? Branches[_MasterBranches] : new[] { string.Format(_noBranchExist.Text, _MasterBranches) };
                comboBox1.DataSource = new[] { GoldenBranch };
                comboBox1.SelectedItem = GoldenBranch;
                comboBox1.Enabled = false;
            }

            cbBranches.Enabled = isThereABranch;
        }

        private void LoadBaseBranches()
        {
            var branchType = cbType.SelectedValue?.ToString() ?? string.Empty;

            var branchTypes = GetBranchTypes();

            if (!branchTypes.Contains(branchType))
            {
                cbType.DataSource = branchTypes;
                branchType = branchTypes.First();
                cbType.SelectedItem = branchType;
            }

            var manageBaseBranch = NeedsBaseBranch(branchType);
            pnlBasedOn.Visible = manageBaseBranch;

            var role = comboBox2.SelectedValue?.ToString();

            cbBaseBranch.Enabled = manageBaseBranch;

            if (manageBaseBranch)
            {
                if (branchType == $"{Branch.hotfix}")
                {
                    cbBaseBranch.DataSource = GetPossibleBaseBranches(branchType);
                }
                else
                {
                    cbBaseBranch.DataSource = new[] { GoldenBranch };
                    cbBaseBranch.SelectedItem = GoldenBranch;
                    cbBaseBranch.Enabled = false;
                }
            }
        }

        private bool NeedsBaseBranch(string branchType)
        {
            if (branchType == Branch.hotfix.ToString("G"))
            {
                return true;
            }

            return false;
        }

        private List<string> GetRemoteBranches(string branchType)
        {
            var pattern = $"origin/{branchType}/*";
            var args = new GitArgumentBuilder("branch")
                    {
                        "-r",
                        "--list",
                        pattern
                    };
            var output = _gitUiCommands.GitModule.GitExecutable.GetOutput(args);

            return output
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim('*', ' ', '\n', '\r'))
                .Select(b => b.Remove(0, "origin/".Length))
                .ToList();
        }

        private List<string> GetPossibleBaseBranches(string branchType)
        {
            var pattern = $"origin/**/*";
            var args = new GitArgumentBuilder("branch")
                    {
                        "-r",
                        "--list",
                        pattern
                    };
            var output = _gitUiCommands.GitModule.GitExecutable.GetOutput(args);

            var filterName = GetPossibleBaseBranchFilter(branchType);

            return output
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim('*', ' ', '\n', '\r'))
                .Where(b => b.Contains(filterName))
                .Select(b => b.Remove(0, "origin/".Length))
                .ToList();
        }

        private string GetPossibleBaseBranchFilter(string branchType)
        {
            if (branchType == Branch.hotfix.ToString("G"))
            {
                return $"/{Branch.production:G}/";
            }

            return $"/{GoldenBranch}";
        }

        #endregion

        #region Run ShiftFlow commands
        private string CreateIntegrationBranch(string targetBranch, string templateName, int complementPrNumber, string branchName)
        {
            if (!FetchBranch(targetBranch))
            {
                throw new Exception($"Failed to fetch branch {targetBranch}");
            }

            var integrationBranchName = $"{_ShiftIntegrationBranchNamespace}/{templateName}{complementPrNumber}";

            var args = new GitArgumentBuilder("checkout")
                    {
                        "-b",
                        integrationBranchName,
                        $"origin/{targetBranch}"
                    };

            if (!RunCommand(args))
            {
                throw new Exception($"Failed to create integration branch {integrationBranchName}");
            }

            if (!PushBranch(integrationBranchName))
            {
                throw new Exception($"Failed to push branch {integrationBranchName}");
            }

            txtBranchName.Text = string.Empty;
            LoadBranches();

            var args2 = new GitArgumentBuilder("merge")
                    {
                        branchName
                    };

            if (RunCommand(args2))
            {
                if (!PushCurrentBranch())
                {
                    throw new Exception($"Failed to push branch {integrationBranchName}");
                }

                return integrationBranchName;
            }

            throw new Exception(_MergeOngoing);
        }

        private bool CreateBranch(string branchType, string branchName)
        {
            var baseBranch = GetBaseBranch();

            if (!FetchBranch(baseBranch))
            {
                return false;
            }

            var args = new GitArgumentBuilder("checkout")
                    {
                        "-b",
                        branchName,
                        $"origin/{baseBranch}"
                    };

            if (args == null)
            {
                return false;
            }

            if (RunCommand(args))
            {
                txtBranchName.Text = string.Empty;
                LoadBranches();

                return true;
            }

            return false;
        }

        private bool TagBranchCreation(string branchType, string branchName, int increment)
        {
            // We tag the creation of releaseMain branch
            if (branchType != _StagingBranches)
            {
                return false;
            }

            var shortName = branchName.Remove(0, $"{_StagingBranches}/".Length);
            var args = new GitArgumentBuilder("tag")
                    {
                        "-a",
                        $"i#{increment}_{shortName}_creation",
                        "-m",
                        $"\"Creation of release {shortName}\""
                    };

            if (args == null)
            {
                return false;
            }

            return RunCommand(args);
        }

        private bool PushTags()
        {
            var args = new GitArgumentBuilder("push")
                    {
                        "origin",
                        "--tags"
                    };

            if (args == null)
            {
                return false;
            }

            return RunCommand(args);
        }

        private bool Stage()
        {
            var args = new GitArgumentBuilder("add")
                    {
                        "."
                    };

            if (args == null)
            {
                return false;
            }

            return RunCommand(args);
        }

        private bool Stash()
        {
            var args = new GitArgumentBuilder("stash") { };

            if (args == null)
            {
                return false;
            }

            return RunCommand(args);
        }

        private bool Unstash()
        {
            var args = new GitArgumentBuilder("stash")
            {
                "pop"
            };

            if (args == null)
            {
                return false;
            }

            return RunCommand(args);
        }

        private bool PushBranch(string branchName, bool setUpstream = true)
        {
            GitArgumentBuilder args = new GitArgumentBuilder("push")
                    {
                        $"origin",
                        branchName,
                        "-u"
                    };

            if (args == null)
            {
                return false;
            }

            return RunCommand(args);
        }

        private bool PushCurrentBranch()
        {
            GitArgumentBuilder args = new GitArgumentBuilder("push");

            if (args == null)
            {
                return false;
            }

            return RunCommand(args);
        }

        private bool FetchBranch(string branchName)
        {
            var args = new GitArgumentBuilder("fetch")
                    {
                        "origin",
                        branchName
                    };

            if (args == null)
            {
                return false;
            }

            return RunCommand(args);
        }

        private void btnStartBranch_Click(object sender, EventArgs e)
        {
            var branchType = cbType.SelectedValue.ToString();
            var branchName = $"{branchType}/{txtBranchName.Text}";

            if (!CreateBranch(branchType, branchName))
            {
                return;
            }

            if (!PushBranch(branchName))
            {
                return;
            }

            var increment = RetrieveIncrement();

            if (!TagBranchCreation(branchType, branchName, increment))
            {
                return;
            }

            PushTags();
        }

        private int RetrieveIncrement()
        {
            var args = new GitArgumentBuilder("tag")
                    {
                        "--list",
                        $"i#*"
                    };

            if (args == null)
            {
                return -1;
            }

            RunCommand(args);

            var tags = txtResult.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(t => t.StartsWith("i#")).ToArray();

            if (tags.Any())
            {
                var max = tags.Select(t => ExtractIncrement(t)).Max();
                return max + 1;
            }
            else
            {
                return 1;
            }
        }

        private int ExtractIncrement(string tag)
        {
            if (!tag.StartsWith("i#"))
            {
                return 1;
            }

            var index = tag.IndexOf('_');

            var increment = tag.Substring(2, index - 2);

            return int.Parse(increment);
        }

        private string GetBaseBranch()
        {
            var branchType = cbType.SelectedValue.ToString();

            if (!Enum.TryParse<Branch>(branchType, out var enumType))
            {
                return _DevelopBranch;
            }

            switch (enumType)
            {
                case Branch.hotfix:
                    return $"{cbBaseBranch.SelectedItem}";
                case Branch.releaseProd:
                case Branch.support:
                case Branch.production:
                    return GoldenBranch;
                case Branch.releaseMain:
                default:
                    return _DevelopBranch;
            }
        }

        private Repository RetrieveRepository()
        {
            var repository = _gitUiCommands.GitModule.LocalConfigFile.GetValue("remote \"origin\".url");

            if (Repositories == null)
            {
                MessageBox.Show(this, $"The repositories were not correctly registered", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (Repositories.TryGetValue(repository, out var res))
            {
                return res;
            }

            MessageBox.Show(this, $"The repository {repository} was not correctly registered", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        private void resolveConflictsToDevelop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pullrequestToDevelop.Text))
            {
                MessageBox.Show(this, $"The pull request to develop has not been created", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(pullrequestToMain.Text))
            {
                MessageBox.Show(this, $"The pull request to main has not been created", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var toDevelopNumber = int.Parse(pullrequestToDevelop.Text.Remove(0, 4));
            var toMainNumber = int.Parse(pullrequestToMain.Text.Remove(0, 4));

            try
            {
                var repository = RetrieveRepository();

                if (repository == null)
                {
                    return;
                }

                PullRequest toDevelop = repository.GetPullRequest(toDevelopNumber);

                toDevelop.Close();

                var integrationBranchName = CreateIntegrationBranch(_DevelopBranch, _ShiftSup2DevBranchNameTemplate, toMainNumber, toDevelop.Head.Ref);

                repository.CreatePullRequest(integrationBranchName, _DevelopBranch, toDevelop.Title, toDevelop.Body);
            }
            catch (Exception ex) when (ex.Message == _MergeOngoing)
            {
                MessageBox.Show(this, $"You have to correct the merge conflicts before pushing the integration branch", "Merge ongoing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Error: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdatePullRequestsValues();
            }
        }

        private void resolveConflictToMain_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pullrequestToDevelop.Text))
            {
                MessageBox.Show(this, $"The pull request to develop has not been created", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(pullrequestToMain.Text))
            {
                MessageBox.Show(this, $"The pull request to main has not been created", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var toDevelopNumber = int.Parse(pullrequestToDevelop.Text.Remove(0, 4));
            var toMainNumber = int.Parse(pullrequestToMain.Text.Remove(0, 4));

            try
            {
                var repository = RetrieveRepository();

                if (repository == null)
                {
                    return;
                }

                PullRequest toMain = repository.GetPullRequest(toMainNumber);

                toMain.Close();

                var integrationBranchName = CreateIntegrationBranch(GoldenBranch, _ShiftRc2MainBranchNameTemplate, toDevelopNumber, toMain.Head.Ref);

                repository.CreatePullRequest(integrationBranchName, GoldenBranch, toMain.Title, toMain.Body);
            }
            catch (Exception ex) when (ex.Message == _MergeOngoing)
            {
                MessageBox.Show(this, $"You have to correct the merge conflicts before pushing the integration branch", "Merge ongoing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Error: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdatePullRequestsValues();
            }
        }

        private bool RunCommand(ArgumentString commandText)
        {
            pbResultCommand.Image = DpiUtil.Scale(Resource.StatusHourglass);
            ShowToolTip(pbResultCommand, "running command : git " + commandText);
            ForceRefresh(pbResultCommand);
            lblRunCommand.Text = "git " + commandText;
            ForceRefresh(lblRunCommand);
            txtResult.Text = "running...";
            ForceRefresh(txtResult);

            var result = _gitUiCommands.GitModule.GitExecutable.Execute(commandText);

            IsRefreshNeeded = true;

            ttDebug.RemoveAll();
            ttDebug.SetToolTip(lblDebug, "cmd: git " + commandText + "\n" + "exit code:" + result.ExitCode);

            var resultText = Regex.Replace(result.AllOutput, @"\r\n?|\n", Environment.NewLine);

            if (result.ExitCode == 0)
            {
                pbResultCommand.Image = DpiUtil.Scale(Resource.success);
                ShowToolTip(pbResultCommand, resultText);
                DisplayHead();
            }
            else
            {
                pbResultCommand.Image = DpiUtil.Scale(Resource.error);
                ShowToolTip(pbResultCommand, "error: " + resultText);
            }

            txtResult.Text = resultText;

            return result.ExitCode == 0;
        }

        #endregion

        #region GUI interactions

        private static void ForceRefresh(Control c)
        {
            c.Invalidate();
            c.Update();
            c.Refresh();
        }

        private void ShowToolTip(Control c, string msg)
        {
            ttCommandResult.RemoveAll();
            ttCommandResult.SetToolTip(c, msg);
        }

        private void RoleSelectedValueChanged(object sender, EventArgs e)
        {
            var role = comboBox2.SelectedValue.ToString();
            label21.Text = role == Role.developer.ToString() ? _RoleDeveloper.Text : _RoleDataScientist.Text;
            LoadBaseBranches();
            ModifyNamingConventionTextBox();
            LoadBranches();
            UpdateTargetBranches();
        }

        private void cbType_SelectedValueChanged(object sender, EventArgs e)
        {
            lblPrefixName.Text = cbType.SelectedValue + "/";
            LoadBaseBranches();
            ModifyNamingConventionTextBox();
        }

        private void ModifyNamingConventionTextBox()
        {
            var branchType = cbType.SelectedValue?.ToString();

            label9.Text = "";

            if (branchType == _ProductionBranches)
            {
                label9.Text = ProductionNamingConvention;
                label8.Text = _ProductionBranchContext.Text;
            }

            if (branchType == _HotfixBranches)
            {
                label9.Text = HotfixNamingConvention;
                label8.Text = _HotfixBranchContext.Text;
            }

            if (branchType == _ReleaseBranches)
            {
                label9.Text = ReleaseNamingConvention;
                label8.Text = _ReleaseBranchContext.Text;
            }

            if (branchType == _MasterBranches)
            {
                label9.Text = MasterNamingConvention;
                label8.Text = _MasterBranchContext.Text;
            }

            if (branchType == _StagingBranches)
            {
                label9.Text = StagingNamingConvention;
                label8.Text = _StagingBranchContext.Text;
            }

            if (branchType == _SupportBranches)
            {
                label9.Text = SupportNamingConvention;
                label8.Text = _SupportBranchContext.Text;
            }
        }

        private void cbManageType_SelectedValueChanged(object sender, EventArgs e)
        {
            var branchType = cbManageType.SelectedValue.ToString();
            pnlManageBranch.Enabled = false;
            pnlManageBranch.Visible = false;
            if (!string.IsNullOrWhiteSpace(branchType))
            {
                var mayHavePrs = branchType != _ProductionBranches && branchType != _MasterBranches;
                pnlManageBranch.Enabled = mayHavePrs;
                pnlManageBranch.Visible = mayHavePrs;
                LoadBranches();
                panel4.Visible = mayHavePrs;

                UpdatePullRequestsValues();
            }
            else
            {
                pnlManageBranch.Enabled = false;
                pnlManageBranch.Visible = false;
                panel4.Visible = false;
            }

            UpdateTargetBranches();
        }

        public static string OAuthToken
        {
            get => ShiftFlowPlugin.Instance.OAuthToken.ValueOrDefault(ShiftFlowPlugin.Instance.Settings);
            set
            {
                ShiftFlowPlugin.Instance.OAuthToken[ShiftFlowPlugin.Instance.Settings] = value;
            }
        }

        private void AskForCredentials()
        {
            if (string.IsNullOrEmpty(OAuthToken))
            {
                var authorizationApiUrl = new Uri(new Uri(GitHubApiEndpoint), GitHubAuthorizationRelativeUrl).ToString();
                using (var gitHubCredentialsPrompt = new GitHubCredentialsPrompt(authorizationApiUrl))
                {
                    gitHubCredentialsPrompt.ShowDialog(this);
                }
            }
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DisplayHead()
        {
            var args = new GitArgumentBuilder("symbolic-ref") { "HEAD" };
            var head = _gitUiCommands.GitModule.GitExecutable.GetOutput(args).Trim('*', ' ', '\n', '\r');

            var currentRef = head.RemovePrefix(GitRefName.RefsHeadsPrefix);

            if (TryExtractBranchFromHead(currentRef, out var branchTypes, out var branchName))
            {
                cbManageType.SelectedItem = branchTypes;
                CurrentBranch = branchName;
            }
        }

        private string _currentBranch = string.Empty;
        private string _currentBranchType = string.Empty;

        private bool IsMergeable(PullRequest pullRequest)
        {
            var status = pullRequest.Mergeable_state;

            if (status == "dirty" || status == "blocked")
            {
                return false;
            }

            return pullRequest.Mergeable == true;
        }

        private void UpdatePullRequestsValues()
        {
            var role = comboBox2.SelectedItem?.ToString();
            var hasProduction = role == $"{Role.datascientist:G}";

            panel2.Enabled = false;
            panel2.Visible = false;

            if (role == $"{Role.datascientist:G}")
            {
                panel2.Enabled = true;
                panel2.Visible = true;
            }

            var branchType = cbManageType.SelectedValue?.ToString();

            if (branchType == null)
            {
                return;
            }

            var mayHavePrs = branchType != _ProductionBranches && branchType != _MasterBranches;

            if (!mayHavePrs)
            {
                return;
            }

            try
            {
                initializePrButton.Enabled = true;
                comboBox1.Enabled = true;
                label12.Text = "         ";
                label13.Text = "         ";
                label19.Text = "         ";
                pullrequestToDevelop.Text = string.Empty;
                pullrequestToMain.Text = string.Empty;
                pullrequestToProduction.Text = string.Empty;
                linkLabel1.Visible = false;
                linkLabel2.Visible = false;
                linkLabel3.Visible = false;
                label12.BackColor = System.Drawing.Color.Green;
                label13.BackColor = System.Drawing.Color.Green;
                label19.BackColor = System.Drawing.Color.Green;

                var branchName = cbBranches.SelectedItem?.ToString();

                if (branchName == _loading.Text || string.IsNullOrEmpty(branchName))
                {
                    initializePrButton.Enabled = false;
                    return;
                }

                if (_currentBranch == branchName && _currentBranchType == branchType)
                {
                    return;
                }

                var productionBranch = comboBox1.SelectedItem?.ToString();
                var mainBranch = comboBox3.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(productionBranch) || productionBranch == _loading.Text)
                {
                    return;
                }

                if (role == $"{Role.datascientist:G}" && (string.IsNullOrEmpty(mainBranch) || mainBranch == _loading.Text))
                {
                    return;
                }

                var repository = RetrieveRepository();

                if (_PullRequests == null)
                {
                    return;
                }

                var branchPullrequests = _PullRequests.Where(p => p.Head.Ref == branchName).ToArray();
                var integrationBranchPullrequests = _PullRequests.Where(p => p.Head.Ref.StartsWith($"{_ShiftIntegrationBranchNamespace}/", StringComparison.InvariantCultureIgnoreCase)).ToArray();

                int? idToDevelop = null;
                int? idToMain = null;

                foreach (var generalBranchPullrequest in branchPullrequests)
                {
                    var branchPullrequest = repository.GetPullRequest(generalBranchPullrequest.Number);
                    var isDevelop = branchPullrequest.Base.Ref == _DevelopBranch;

                    // var isMaster = branchPullrequest.Base.Ref.StartsWith($"{Branch.masters:G}/");
                    var isMaster = branchPullrequest.Base.Ref == GoldenBranch;
                    initializePrButton.Enabled = false;
                    var number = $"PR #{branchPullrequest.Number}";
                    var link = $"{branchPullrequest.Url}".Replace("https://api.github.com/repos/", "https://github.com/").Replace("pulls", "pull");
                    var mergeable = IsMergeable(branchPullrequest);

                    if (isDevelop)
                    {
                        pullrequestToDevelop.Text = number;
                        idToDevelop = branchPullrequest.Number;
                        linkLabel1.Text = link;
                        linkLabel1.Visible = true;
                        label12.BackColor = mergeable ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                        label12.Text = branchPullrequest.Mergeable_state;
                    }
                    else if (isMaster)
                    {
                        pullrequestToMain.Text = number;
                        idToMain = branchPullrequest.Number;
                        linkLabel2.Text = link;
                        linkLabel2.Visible = true;
                        comboBox1.SelectedItem = branchPullrequest.Base.Ref;
                        comboBox1.Enabled = false;
                        label13.BackColor = mergeable ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                        label13.Text = branchPullrequest.Mergeable_state;
                    }
                    else if (hasProduction && !isMaster)
                    {
                        pullrequestToProduction.Text = number;
                        linkLabel3.Text = link;
                        linkLabel3.Visible = true;
                        comboBox1.SelectedItem = branchPullrequest.Base.Ref;
                        comboBox1.Enabled = false;
                        label19.BackColor = mergeable ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                        label19.Text = branchPullrequest.Mergeable_state;
                    }
                }

                foreach (var integrationBranchPullrequest in integrationBranchPullrequests)
                {
                    var branchPullrequest = repository.GetPullRequest(integrationBranchPullrequest.Number);
                    var isDevelop = idToMain.HasValue && branchPullrequest.Head.Ref == $"{_ShiftIntegrationBranchNamespace}/{_ShiftSup2DevBranchNameTemplate}{idToMain.Value}" && branchPullrequest.Base.Ref == _DevelopBranch;
                    var isMaster = idToDevelop.HasValue && branchPullrequest.Head.Ref == $"{_ShiftIntegrationBranchNamespace}/{_ShiftRc2MainBranchNameTemplate}{idToDevelop.Value}" && branchPullrequest.Base.Ref == GoldenBranch;

                    if (!isDevelop && !isMaster)
                    {
                        continue;
                    }

                    initializePrButton.Enabled = false;
                    var number = $"PR #{branchPullrequest.Number}";
                    var link = $"{branchPullrequest.Url}".Replace("https://api.github.com/repos/", "https://github.com/").Replace("pulls", "pull");
                    var mergeable = IsMergeable(branchPullrequest);

                    if (isDevelop)
                    {
                        pullrequestToDevelop.Text = number;
                        idToDevelop = branchPullrequest.Number;
                        linkLabel1.Text = link;
                        linkLabel1.Visible = true;
                        label12.BackColor = mergeable ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                        label12.Text = branchPullrequest.Mergeable_state;
                    }
                    else if (isMaster)
                    {
                        pullrequestToMain.Text = number;
                        idToMain = branchPullrequest.Number;
                        linkLabel2.Text = link;
                        linkLabel2.Visible = true;
                        comboBox1.SelectedItem = branchPullrequest.Base.Ref;
                        comboBox1.Enabled = false;
                        label13.BackColor = mergeable ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                        label13.Text = branchPullrequest.Mergeable_state;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Error: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreatePullRequestsDevContext(Repository repository, string branchName, string productionBranch, string body)
        {
            var toMain = repository.CreatePullRequest(branchName, productionBranch, $"PR to {productionBranch} for {branchName}", body);
            _PullRequests.Add(toMain);
            var link = $"{toMain.Url}".Replace("https://api.github.com/repos/", "https://github.com/").Replace("pulls", "pull");

            var toDevelop = repository.CreatePullRequest(branchName, "develop", $"PR to develop for {branchName}", $"{body}\r\nMerge to main {link}");
            _PullRequests.Add(toDevelop);
        }

        private void initializePrButton_Click(object sender, EventArgs e)
        {
            try
            {
                var role = comboBox2.SelectedItem?.ToString();

                var branchType = cbManageType.SelectedValue.ToString();
                var branchName = cbBranches.SelectedItem.ToString();

                if (branchName == _loading.Text || string.IsNullOrEmpty(branchName))
                {
                    return;
                }

                if (_currentBranch == branchName && _currentBranchType == branchType)
                {
                    return;
                }

                var repository = RetrieveRepository();

                if (repository == null)
                {
                    return;
                }

                var body = GetPullRequestBody($"Pull request for {branchName}");

                var productionBranch = comboBox1.SelectedItem.ToString();
                var mainBranch = comboBox3.SelectedItem.ToString();

                if (role == $"{Role.developer:G}")
                {
                    CreatePullRequestsDevContext(repository, branchName, productionBranch, body);
                }
                else
                {
                    var toMaster = repository.CreatePullRequest(branchName, mainBranch, $"PR to {mainBranch} for {branchName}", body);
                    _PullRequests.Add(toMaster);
                    var linkMain = $"{toMaster.Url}".Replace("https://api.github.com/repos/", "https://github.com/").Replace("pulls", "pull");
                    var toProduction = repository.CreatePullRequest(branchName, productionBranch, $"PR to {productionBranch} for {branchName}", $"{body}\r\nMerge to main {linkMain}");
                    _PullRequests.Add(toProduction);
                    var linkProd = $"{toProduction.Url}".Replace("https://api.github.com/repos/", "https://github.com/").Replace("pulls", "pull");
                    var toDevelop = repository.CreatePullRequest(branchName, "develop", $"PR to develop for {branchName}", $"{body}\r\nMerge to main {linkMain}\r\nMerge to main {linkProd}");
                    _PullRequests.Add(toDevelop);
                }

                UpdatePullRequestsValues();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Error: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetPullRequestBody(string content)
        {
            using (var pullRequestBodyPrompt = new PullRequestBodyPrompt(content))
            {
                pullRequestBodyPrompt.ShowDialog(this);
                return PullRequestContent.Body;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", linkLabel2.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", linkLabel3.Text);
        }
    }

    internal static class GitHubLoginInfo
    {
        public static string OAuthToken
        {
            get => ShiftFlowPlugin.Instance.OAuthToken.ValueOrDefault(ShiftFlowPlugin.Instance.Settings);
            set
            {
                ShiftFlowPlugin.Instance.OAuthToken[ShiftFlowPlugin.Instance.Settings] = value;
            }
        }
    }
}
