using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel.Interfaces
{
    public enum enumMoneyConDialogs
    {

    }

    public interface IDialogService
    {
        bool ShowDialog(enumMoneyConDialogs toShow);
        void ShowMainWindow();
    }
}
