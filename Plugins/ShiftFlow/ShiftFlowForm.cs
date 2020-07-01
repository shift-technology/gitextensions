using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
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
        private readonly TranslationString _ShiftFlowTooltip = new TranslationString("A good branch model for your project with Git...");
        private readonly TranslationString _loading = new TranslationString("Loading...");
        private readonly TranslationString _noBranchExist = new TranslationString("No {0} branches exist.");

        private readonly GitUIEventArgs _gitUiCommands;

        private Dictionary<string, IReadOnlyList<string>> Branches { get; } = new Dictionary<string, IReadOnlyList<string>>();

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
            var prs = GetPullRequests(cbManageType.SelectedValue.ToString(), cbBranches.SelectedValue.ToString());

            var argsTags = new GitArgumentBuilder("push")
            {
                "origin",
                "--tags"
            };
            RunCommand(argsTags);
        }

        private object[] GetPullRequests(string v1, string v2)
        {
            throw new NotImplementedException();
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
                pnlManageBranch.Enabled = true;
                LoadBranches(branchType);
            }
            else
            {
                pnlManageBranch.Enabled = false;
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
    }
}
