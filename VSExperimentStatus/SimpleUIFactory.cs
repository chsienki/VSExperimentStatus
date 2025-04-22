using System.Runtime.InteropServices;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;


namespace VSExperimentStatus
{
    [Guid(PackageGuids.UIProviderString)]
    public class SimpleUIFactory(IServiceProvider serviceProvider, IVsUIElement element) : UIFactory(serviceProvider)
    {
        public static void RegisterUIElement(IServiceProvider serviceProvider, IVsUIElement element)
        {
            IVsRegisterUIFactories registrar = serviceProvider.GetService<SVsUIFactory, IVsRegisterUIFactories>();
            ErrorHandler.ThrowOnFailure(registrar.RegisterUIFactory(PackageGuids.UIProvider, new SimpleUIFactory(serviceProvider, element)));
        }

        protected override IVsUIElement CreateUIElement(ref Guid factory, uint elementId) => element;
    }
}