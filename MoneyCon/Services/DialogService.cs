using MoneyCon.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.Services
{
    public class DialogService : IDialogService
    {
        public bool ShowDialog(enumMoneyConDialogs toShow)
        {
            switch (toShow)
            {
                default:
                    return false;
            }
        }

        public void ShowMainWindow()
        {
            if(!App.CurrentInstance.MainWindow.IsVisible)
                App.CurrentInstance.MainWindow.Show();
        }
    }
}
