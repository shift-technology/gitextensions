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
        public const string GearsEnvironment = "Gears";
        public const string ForceEnvironment = "Force v2";

        public readonly StringSetting OAuthToken = new StringSetting("OAuth Token", "");
        public readonly ChoiceSetting EnvironmentSetting = new ChoiceSetting("Project environment", new List<string>() { ForceEnvironment, GearsEnvironment }, ForceEnvironment);
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
            yield return EnvironmentSetting;
        }
    }
}
