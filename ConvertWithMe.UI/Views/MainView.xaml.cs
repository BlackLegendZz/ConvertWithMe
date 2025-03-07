﻿using ConvertWithMe.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace ConvertWithMe.UI.Views
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            DataContext = ((App)Application.Current).serviceProvider.GetRequiredService<MainViewModel>();
            InitializeComponent();
        }
    }
}
