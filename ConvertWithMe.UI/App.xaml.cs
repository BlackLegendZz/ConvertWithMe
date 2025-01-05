using ConvertWithMe.Core;
using ConvertWithMe.Core.Installer;
using ConvertWithMe.UI.Models;
using ConvertWithMe.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

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
            services.AddSingleton<ProgressDialogViewModel>();

            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitPaths();

            if (!Directory.GetFiles(ApplicationPaths.FFmpeg).Contains(Path.Combine(ApplicationPaths.FFmpeg, "ffmpeg.exe")))
            {
                DialogViewModel dialogViewModel = serviceProvider.GetRequiredService<DialogViewModel>();
                ProgressDialogViewModel progressDialogViewModel = serviceProvider.GetRequiredService<ProgressDialogViewModel>();

                dialogViewModel.ShowDialogAsync(
                    progressDialogViewModel,
                    new ProgressViewModelData(
                        "Downloading required files", 
                        "The dependency ffmpeg couldn't be found so its currently being downloaded as it is essential for ConvertWithMe to function."
                        )
                    ).ConfigureAwait(false);
                FFmpegUpdate.DownloadAndInstallNewestVersion((p, m) =>
                {
                    progressDialogViewModel.Progress = p;
                    progressDialogViewModel.ProgressMessage = m;
                }).ConfigureAwait(false);
            }
            
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
