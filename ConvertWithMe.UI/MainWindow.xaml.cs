using ConvertWithMe.UI.ViewModels;
using System.Windows;

namespace ConvertWithMe.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (SystemParameters.PrimaryScreenWidth <= 1920)
            {
                Width = MinWidth;
                Height = MinHeight;
            }
            else if (SystemParameters.PrimaryScreenWidth <= 2560)
            {
                Width = Math.Round(1.5 * MinWidth);
                Height = Math.Round(1.5 * MinHeight);
            }
            else
            {
                Width = Math.Round(2 * MinWidth);
                Height = Math.Round(2 * MinHeight);
            }
        }
    }
}