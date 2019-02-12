using Avalonia;
using Avalonia.Markup.Xaml;
using MoneyCon.ViewModel;

namespace MoneyCon
{
    public class App : Application
    {
        public static App CurrentInstance { get; set; }

        public MainViewModel MainViewModel { get; set; }

        public MainWindow MainWindow { get; set; }

        public override void Initialize()
        {
            CurrentInstance = this;
            AvaloniaXamlLoader.Load(this);

            MainViewModel = new MainViewModel();
        }
    }
}
