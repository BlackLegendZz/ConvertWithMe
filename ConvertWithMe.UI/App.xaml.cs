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
