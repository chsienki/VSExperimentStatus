using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Internal.VisualStudio.Shell.Interop;

namespace VSExperimentStatus
{
    internal class FeatureFlagsService : IVsFeatureFlagEvents
    {
        private readonly IVsFeatureFlags featureFlags;

        public List<FeatureFlag> Flags = [
            new ("Razor.LSP.ForceRuntimeCodeGeneration", "Fuse", false),
            new ("Razor.LSP.UseRazorCohostServer", "Cohosting", false)
            ];

        public event EventHandler? OnFlagsChanged;

        public FeatureFlagsService(IServiceProvider serviceProvider)
        {
            featureFlags = (IVsFeatureFlags)serviceProvider.GetService(typeof(SVsFeatureFlags));

            for(int i = 0; i < Flags.Count; i++)
            {
                var flag = Flags[i];
                bool state = featureFlags.IsFeatureEnabled(flag.FlagName, flag.Value);
                Flags[i] = Flags[i] with { Value = state };
                ((IVsFeatureFlagStateChanged)featureFlags).AdviseFeatureFlagStateChangedEvent(this, flag.FlagName);
            }
        }

        public void OnFeatureFlagStateChanged(string featureFlagName, bool value)
        {
            var index = Flags.FindIndex(f => f.FlagName == featureFlagName);
            Flags[index] = Flags[index] with { Value = value };
            OnFlagsChanged?.Invoke(this, EventArgs.Empty);
        }

        public record FeatureFlag(string FlagName, string FriendlyName, bool Value);
    }
}
