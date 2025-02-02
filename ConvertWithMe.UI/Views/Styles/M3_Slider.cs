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
        private StackPanel ticksLeft;
        private StackPanel ticksRight;
        private double borderGrowthRate = 1;
        double[] ticks = [];
        double[] tickXCoordinates = [];

        static M3_Slider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_Slider), new FrameworkPropertyMetadata(typeof(M3_Slider)));
        }

        private void SetBorders()
        {
            borderGrowthRate = (ActualWidth - (handleBorder.Margin.Left + handleBorder.Margin.Right + handleBorder.Width)) / (Maximum - Minimum);

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
            ticksLeft = Template.FindName("Ticks_Left", this) as StackPanel;
            ticksRight = Template.FindName("Ticks_Right", this) as StackPanel;

            thumb.DragStarted += Thumb_DragStarted;
            thumb.DragCompleted += Thumb_DragCompleted;

            // Calculate the growth rate after the Window loaded
            this.SizeChanged += M3_Slider_SizeChanged;
        }

        private void GetTicks()
        {
            if (Ticks.Count > 0)
            {
                ticks = Ticks.ToArray();
            }
            if (TickFrequency > 1)
            {
                int tickAmount = (int)Math.Ceiling((Maximum - Minimum) / TickFrequency);
                ticks = new double[tickAmount];
                for (int i = 0; i < tickAmount; i++)
                {
                    ticks[i] = i * TickFrequency;
                }
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Size size = new Size(base.ActualWidth, base.ActualHeight);
            int tickCount = (int)((this.Maximum - this.Minimum) / this.TickFrequency) + 1;
            Double tickFrequencySize = (size.Width * this.TickFrequency / (this.Maximum - this.Minimum));

            double scaling = (ActualWidth - 16) / ActualWidth;

            tickXCoordinates = new double[tickCount];
            for (int i = 0; i < tickCount; i++)
            {
                tickXCoordinates[i] = tickFrequencySize * i * scaling;
            }

            SetBorders();
            GetTicks();
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

            double totalLeftPadding = 0;
            double totalRightPadding = 0;

            for (int i = 0; i < tickXCoordinates.Length; i++)
            {
                Thickness ml = new Thickness(tickXCoordinates[i], 0, 0, 0);
                Thickness mr = new Thickness(0, 0, tickXCoordinates[i], 0);

                ml.Left -= totalLeftPadding;
                mr.Right -= totalRightPadding;

                totalLeftPadding += ml.Left + 4;
                totalRightPadding += mr.Right + 4;

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
                    Margin = ml,
                    Width = 4,
                    Height = 4
                };
                ticksLeft.Children.Add(el);
                ticksRight.Children.Add(er);
            }
            TickBar tb = Template.FindName("helpplease", this) as TickBar;
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
