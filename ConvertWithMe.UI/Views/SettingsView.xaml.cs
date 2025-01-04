using ConvertWithMe.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace ConvertWithMe.UI.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        DataContext = ((App)Application.Current).serviceProvider.GetRequiredService<SettingsViewModel>();
        InitializeComponent();
    }
}