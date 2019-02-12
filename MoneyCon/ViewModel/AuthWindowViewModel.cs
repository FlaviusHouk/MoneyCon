using MoneyCon.ViewModel.Infrastructure;
using MoneyCon.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel
{
    public class AuthWindowViewModel : BindableBase
    {
        public IWebService WebService { get; }
        public IDialogService DialogService { get; }

        public AuthWindowViewModel(IWebService service, IDialogService dialogService)
        {
            WebService = service;
            DialogService = dialogService;
        }
    }
}
