using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ConvertWithMe.UI.Views.Styles
{
    public class M3_Slider : Slider
    {
        private Border leftBorder;
        private Border rightBorder;
        private Border handleBorder;
        private Thumb thumb;
        private double valueToSize = 0;
        private int handleWidth = 16;

        static M3_Slider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_Slider), new FrameworkPropertyMetadata(typeof(M3_Slider)));
        }

        private void SetBorders()
        {
            leftBorder.Width = Math.Max((Value - Minimum) * valueToSize, 0);
            rightBorder.Width = Math.Max((Maximum - Value) * valueToSize, 0);
        }

        private void CalculateGrowthRate(int handleWidth)
        {
            valueToSize = (Width - handleWidth) / (Maximum - Minimum);
        }

        public override void OnApplyTemplate()
        {
            leftBorder = Template.FindName("PART_left", this) as Border;
            rightBorder = Template.FindName("PART_right", this) as Border;
            handleBorder = Template.FindName("PART_Handle", this) as Border;
            thumb = Template.FindName("PART_Thumb", this) as Thumb;
            thumb.DragStarted += Thumb_DragStarted;
            thumb.DragCompleted += Thumb_DragCompleted;
            
            CalculateGrowthRate(handleWidth);
            SetBorders();

            base.OnApplyTemplate();
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            CalculateGrowthRate(handleWidth);
            SetBorders();
            handleBorder.Width = 4;
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            CalculateGrowthRate(handleWidth-2);
            SetBorders();
            handleBorder.Width = 2;
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            if (leftBorder is null || rightBorder is null)
            {
                return;
            }
            SetBorders();
            base.OnValueChanged(oldValue, newValue);
        }
    }
}
