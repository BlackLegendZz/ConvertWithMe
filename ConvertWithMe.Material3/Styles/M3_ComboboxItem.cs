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
using ConvertWithMe.Material3.Styles.UIEnums;
using Material.Icons;

namespace ConvertWithMe.Material3.Styles
{
    public class M3_ComboboxItem : ComboBoxItem
    {
        #region PROPDP


        public bool ShowDivider
        {
            get { return (bool)GetValue(ShowDividerProperty); }
            set { SetValue(ShowDividerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowDivider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDividerProperty =
            DependencyProperty.Register("ShowDivider", typeof(bool), typeof(M3_ComboboxItem), new PropertyMetadata(false));

        public MaterialIconKind LeadingIcon
        {
            get { return (MaterialIconKind)GetValue(LeadingIconProperty); }
            set { SetValue(LeadingIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeadingIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeadingIconProperty =
            DependencyProperty.Register("LeadingIcon", typeof(MaterialIconKind), typeof(M3_ComboboxItem), new PropertyMetadata(MaterialIconKind.Numeric));

        public bool DisplayLeadingIcon
        {
            get { return (bool)GetValue(DisplayLeadingIconProperty); }
            set { SetValue(DisplayLeadingIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayLeadingIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayLeadingIconProperty =
            DependencyProperty.Register("DisplayLeadingIcon", typeof(bool), typeof(M3_ComboboxItem), new PropertyMetadata(false));


        #endregion

        static M3_ComboboxItem()
        {
            
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_ComboboxItem), new FrameworkPropertyMetadata(typeof(M3_ComboboxItem)));
        }
    }
}
