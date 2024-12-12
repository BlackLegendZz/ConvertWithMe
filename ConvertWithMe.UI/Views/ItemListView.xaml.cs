using ConvertWithMe.UI.ViewModels;
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
			DataContext = new ItemListViewModel();
			InitializeComponent();
        }
    }
}
