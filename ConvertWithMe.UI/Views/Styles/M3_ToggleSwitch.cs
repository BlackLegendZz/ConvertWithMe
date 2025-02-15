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
    public enum ToggleIcon { None, OnChecked, OnUnchecked, OnBoth }
    public class M3_ToggleSwitch : ToggleButton
    {


        public ToggleIcon UseIcon
        {
            get { return (ToggleIcon)GetValue(UseIconProperty); }
            set { SetValue(UseIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UseIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseIconProperty =
            DependencyProperty.Register("UseIcon", typeof(ToggleIcon), typeof(M3_ToggleSwitch), new PropertyMetadata(ToggleIcon.None));


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
