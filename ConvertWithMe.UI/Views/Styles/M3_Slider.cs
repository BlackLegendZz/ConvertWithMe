using System.Runtime.CompilerServices;
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

        static M3_Slider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_Slider), new FrameworkPropertyMetadata(typeof(M3_Slider)));
        }

        private void SetBorders()
        {
            leftBorder.Width = Math.Max((Value - Minimum) * valueToSize, 0);
            rightBorder.Width = Math.Max((Maximum - Value) * valueToSize, 0);
        }

        private void CalculateGrowthRate()
        {
            valueToSize = (ActualWidth - (handleBorder.Margin.Left + handleBorder.Margin.Right + handleBorder.Width)) / (Maximum - Minimum);
        }

        public override void OnApplyTemplate()
        {
            leftBorder = Template.FindName("PART_left", this) as Border;
            rightBorder = Template.FindName("PART_right", this) as Border;
            handleBorder = Template.FindName("PART_Handle", this) as Border;
            thumb = Template.FindName("PART_Thumb", this) as Thumb;
            thumb.DragStarted += Thumb_DragStarted;
            thumb.DragCompleted += Thumb_DragCompleted;

            base.OnApplyTemplate();

            // Calculate the growth rate after the Window loaded
            this.SizeChanged += M3_Slider_SizeChanged;
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, () => CalculateGrowthRate());
        }

        private void M3_Slider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CalculateGrowthRate();
            SetBorders();
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            handleBorder.Width = 4;
            CalculateGrowthRate();
            SetBorders();
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            handleBorder.Width = 2;
            CalculateGrowthRate();
            SetBorders();
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
