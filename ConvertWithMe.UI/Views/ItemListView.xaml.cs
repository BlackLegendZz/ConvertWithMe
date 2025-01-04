using ConvertWithMe.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace ConvertWithMe.UI.Views
{
    /// <summary>
    /// Interaktionslogik für ItemListView.xaml
    /// </summary>
    public partial class ItemListView : UserControl
    {
        public ItemListView()
        {
            DataContext = ((App)Application.Current).serviceProvider.GetRequiredService<ItemListViewModel>();
			InitializeComponent();
        }
    }
}
