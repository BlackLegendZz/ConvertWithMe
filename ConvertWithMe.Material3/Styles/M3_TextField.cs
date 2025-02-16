using System.Windows;
using System.Windows.Controls;

namespace ConvertWithMe.Material3.Styles
{
    public enum FieldType { Filled, Outlined }

    public class M3_TextField : TextBox
    {
        public FieldType TextFieldType
        {
            get { return (FieldType)GetValue(TextFieldTypeProperty); }
            set { SetValue(TextFieldTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextFieldType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextFieldTypeProperty =
            DependencyProperty.Register("TextFieldType", typeof(FieldType), typeof(M3_TextField), new PropertyMetadata(FieldType.Filled));

        public bool DisplayIcons
        {
            get { return (bool)GetValue(DisplayIconsProperty); }
            set { SetValue(DisplayIconsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayIcons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayIconsProperty =
            DependencyProperty.Register("DisplayIcons", typeof(bool), typeof(M3_TextField), new PropertyMetadata(false));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(M3_TextField), new PropertyMetadata(string.Empty));

        public string SupportingText
        {
            get { return (string)GetValue(SupportingTextProperty); }
            set { SetValue(SupportingTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SupportingText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SupportingTextProperty =
            DependencyProperty.Register("SupportingText", typeof(string), typeof(M3_TextField), new PropertyMetadata(string.Empty));

        public string CharacterCount
        {
            get { return (string)GetValue(CharacterCountProperty); }
            set { SetValue(CharacterCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharacterCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharacterCountProperty =
            DependencyProperty.Register("CharacterCount", typeof(string), typeof(M3_TextField), new PropertyMetadata(string.Empty));

        public int CharLimit
        {
            get { return (int)GetValue(CharLimitProperty); }
            set { SetValue(CharLimitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharLimit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharLimitProperty =
            DependencyProperty.Register("CharLimit", typeof(int), typeof(M3_TextField), new PropertyMetadata(0));



        static M3_TextField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_TextField), new FrameworkPropertyMetadata(typeof(M3_TextField)));
        }
    }
}
