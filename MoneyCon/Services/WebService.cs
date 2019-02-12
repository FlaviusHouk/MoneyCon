using MoneyCon.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.Services
{
    public class WebService : IWebService
    {
        public bool CheckCredentials(string login, string pass)
        {
            return true;
        }
    }
}
