using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConvertWithMe.UI.Views.Styles
{
    [TemplatePart(Name = nameof(PART_left), Type = typeof(Border))]
    [TemplatePart(Name = nameof(PART_right), Type = typeof(Border))]
    [TemplatePart(Name = nameof(PART_Handle), Type = typeof(Border))]
    [TemplatePart(Name = nameof(thumb), Type = typeof(Thumb))]
    [TemplatePart(Name = nameof(Ticks_Left), Type = typeof(Canvas))]
    [TemplatePart(Name = nameof(Ticks_Right), Type = typeof(Canvas))]
    public class M3_Slider : Slider
    {
        private Border PART_left;
        private Border PART_right;
        private Border PART_Handle;
        private Thumb thumb;
        private Canvas Ticks_Left;
        private Canvas Ticks_Right;
        private double borderGrowthRate = 1;
        private double sliderWidth = 1;
        double[] tickXCoordinates = [];
        Ellipse[] tickElements;
        static M3_Slider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(M3_Slider), new FrameworkPropertyMetadata(typeof(M3_Slider)));
        }

        private void SetBorders()
        {
            borderGrowthRate = (ActualWidth - sliderWidth) / (Maximum - Minimum);

            PART_left.Width = Math.Max((Value - Minimum) * borderGrowthRate, 0);
            PART_right.Width = Math.Max((Maximum - Value) * borderGrowthRate, 0);
        }

        public override void OnApplyTemplate()
        {
            // Function can be called more than once
            if (thumb != null)
            {
                thumb.DragStarted -= Thumb_DragStarted;
                thumb.DragCompleted -= Thumb_DragCompleted;
            }

            base.OnApplyTemplate();

            PART_left = GetTemplateChild(nameof(PART_left)) as Border;
            PART_right = GetTemplateChild(nameof(PART_right)) as Border;
            PART_Handle = GetTemplateChild(nameof(PART_Handle)) as Border;
            thumb = GetTemplateChild(nameof(thumb)) as Thumb;
            Ticks_Left = GetTemplateChild(nameof(Ticks_Left)) as Canvas;
            Ticks_Right = GetTemplateChild(nameof(Ticks_Right)) as Canvas;

            if (thumb != null)
            {
                thumb.DragStarted += Thumb_DragStarted;
                thumb.DragCompleted += Thumb_DragCompleted;
            }
            // Calculate the growth rate after the Window loaded
            SizeChanged += M3_Slider_SizeChanged;
            IsEnabledChanged += M3_Slider_IsEnabledChanged;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            sliderWidth = PART_Handle.Margin.Left + PART_Handle.Margin.Right + PART_Handle.Width;

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

        private Tuple<SolidColorBrush, SolidColorBrush> GetCorrectColours()
        {
            string cn1 = string.Empty;
            string cn2 = string.Empty;
            if (IsEnabled)
            {
                cn1 = "OnPrimary";
                cn2 = "OnSecondaryContainer";
            }
            else
            {
                cn1 = "InverseOnSurface";
                cn2 = "OnSurface";
            }

            return new Tuple<SolidColorBrush, SolidColorBrush>(
                (Application.Current.Resources[cn1] as SolidColorBrush) ?? new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                (Application.Current.Resources[cn2] as SolidColorBrush) ?? new SolidColorBrush(Color.FromRgb(0, 0, 0))
            );
        }

        private void DrawStopIndicators()
        {
            Ticks_Left.Children.Clear();
            Ticks_Right.Children.Clear();
            tickElements = new Ellipse[tickXCoordinates.Length * 2];

            (SolidColorBrush ColStopIndicatorSelected, SolidColorBrush ColStopIndicator) = GetCorrectColours();
            
            for (int i = 0; i < tickXCoordinates.Length; i++)
            {
                Thickness ml = new Thickness(tickXCoordinates[i], 6, 0, 0);
                Thickness mr = new Thickness(ActualWidth - tickXCoordinates[i] - sliderWidth, 6, 0, 0);

                Ellipse el = new Ellipse()
                {
                    Fill = ColStopIndicatorSelected,
                    Margin = ml,
                    Width = 4,
                    Height = 4
                };
                Ellipse er = new Ellipse()
                {
                    Fill = ColStopIndicator,
                    Margin = mr,
                    Width = 4,
                    Height = 4
                };

                Ticks_Left.Children.Add(el);
                Ticks_Right.Children.Add(er);
                tickElements[i] = el;
                tickElements[i + tickXCoordinates.Length] = er;
            }
        }

        private void M3_Slider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetBorders();
            DrawStopIndicators();
        }

        private void M3_Slider_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (SolidColorBrush ColStopIndicatorSelected, SolidColorBrush ColStopIndicator) = GetCorrectColours();
            for (int i = 0; i < tickXCoordinates.Length; i++)
            {
                tickElements[i].Fill = ColStopIndicatorSelected;
                tickElements[i + tickXCoordinates.Length].Fill = ColStopIndicator;
            }
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            PART_Handle.Width = 4;
            SetBorders();
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            PART_Handle.Width = 2;
            SetBorders();
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            if (PART_left is null || PART_right is null)
            {
                return;
            }
            base.OnValueChanged(oldValue, newValue);
            SetBorders();
        }
    }
}
