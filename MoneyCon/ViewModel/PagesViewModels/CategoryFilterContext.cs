using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel.PagesViewModels
{
    class CategoryFilterContext : BindableBase, IPageContext
    {
        

        public bool IsFiltered(Cost cost)
        {
            throw new NotImplementedException();
        }
    }
}
