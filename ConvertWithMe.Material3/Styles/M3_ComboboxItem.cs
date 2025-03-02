using System.Windows;
using System.Windows.Controls;
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
            DependencyProperty.Register(nameof(ShowDivider), typeof(bool), typeof(M3_ComboboxItem), new PropertyMetadata(false));

        public MaterialIconKind LeadingIcon
        {
            get { return (MaterialIconKind)GetValue(LeadingIconProperty); }
            set { SetValue(LeadingIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeadingIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeadingIconProperty =
            DependencyProperty.Register(nameof(LeadingIcon), typeof(MaterialIconKind), typeof(M3_ComboboxItem), new PropertyMetadata(MaterialIconKind.Numeric));

        public bool DisplayLeadingIcon
        {
            get { return (bool)GetValue(DisplayLeadingIconProperty); }
            set { SetValue(DisplayLeadingIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayLeadingIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayLeadingIconProperty =
            DependencyProperty.Register(nameof(DisplayLeadingIcon), typeof(bool), typeof(M3_ComboboxItem), new PropertyMetadata(false));


        #endregion

        static M3_ComboboxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_ComboboxItem), new FrameworkPropertyMetadata(typeof(M3_ComboboxItem)));
        }
    }
}
