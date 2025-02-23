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
using ConvertWithMe.Material3.Styles.UIEnums;

namespace ConvertWithMe.Material3.Styles
{
    public class M3_ToggleSwitch : ToggleButton
    {
        public IconTypeSwitch DisplayIcons
        {
            get { return (IconTypeSwitch)GetValue(DisplayIconsProperty); }
            set { SetValue(DisplayIconsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayIcons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayIconsProperty =
            DependencyProperty.Register("DisplayIcons", typeof(IconTypeSwitch), typeof(M3_ToggleSwitch), new PropertyMetadata(IconTypeSwitch.None));


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
