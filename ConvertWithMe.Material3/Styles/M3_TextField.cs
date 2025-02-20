using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConvertWithMe.Material3.Styles
{
    public enum FieldType { Filled, Outlined }
    public enum IconType { None, LeadingIcon, TrailingIcon, Both }

    [TemplatePart(Name = "PART_LabelText", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_SupportingText", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_CharacterCount", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_Text", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_TrailingIconButton", Type = typeof(Button))]
    public class M3_TextField : TextBox
    {
        #region Dependency Properties
        internal bool IsErrorState
        {
            get { return (bool)GetValue(IsErrorStateProperty); }
            set { SetValue(IsErrorStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsErrorState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsErrorStateProperty =
            DependencyProperty.Register("IsErrorState", typeof(bool), typeof(M3_TextField), new PropertyMetadata(false));

        public FieldType TextFieldType
        {
            get { return (FieldType)GetValue(TextFieldTypeProperty); }
            set { SetValue(TextFieldTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextFieldType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextFieldTypeProperty =
            DependencyProperty.Register(nameof(TextFieldType), typeof(FieldType), typeof(M3_TextField), new PropertyMetadata(FieldType.Filled));

        public IconType DisplayIcons
        {
            get { return (IconType)GetValue(DisplayIconsProperty); }
            set { SetValue(DisplayIconsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayIcons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayIconsProperty =
            DependencyProperty.Register(nameof(DisplayIcons), typeof(IconType), typeof(M3_TextField), new PropertyMetadata(IconType.None));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(M3_TextField), new PropertyMetadata(string.Empty));

        public string SupportingText
        {
            get { return (string)GetValue(SupportingTextProperty); }
            set { SetValue(SupportingTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SupportingText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SupportingTextProperty =
            DependencyProperty.Register(nameof(SupportingText), typeof(string), typeof(M3_TextField), new PropertyMetadata(string.Empty));

        internal string CharacterCount
        {
            get { return (string)GetValue(CharacterCountProperty); }
            set { SetValue(CharacterCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharacterCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharacterCountProperty =
            DependencyProperty.Register(nameof(CharacterCount), typeof(string), typeof(M3_TextField), new PropertyMetadata(string.Empty));

        public int CharLimit
        {
            get { return (int)GetValue(CharLimitProperty); }
            set { SetValue(CharLimitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharLimit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharLimitProperty =
            DependencyProperty.Register(nameof(CharLimit), typeof(int), typeof(M3_TextField), new PropertyMetadata(0));
        #endregion

        private TextBlock PART_LabelText;
        private TextBlock PART_SupportingText;
        private TextBlock PART_CharacterCount;
        private TextBox PART_Text;
        private Button PART_TrailingIconButton;

        // Not needed but gets rid of the annoying warnings
        public M3_TextField()
        {
            PART_LabelText = new TextBlock();
            PART_SupportingText = new TextBlock();
            PART_CharacterCount = new TextBlock();
            PART_Text = new TextBox();
            PART_TrailingIconButton = new Button();
        }

        static M3_TextField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_TextField), new FrameworkPropertyMetadata(typeof(M3_TextField)));
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
            Grid.SetRow(PART_Text, 0);
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
            Grid.SetRow(PART_Text, 1);

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            PART_LabelText = (TextBlock)GetTemplateChild(nameof(PART_LabelText));
            PART_SupportingText = (TextBlock)GetTemplateChild(nameof(PART_SupportingText));
            PART_CharacterCount = (TextBlock)GetTemplateChild(nameof(PART_CharacterCount));
            PART_Text = (TextBox)GetTemplateChild(nameof(PART_Text));
            PART_TrailingIconButton = (Button)GetTemplateChild(nameof(PART_TrailingIconButton));

            GotFocus += PART_Text_GotFocus;
            PART_Text.LostFocus += PART_Text_LostFocus;
            PART_TrailingIconButton.Click += PART_TrailingIconButton_Click;
            PART_Text.TextChanged += PART_Text_TextChanged;

            if (PART_Text.Text != string.Empty)
            {
                PutLabelOutOfFocus();
            }

            if (CharLimit > 0)
            {
                CharacterCount = $"{PART_Text.Text.Length}/{CharLimit}";
                PART_CharacterCount.Visibility = Visibility.Visible;
            }
        }

        private void PART_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CharLimit > 0)
            {
                CharacterCount = $"{PART_Text.Text.Length}/{CharLimit}";
                if (PART_Text.Text.Length > CharLimit)
                {
                    SupportingText = "Text exceeds maximum length";
                    IsErrorState = true;
                }
                else
                {
                    if (SupportingText == "Text exceeds maximum length")
                    {
                        SupportingText = string.Empty;
                        IsErrorState = false;
                    }
                }
            }

            if (PART_Text.Text != string.Empty)
            {
                PutLabelOutOfFocus();
                return;
            }

            if (PART_Text.Text == string.Empty && !PART_Text.IsFocused)
            {
                PutLabelInFocus();
            }
        }

        private void PART_TrailingIconButton_Click(object sender, RoutedEventArgs e)
        {
            PART_Text.Text = string.Empty;
            PutLabelInFocus();
        }

        private void PART_Text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PART_Text.Text == string.Empty)
            {
                PutLabelInFocus();
            }
        }

        private void PART_Text_GotFocus(object sender, RoutedEventArgs e)
        {
            PutLabelOutOfFocus();
        }
    }
}
