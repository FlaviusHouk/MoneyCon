using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyCon.ViewModel.PagesViewModels
{
    public interface IPageContext
    {
        bool IsFiltered(Cost cost);
    }
}
