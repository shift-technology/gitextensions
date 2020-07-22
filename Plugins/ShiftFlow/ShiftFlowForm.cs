using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Git.hub;
using GitCommands;
using GitCommands.Utils;
using GitExtUtils;
using GitExtUtils.GitUI;
using GitHub3;
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

        private readonly GitUIEventArgs _gitUiCommands;

        private Dictionary<string, IReadOnlyList<string>> Branches { get; } = new Dictionary<string, IReadOnlyList<string>>();

        private Dictionary<string, Repository> Repositories { get; } = new Dictionary<string, Repository>();

        private readonly AsyncLoader _task = new AsyncLoader();

        public bool IsRefreshNeeded { get; set; }

        private string CurrentBranch { get; set; }

        private enum Branch
        {
            release,
            hotfix,
            production
        }

        private static List<string> BranchTypes
        {
            get { return Enum.GetValues(typeof(Branch)).Cast<object>().Select(e => e.ToString()).ToList(); }
        }

        public ShiftFlowForm(GitUIEventArgs gitUiCommands)
        {
            InitializeComponent();
            InitializeComplete();

            _gitUiCommands = gitUiCommands;

            lblPrefixManage.Text = string.Empty;
            ttShiftFlow.SetToolTip(lnkShiftFlow, _ShiftFlowTooltip.Text);

            if (_gitUiCommands != null)
            {
                Init();
            }
        }

        private void Init()
        {
            gbStart.Enabled = true;
            gbManage.Enabled = true;
            lblCaptionHead.Visible = true;
            lblHead.Visible = true;

            cbType.DataSource = BranchTypes;
            var types = new List<string>();
            types.AddRange(BranchTypes);
            cbManageType.DataSource = types;

            cbBaseBranch.Enabled = false;

            LoadBaseBranches();

            DisplayHead();
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
        private void LoadBranches(string branchType)
        {
            cbManageType.Enabled = false;
            cbBranches.DataSource = new List<string> { _loading.Text };
            comboBox1.DataSource = new List<string> { _loading.Text };

            if (!Branches.Any())
            {
                _task.LoadAsync(() => GetRemoteProductionBranches().Union(GetBranches("hotfix")).Union(GetBranches("release")), branches =>
                {
                    Branches.Add("production", branches.Where(b => b.StartsWith("production")).ToList());
                    Branches.Add("release", branches.Where(b => b.StartsWith("release")).ToList());
                    Branches.Add("hotfix", branches.Where(b => b.StartsWith("hotfix")).ToList());
                    DisplayBranchData();
                });
            }
            else
            {
                DisplayBranchData();
            }

            if (!Repositories.Any())
            {
                if (string.IsNullOrEmpty(OAuthToken))
                {
                    AskForCredentials();
                }

                GitHub.setOAuth2Token(OAuthToken);

                foreach (var repository in GitHub.getRepositories())
                {
                    Repositories[repository.Name] = repository;
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
            var branches = Branches[branchType];
            var isThereABranch = branches.Any();

            cbManageType.Enabled = true;
            cbBranches.DataSource = isThereABranch ? branches : new[] { string.Format(_noBranchExist.Text, branchType) };
            if (Branches.ContainsKey("production"))
            {
                comboBox1.DataSource = Branches["production"];
            }
            else
            {
                comboBox1.DataSource = new[] { string.Format(_noBranchExist.Text, "production") };
            }

            comboBox1.Enabled = true;
            cbBranches.Enabled = isThereABranch;
            if (isThereABranch && CurrentBranch != null)
            {
                cbBranches.SelectedItem = CurrentBranch;
                CurrentBranch = null;
            }

            btnFinish.Enabled = isThereABranch;
            button2.Enabled = isThereABranch;
        }

        private void LoadBaseBranches()
        {
            var branchType = cbType.SelectedValue.ToString();
            var manageBaseBranch = branchType == Branch.hotfix.ToString("G");
            pnlBasedOn.Visible = manageBaseBranch;
            cbBaseBranch.Enabled = manageBaseBranch;

            if (manageBaseBranch)
            {
                cbBaseBranch.DataSource = GetRemoteProductionBranches();
            }
        }

        private List<string> GetRemoteProductionBranches()
        {
            var pattern = $"origin/{Branch.production:G}/*";
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

        #endregion

        #region Run ShiftFlow commands
        private bool CreateBranch(string branchType, string branchName)
        {
            var baseBranch = GetBaseBranch();
            var args = new GitArgumentBuilder("checkout")
                    {
                        "-b",
                        branchName,
                        baseBranch
                    };

            if (args == null)
            {
                return false;
            }

            if (RunCommand(args))
            {
                txtBranchName.Text = string.Empty;
                LoadBranches(branchType);

                return true;
            }

            return false;
        }

        private bool TagBranchCreation(string branchType, string branchName, int increment)
        {
            if (branchType != "release")
            {
                return false;
            }

            var shortName = branchName.Remove(0, "release/".Length);
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

        private bool PushBranch(string branchName)
        {
            var args = new GitArgumentBuilder("push")
                    {
                        "-u",
                        $"origin",
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

            if (branchType == Branch.hotfix.ToString("G"))
            {
                return $"{cbBaseBranch.SelectedItem}";
            }

            return "develop";
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show(this, $"The pull request has not been created", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var toMasterNumber = int.Parse(textBox2.Text.Remove(0, 4));

            var branchName = cbBranches.SelectedItem.ToString();

            try
            {
                var currentRepository = Path.GetFileName(_gitUiCommands.GitModule.WorkingDir.Trim('\\'));
                var repository = Repositories[currentRepository];

                PullRequest toMaster = repository.GetPullRequest(toMasterNumber);

                var success2 = toMaster.Merge(branchName);

                if (!success2)
                {
                    var bodyMessage = $"Failed to merge {branchName} into master\r\n";

                    MessageBox.Show(this, bodyMessage, "Failed merge", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Error: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var argsTags = new GitArgumentBuilder("push")
            {
                "origin",
                "--tags"
            };
            RunCommand(argsTags);
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

        private void cbType_SelectedValueChanged(object sender, EventArgs e)
        {
            lblPrefixName.Text = cbType.SelectedValue + "/";
            LoadBaseBranches();
            ModifyNamingConventionTextBox();
        }

        private void ModifyNamingConventionTextBox()
        {
            var branchType = cbType.SelectedValue.ToString();

            textBox3.Text = "";

            if (branchType == "production")
            {
                textBox3.Text = "(luke|force|projectname)_(tenant|target)[_v{version}]";
            }

            if (branchType == "hotfix")
            {
                textBox3.Text = "(luke|force|projectname)_(context)";
            }

            if (branchType == "release")
            {
                textBox3.Text = "(luke|force|projectname)_(tenant|target)_(YYYYmmdd)";
            }
        }

        private void cbManageType_SelectedValueChanged(object sender, EventArgs e)
        {
            var branchType = cbManageType.SelectedValue.ToString();
            lblPrefixManage.Text = branchType + "/";
            if (!string.IsNullOrWhiteSpace(branchType))
            {
                var mayHavePrs = branchType != "production";
                pnlManageBranch.Enabled = true;
                LoadBranches(branchType);
                panel4.Visible = mayHavePrs;

                if (mayHavePrs)
                {
                    UpdatePullRequestsValues();
                }
            }
            else
            {
                pnlManageBranch.Enabled = false;
                panel4.Visible = false;
            }
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

        private void lnkShiftFlow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/nvie/gitflow");
        }

        private void DisplayHead()
        {
            var args = new GitArgumentBuilder("symbolic-ref") { "HEAD" };
            var head = _gitUiCommands.GitModule.GitExecutable.GetOutput(args).Trim('*', ' ', '\n', '\r');
            lblHead.Text = head;

            var currentRef = head.RemovePrefix(GitRefName.RefsHeadsPrefix);

            if (TryExtractBranchFromHead(currentRef, out var branchTypes, out var branchName))
            {
                cbManageType.SelectedItem = branchTypes;
                CurrentBranch = branchName;
            }
        }

        private string _currentBranch = string.Empty;
        private string _currentBranchType = string.Empty;

        private void UpdatePullRequestsValues()
        {
            try
            {
                button1.Enabled = true;
                button2.Enabled = false;
                btnFinish.Enabled = false;
                comboBox1.Enabled = true;
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                linkLabel1.Visible = false;
                linkLabel2.Visible = false;

                var branchType = cbManageType.SelectedValue.ToString();
                var branchName = cbBranches.SelectedItem?.ToString();

                if (branchName == _loading.Text || string.IsNullOrEmpty(branchName))
                {
                    return;
                }

                if (_currentBranch == branchName && _currentBranchType == branchType)
                {
                    return;
                }

                var masterBranch = comboBox1.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(masterBranch) || masterBranch == _loading.Text)
                {
                    return;
                }

                var currentRepository = Path.GetFileName(_gitUiCommands.GitModule.WorkingDir.Trim('\\'));
                var repository = Repositories[currentRepository];
                var prs = repository.GetPullRequests();
                var branchPullrequests = prs.Where(p => p.Head.Ref == branchName).ToArray();

                foreach (var branchPullrequest in branchPullrequests)
                {
                    var isDevelop = branchPullrequest.Base.Ref == "develop";
                    button1.Enabled = false;
                    var number = $"PR #{branchPullrequest.Number}";
                    var link = $"{branchPullrequest.Url}".Replace("https://api.github.com/repos/", "https://github.com/").Replace("pulls", "pull");
                    if (isDevelop)
                    {
                        textBox1.Text = number;
                        linkLabel1.Text = link;
                        linkLabel1.Visible = true;
                        button2.Enabled = true;
                    }
                    else
                    {
                        textBox2.Text = number;
                        linkLabel2.Text = link;
                        linkLabel2.Visible = true;
                        btnFinish.Enabled = true;
                        comboBox1.SelectedItem = branchPullrequest.Base.Ref;
                        comboBox1.Enabled = false;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Error: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
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

                var currentRepository = Path.GetFileName(_gitUiCommands.GitModule.WorkingDir.Trim('\\'));
                var repository = Repositories[currentRepository];
                var prs = repository.GetPullRequests();
                var branchPullrequests = prs.Where(p => p.Head.Ref == branchName).ToArray();

                var toDevelop = repository.CreatePullRequest(branchName, "develop", $"PR to develop for {branchName}", $"PR to develop for {branchName}");
                var masterBranch = comboBox1.SelectedItem.ToString();
                var toMaster = repository.CreatePullRequest(branchName, masterBranch, $"PR to {masterBranch} for {branchName}", $"PR to {masterBranch} for {branchName}");

                UpdatePullRequestsValues();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Error: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show(this, $"The pull request has not been created", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var toDevelopNumber = int.Parse(textBox1.Text.Remove(0, 4));

            var branchName = cbBranches.SelectedItem.ToString();

            try
            {
                var currentRepository = Path.GetFileName(_gitUiCommands.GitModule.WorkingDir.Trim('\\'));
                var repository = Repositories[currentRepository];

                PullRequest toDevelop = repository.GetPullRequest(toDevelopNumber);

                var success1 = toDevelop.Merge(branchName);

                if (!success1)
                {
                    var bodyMessage = $"Failed to merge {branchName} into develop\r\n";

                    MessageBox.Show(this, bodyMessage, "Failed merge", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Error: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var argsTags = new GitArgumentBuilder("push")
            {
                "origin",
                "--tags"
            };
            RunCommand(argsTags);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", linkLabel2.Text);
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
