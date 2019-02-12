using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel
{
    public class AddCostViewModel : BindableBase
    {
        public CostViewModel Cost
        {
            get;
        }

        public AddCostViewModel(IDatabaseWorker db)
        {
            Cost = new CostViewModel(db) { Desc = string.Empty, Date = DateTime.Now };
        }
    }
}
