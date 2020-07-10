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

            var remotes = _gitUiCommands.GitModule.GetRemoteNames();
            cbRemote.DataSource = remotes;
            btnPull.Enabled = btnPublish.Enabled = remotes.Any();

            cbType.DataSource = BranchTypes;
            var types = new List<string> { string.Empty };
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

            if (!Branches.ContainsKey("production"))
            {
                _task.LoadAsync(() => GetBranches("production"), branches =>
                {
                    Branches.Add("production", branches);
                });
            }

            if (!Branches.ContainsKey(branchType))
            {
                _task.LoadAsync(() => GetBranches(branchType), branches =>
                {
                    Branches.Add(branchType, branches);
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
            comboBox1.DataSource = Branches["production"] ?? new[] { string.Format(_noBranchExist.Text, branchType) };
            comboBox1.Enabled = true;
            cbBranches.Enabled = isThereABranch;
            if (isThereABranch && CurrentBranch != null)
            {
                cbBranches.SelectedItem = CurrentBranch;
                CurrentBranch = null;
            }

            btnFinish.Enabled = isThereABranch;
            btnPublish.Enabled = isThereABranch;
            btnPull.Enabled = isThereABranch;
            pnlPull.Enabled = true;
        }

        private void LoadBaseBranches()
        {
            var branchType = cbType.SelectedValue.ToString();
            var manageBaseBranch = branchType == Branch.hotfix.ToString("G");
            pnlBasedOn.Visible = manageBaseBranch;
            cbBaseBranch.Enabled = manageBaseBranch;

            if (manageBaseBranch)
            {
                cbBaseBranch.DataSource = GetLocalBranches();
            }

            List<string> GetLocalBranches()
            {
                var prefix = Branch.production.ToString("G") + "/";
                var args = new GitArgumentBuilder("branch");
                return _gitUiCommands.GitModule
                    .GitExecutable.GetOutput(args)
                    .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim('*', ' ', '\n', '\r'))
                    .Where(b => b.StartsWith(prefix))
                    .ToList();
            }
        }

        #endregion

        #region Run ShiftFlow commands
        private void btnStartBranch_Click(object sender, EventArgs e)
        {
            var branchType = cbType.SelectedValue.ToString();
            var baseBranch = GetBaseBranch();
            var args = new GitArgumentBuilder("checkout")
                    {
                        "-b",
                        $"{branchType}/{txtBranchName.Text}",
                        baseBranch
                    };

            if (args == null)
            {
                return;
            }

            if (RunCommand(args))
            {
                txtBranchName.Text = string.Empty;
                if (cbManageType.SelectedValue.ToString() == branchType)
                {
                    Branches.Remove(branchType);
                    LoadBranches(branchType);
                }
                else
                {
                    Branches.Remove(branchType);
                }
            }
        }

        private string GetBaseBranch()
        {
            var branchType = cbType.SelectedValue.ToString();

            if (branchType == Branch.hotfix.ToString("G"))
            {
                return $"{Branch.production}/{cbBaseBranch.SelectedItem}";
            }

            return "develop";
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            var args = new GitArgumentBuilder("push")
            {
                "origin",
                cbBranches.SelectedValue.ToString(),
                "-u"
            };
            RunCommand(args);
        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            var args = new GitArgumentBuilder("pull")
            {
                "origin",
                "--rebase"
            };
            RunCommand(args);
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show(this, $"The pull requests have not been created", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var toDevelopNumber = int.Parse(textBox1.Text.Remove(0, 4));
            var toMasterNumber = int.Parse(textBox2.Text.Remove(0, 4));

            var branchName = cbBranches.SelectedItem.ToString();

            try
            {
                var currentRepository = Path.GetFileName(_gitUiCommands.GitModule.WorkingDir.Trim('\\'));
                var repository = Repositories[currentRepository];

                PullRequest toDevelop = repository.GetPullRequest(toDevelopNumber);
                PullRequest toMaster = repository.GetPullRequest(toMasterNumber);

                var success1 = toDevelop.Merge(branchName);
                var success2 = toMaster.Merge(branchName);

                if (!success1 || !success2)
                {
                    var bodyMessage = string.Empty;
                    if (!success1)
                    {
                        bodyMessage += $"Failed to merge {branchName} into develop\r\n";
                    }

                    if (!success2)
                    {
                        bodyMessage += $"Failed to merge {branchName} into master\r\n";
                    }

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
                button1.Enabled = false;
                btnFinish.Enabled = false;
                comboBox1.Enabled = true;
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;

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

                var masterBranch = comboBox1.SelectedItem.ToString();

                if (string.IsNullOrEmpty(masterBranch) || masterBranch == _loading.Text)
                {
                    return;
                }

                var currentRepository = Path.GetFileName(_gitUiCommands.GitModule.WorkingDir.Trim('\\'));
                var repository = Repositories[currentRepository];
                var prs = repository.GetPullRequests();
                var branchPullrequests = prs.Where(p => p.Head.Ref == branchName).ToArray();

                if (branchPullrequests.Length == 2)
                {
                    var toDevelop = branchPullrequests.FirstOrDefault(b => b.Base.Ref == "develop");
                    var toMaster = branchPullrequests.FirstOrDefault(b => b.Base.Ref != "develop");
                    comboBox1.SelectedItem = toMaster.Base.Ref;
                    comboBox1.Enabled = false;

                    textBox1.Text = $"PR #{toDevelop.Number}";
                    textBox2.Text = $"PR #{toMaster.Number}";
                    button1.Enabled = false;
                    btnFinish.Enabled = true;
                }
                else
                {
                    button1.Enabled = true;
                    btnFinish.Enabled = false;
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
