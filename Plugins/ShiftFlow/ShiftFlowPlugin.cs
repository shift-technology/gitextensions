using System.Collections.Generic;
using System.ComponentModel.Composition;
using GitUIPluginInterfaces;
using ResourceManager;
using ShiftFlow.Properties;

namespace ShiftFlow
{
    [Export(typeof(IGitPlugin))]
    public class ShiftFlowPlugin : GitPluginBase, IGitPluginForRepository
    {
        public readonly StringSetting OAuthToken = new StringSetting("OAuth Token", "");
        internal static ShiftFlowPlugin Instance;

        public ShiftFlowPlugin() : base(true)
        {
            SetNameAndDescription("ShiftFlow");
            Translate();
            Icon = Resource.IconGitFlow;

            if (Instance == null)
            {
                Instance = this;
            }
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
            yield return OAuthToken;
        }
    }
}
