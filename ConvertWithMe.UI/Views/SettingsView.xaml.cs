using ConvertWithMe.UI.ViewModels;
using System.Windows.Controls;

namespace ConvertWithMe.UI.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        DataContext = new SettingsViewModel();
        InitializeComponent();
    }
}