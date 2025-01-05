using ConvertWithMe.Core;
using ConvertWithMe.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace ConvertWithMe.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IServiceCollection services = new ServiceCollection();
        public IServiceProvider serviceProvider;

        public App()
        {
            // ViewModels
            services.AddSingleton<DialogViewModel>();
            services.AddSingleton<ItemListViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SettingsViewModel>();
            // Dialog ViewModels
            services.AddSingleton<NotificationDialogViewModel>();
            services.AddSingleton<QuestionDialogViewModel>();

            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitPaths();
        }
        
        /// <summary>
        /// Initializes all Paths that are used by the application.
        /// </summary>
        private static void InitPaths()
        {
            var x = typeof(ApplicationPaths).GetFields();

            foreach (var path in x)
            {
                var p = path.GetValue(typeof(string)) as string;
                if (p!= null && !Directory.Exists(p)) { Directory.CreateDirectory(p); }
            }
        }
    }
}
