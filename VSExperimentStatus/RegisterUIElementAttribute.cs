using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSExperimentStatus
{
    internal class RegisterUIElementAttribute : RegistrationAttribute
    {
        public override void Register(RegistrationContext context)
        {
            using var controlsKey = context.CreateKey($"MainWindowFrameControls\\{PackageGuids.UIButton:B}");
            controlsKey.SetValue(string.Empty, "VSExperimentStatusControl");
            controlsKey.SetValue("Package", PackageGuids.VSExperimentStatus.ToString("B"));
            controlsKey.SetValue("DisplayName", "VS Experiments");
            controlsKey.SetValue("ViewFactory", PackageGuids.UIProvider.ToString("B"));
            controlsKey.SetValue("ViewId", 100);
            controlsKey.SetValue("Alignment", "TitleBarRight");
            controlsKey.SetValue("Sort", 10);
            controlsKey.SetValue("FullScreenAlignment", "MenuBarRight");
            controlsKey.SetValue("FullScreenSort", 10);
            controlsKey.SetValue("MinimalAlignment", "ToolBarRight");
            controlsKey.SetValue("MinimalSort", 10);
            controlsKey.SetValue("NoToolbarAlignment", "TitleBarRight");
            controlsKey.SetValue("NoToolbarSort", 10);
        }

        public override void Unregister(RegistrationContext context)
        {
            context.RemoveKey($"MainWindowFrameControls\\{PackageGuids.VSExperimentStatusString:B}");
        }
    }
}
