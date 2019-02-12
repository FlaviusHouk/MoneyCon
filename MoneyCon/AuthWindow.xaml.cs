using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MoneyCon.ViewModel;
using System;

namespace MoneyCon
{
    public class AuthWindow : Window
    {
        public AuthWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public AuthWindowViewModel AuthWindowViewModel
        {
            get
            {
                return DataContext as AuthWindowViewModel;
            }
        }

        public void OnClose(object sender, RoutedEventArgs args)
        {
            if (AuthWindowViewModel.WebService.CheckCredentials(null, null))
            {
                Close(true);

                AuthWindowViewModel.DialogService.ShowMainWindow();
            }
            else
            {
                
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
