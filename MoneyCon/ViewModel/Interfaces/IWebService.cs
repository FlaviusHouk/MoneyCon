using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel.Interfaces
{
    public interface IWebService
    {
        bool CheckCredentials(string login, string pass);
    }
}
