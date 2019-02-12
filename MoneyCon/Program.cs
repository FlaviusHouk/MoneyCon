using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging.Serilog;
using Avalonia.Threading;
using MoneyCon.ViewModel;

namespace MoneyCon
{
    class Program
    {
        static void Main(string[] args)
        {
            AppBuilder builder = BuildAvaloniaApp();

            MainViewModel model = App.CurrentInstance.MainViewModel;
            App.CurrentInstance.MainWindow = new MainWindow() { DataContext = model };

            AuthWindow authWindow = new AuthWindow
            {
                DataContext = new AuthWindowViewModel(model.WebService, model.DialogService)
            };
            authWindow.Show();

            builder.Instance.Run(App.CurrentInstance.MainWindow);
        }

        public static void Useless()
        {
            TabControl cont = new TabControl();
            TabItem item = new TabItem();
            
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                             .UsePlatformDetect()
                             .SetupWithoutStarting()
                             .LogToDebug();
        }
    }
}
