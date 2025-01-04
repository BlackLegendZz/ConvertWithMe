using ConvertWithMe.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace ConvertWithMe.UI.Views
{
    /// <summary>
    /// Interaktionslogik für DialogViewModel.xaml
    /// </summary>
    public partial class DialogView : UserControl
    {
        public DialogView()
        {
            DataContext = ((App)Application.Current).serviceProvider.GetRequiredService<DialogViewModel>();
            InitializeComponent();
        }
    }
}
