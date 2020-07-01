using System.Collections.Generic;
using System.ComponentModel.Composition;
using GitUIPluginInterfaces;
using GitUIPluginInterfaces.UserControls;
using ResourceManager;
using ShiftFlow.Properties;

namespace ShiftFlow
{
    [Export(typeof(IGitPlugin))]
    public class ShiftFlowPlugin : GitPluginBase, IGitPluginForRepository
    {
        private IGitModule _gitModule;

        private readonly CredentialsSetting _gitHubCredentials;

        public ShiftFlowPlugin() : base(true)
        {
            SetNameAndDescription("ShiftFlow");
            Translate();
            Icon = Resource.IconGitFlow;

            _gitHubCredentials = new CredentialsSetting("GitHubCredentials", "GitHub credentials", () => _gitModule?.WorkingDir);
        }

        public override bool Execute(GitUIEventArgs args)
        {
            using (var frm = new ShiftFlowForm(args))
            {
                frm.ShowDialog(args.OwnerForm);
                return frm.IsRefreshNeeded;
            }
        }

        public override IEnumerable<ISetting> GetSettings()
        {
            _gitHubCredentials.CustomControl = new CredentialsControl();
            yield return _gitHubCredentials;
        }

        public override void Register(IGitUICommands gitUiCommands)
        {
            base.Register(gitUiCommands);
            _gitModule = gitUiCommands.GitModule;
        }
    }
}
