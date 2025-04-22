global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Internal.VisualStudio.PlatformUI;


namespace VSExperimentStatus
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [Guid(PackageGuids.VSExperimentStatusString)]
    [ProvideUIProvider(PackageGuids.UIProviderString, "VS Experiments UI Provider", PackageGuids.VSExperimentStatusString)]
    [RegisterUIElement]
    public sealed class VSExperimentStatusPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var featureFlagsService = new FeatureFlagsService(this);
            var viewModel = new StatusViewModel(featureFlagsService);
            var view = new StatusControl() { DataContext = viewModel };

            SimpleUIFactory.RegisterUIElement(this, view);
        }
    }
}