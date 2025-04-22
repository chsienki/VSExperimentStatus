using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSExperimentStatus
{
    public partial class StatusControl : UserControl, IVsUIElement, INonClientArea
    {
        public StatusControl()
        {
            InitializeComponent();
        }

        #region IVSUIElement

        IVsUISimpleDataSource? dataSource = null;
        
        int IVsUIElement.get_DataSource(out IVsUISimpleDataSource? ppDataSource)
        {
            ppDataSource = dataSource;
            return 0;
        }

        int IVsUIElement.put_DataSource(IVsUISimpleDataSource pDataSource)
        {
            dataSource = pDataSource;
            return 0;
        }

        int IVsUIElement.TranslateAccelerator(IVsUIAccelerator pAccel) => 0;

        int IVsUIElement.GetUIObject(out object ppUnk)
        {
            ppUnk = this;
            return 0;
        }

        #endregion

        #region INonClientArea

        // Make the control interactive when in the title bar
        int INonClientArea.HitTest(Point point) => 1;
        
        #endregion
    }
}
