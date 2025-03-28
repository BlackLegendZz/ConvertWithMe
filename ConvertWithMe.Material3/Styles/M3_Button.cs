using ConvertWithMe.Material3.Styles.UIEnums;
using Material.Icons;
using System.Windows;
using System.Windows.Controls;

namespace ConvertWithMe.Material3.Styles
{
    public class M3_Button : Button
    {
        #region PROPDP

        public BtnType ButtonType
        {
            get { return (BtnType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register(nameof(ButtonType), typeof(BtnType), typeof(M3_Button), new PropertyMetadata(BtnType.Filled));


        public MaterialIconKind IconKind
        {
            get { return (MaterialIconKind)GetValue(IconKindProperty); }
            set { SetValue(IconKindProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconKind.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register(nameof(IconKind), typeof(MaterialIconKind), typeof(M3_Button), new PropertyMetadata(MaterialIconKind.Numeric));


        public bool DisplayIcon
        {
            get { return (bool)GetValue(DisplayIconProperty); }
            set { SetValue(DisplayIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayIconProperty =
            DependencyProperty.Register(nameof(DisplayIcon), typeof(bool), typeof(M3_Button), new PropertyMetadata(false));

        #endregion
        
        static M3_Button()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_Button), new FrameworkPropertyMetadata(typeof(M3_Button)));
        }
    }
}
