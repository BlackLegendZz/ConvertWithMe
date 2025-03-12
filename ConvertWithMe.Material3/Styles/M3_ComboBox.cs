using System.Reflection;
using System.Windows;
using System.Windows.Controls;

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
            DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(M3_ComboBox), new PropertyMetadata(string.Empty));


        #endregion

        private ContentPresenter PART_Content = new();
        private TextBlock PART_LabelText = new();
        private readonly Style styleBodyLarge;
        private readonly Style styleBodySmall;

        static M3_ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_ComboBox), new FrameworkPropertyMetadata(typeof(M3_ComboBox)));
        }

        public M3_ComboBox()
        {
            Style? sBodyLarge = (Style?)FindResource("BodyLarge_TBl");
            Style? sBodySmall = (Style?)FindResource("BodySmall_TBl");

            if (sBodyLarge == null || sBodySmall == null)
            {
                throw new NullReferenceException("M3_ComboBox: StyleBodyLarge or StyleBodySmall are null");
            }
            styleBodyLarge = sBodyLarge;
            styleBodySmall = sBodySmall;
        }
        private void PutLabelInFocus()
        {
            if (PART_LabelText.Text == string.Empty)
            {
                return;
            }
            PART_LabelText.Style = styleBodyLarge;
            Grid.SetRow(PART_LabelText, 0);
            Grid.SetRow(PART_Content, 0);
        }

        private void PutLabelOutOfFocus()
        {
            if (PART_LabelText.Text == string.Empty)
            {
                return;
            }
            PART_LabelText.Style = styleBodySmall;
            Grid.SetRow(PART_LabelText, 0);
            Grid.SetRow(PART_Content, 1);

        }

        private string GetContentValue()
        {
            string contentValue = string.Empty;
            if (PART_Content.Content == null)
            {
                return contentValue;
            }
            
            contentValue = PART_Content.Content.ToString()?? contentValue;
            Type cType = PART_Content.Content.GetType();
            PropertyInfo? property = cType.GetProperty(DisplayMemberPath);
            
            if (property == null)
            {
                return contentValue;
            }
            
            object? v = property.GetValue(PART_Content.Content);
            if (v != null)
            {
                contentValue = v.ToString()?? contentValue;
            }
            
            return contentValue;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_LabelText = GetTemplateChild(nameof(PART_LabelText)) as TextBlock?? PART_LabelText;
            PART_Content = GetTemplateChild(nameof(PART_Content)) as ContentPresenter?? PART_Content;
            
            string contentValue = GetContentValue();            
            if (contentValue != string.Empty)
            {
                PutLabelOutOfFocus();
            }

            GotFocus += M3_ComboBox_GotFocus;
            LostFocus += M3_ComboBox_LostFocus;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            string contentValue = GetContentValue();            
            if (contentValue != string.Empty)
            {
                PutLabelOutOfFocus();
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new M3_ComboboxItem();
        }

        private void M3_ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string contentValue = GetContentValue();
            if (contentValue == string.Empty)
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
