using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConvertWithMe.UI.Views.Styles
{
    public class M3_Slider : Slider
    {
        private Border leftBorder;
        private Border rightBorder;
        private Border handleBorder;
        private Thumb thumb;
        private Canvas ticksLeft;
        private Canvas ticksRight;
        private double borderGrowthRate = 1;
        private double sliderWidth = 1;
        double[] tickXCoordinates = [];

        static M3_Slider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_Slider), new FrameworkPropertyMetadata(typeof(M3_Slider)));
        }

        private void SetBorders()
        {
            borderGrowthRate = (ActualWidth - sliderWidth) / (Maximum - Minimum);

            leftBorder.Width = Math.Max((Value - Minimum) * borderGrowthRate, 0);
            rightBorder.Width = Math.Max((Maximum - Value) * borderGrowthRate, 0);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            leftBorder = Template.FindName("PART_left", this) as Border;
            rightBorder = Template.FindName("PART_right", this) as Border;
            handleBorder = Template.FindName("PART_Handle", this) as Border;
            thumb = Template.FindName("PART_Thumb", this) as Thumb;
            ticksLeft = Template.FindName("Ticks_Left", this) as Canvas;
            ticksRight = Template.FindName("Ticks_Right", this) as Canvas;

            thumb.DragStarted += Thumb_DragStarted;
            thumb.DragCompleted += Thumb_DragCompleted;

            // Calculate the growth rate after the Window loaded
            this.SizeChanged += M3_Slider_SizeChanged;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            sliderWidth = handleBorder.Margin.Left + handleBorder.Margin.Right + handleBorder.Width;
            // I have no idea why subtracting the with of the slider gets the correct scaling.
            double scaling = (ActualWidth - sliderWidth) / ActualWidth;
            if (TickFrequency > 1)
            {
                int tickCount = (int)Math.Ceiling((Maximum - Minimum) / TickFrequency);
                double tickFrequencySize = ActualWidth * TickFrequency / (Maximum - Minimum);

                tickXCoordinates = new double[tickCount];
                for (int i = 0; i < tickCount; i++)
                {
                    tickXCoordinates[i] = tickFrequencySize * i * scaling;
                }
                //TODO: Bei nicht echten Teilern muss noch ein Punkt hinzugefügt werden
            }
            else
            {
                double tickFrequencySize = ActualWidth / (Maximum - Minimum);

                tickXCoordinates = new double[Ticks.Count];
                for (int i = 0; i < Ticks.Count; i++)
                {
                    tickXCoordinates[i] = tickFrequencySize * (Ticks[i] - Ticks[0]) * scaling;
                }
            }
            SetBorders();
            DrawStopIndicators();
        }

        private void DrawStopIndicators()
        {
            ticksLeft.Children.Clear();
            ticksRight.Children.Clear();

            Color cl = Color.FromRgb(211, 191, 239);
            Color cr = Color.FromRgb(101, 85, 143);

            SolidColorBrush bl = new SolidColorBrush(cl);
            SolidColorBrush br = new SolidColorBrush(cr);

            for (int i = 0; i < tickXCoordinates.Length; i++)
            {
                Thickness ml = new Thickness(tickXCoordinates[i], 6, 0, 0);
                Thickness mr = new Thickness(ActualWidth - tickXCoordinates[i] - sliderWidth, 6, 0, 0);

                Ellipse el = new Ellipse()
                {
                    Fill = bl,
                    Margin = ml,
                    Width = 4,
                    Height = 4
                };
                Ellipse er = new Ellipse()
                {
                    Fill = br,
                    Margin = mr,
                    Width = 4,
                    Height = 4
                };

                ticksLeft.Children.Add(el);
                ticksRight.Children.Add(er);
            }
        }

        private void M3_Slider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetBorders();
            DrawStopIndicators();
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            handleBorder.Width = 4;
            SetBorders();
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            handleBorder.Width = 2;
            SetBorders();
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            if (leftBorder is null || rightBorder is null)
            {
                return;
            }
            base.OnValueChanged(oldValue, newValue);
            SetBorders();
        }
    }
}
