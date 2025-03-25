using ConvertWithMe.Material3.Styles.UIEnums;
using Material.Icons;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConvertWithMe.Material3.Styles
{
    public class M3_Button : Button
    {
        #region PROPDP
        public double DropShadowOpacity
        {
            get { return (double)GetValue(DropShadowOpacityProperty); }
            set { SetValue(DropShadowOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DropShadowOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropShadowOpacityProperty =
            DependencyProperty.Register(nameof(DropShadowOpacity), typeof(double), typeof(M3_Button), new PropertyMetadata(0.0));


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

        public M3_Button()
        {
            this.MouseEnter += M3_Button_MouseEnter;
            this.MouseLeave += M3_Button_MouseLeave;
            this.MouseLeftButtonDown += M3_Button_MouseLeftButtonDown;
        }

        private void M3_Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DropShadowOpacity = 0.0;
        }

        private void M3_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            DropShadowOpacity = 0.0;
        }

        private void M3_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            DropShadowOpacity = 0.3;
        }
    }
}
