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

namespace ConvertWithMe.Material3.Styles
{
    [TemplatePart(Name = "PART_LabelText", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_Content", Type = typeof(ContentPresenter))]
    public class M3_ComboBox : ComboBox
    {
        #region DEPENDENCY PROPERTIES


        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(M3_ComboBox), new PropertyMetadata(string.Empty));


        #endregion

        private ContentPresenter PART_Content;
        private TextBlock PART_LabelText;

        static M3_ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_ComboBox), new FrameworkPropertyMetadata(typeof(M3_ComboBox)));
        }

        // Not needed but gets rid of the annoying warnings
        public M3_ComboBox()
        {
            PART_LabelText = new TextBlock();
            PART_Content = new ContentPresenter();
        }

        private void PutLabelInFocus()
        {
            if (PART_LabelText.Text == string.Empty)
            {
                return;
            }
            PART_LabelText.FontSize = 16;
            PART_LabelText.LineHeight = 24;
            Grid.SetRow(PART_LabelText, 0);
            Grid.SetRow(PART_Content, 0);
        }

        private void PutLabelOutOfFocus()
        {
            if (PART_LabelText.Text == string.Empty)
            {
                return;
            }
            PART_LabelText.FontSize = 12;
            PART_LabelText.LineHeight = 16;
            Grid.SetRow(PART_LabelText, 0);
            Grid.SetRow(PART_Content, 1);

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_LabelText = (TextBlock)GetTemplateChild(nameof(PART_LabelText));
            PART_Content = (ContentPresenter)GetTemplateChild(nameof(PART_Content));

            if ((string)PART_Content.Content != string.Empty)
            {
                PutLabelOutOfFocus();
            }

            GotFocus += M3_ComboBox_GotFocus;
            LostFocus += M3_ComboBox_LostFocus;
        }

        private void M3_ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((string)PART_Content.Content != string.Empty)
            {
                PutLabelInFocus();
            }
        }

        private void M3_ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PutLabelOutOfFocus();
        }
    }
}
