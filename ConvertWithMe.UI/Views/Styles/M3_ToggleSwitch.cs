using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConvertWithMe.UI.Views.Styles
{
    public class M3_ToggleSwitch : ToggleButton
    {
        static M3_ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_ToggleSwitch), new FrameworkPropertyMetadata(typeof(M3_ToggleSwitch)));
        }

        public override void OnApplyTemplate()
        {
            //Todo:
            // Handle the icon option. There might be more things later.
            base.OnApplyTemplate();
        }
    }
}
