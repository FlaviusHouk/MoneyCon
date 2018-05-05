using CostControl.Model;
using GalaSoft.MvvmLight;
using System;

namespace CostControl.ViewModel
{
    public class AddCostViewModel : ViewModelBase
    {
        public CostViewModel Cost
        {
            get;
        }
        public AddCostViewModel(DataBaseWorker db)
        {
            Cost = new CostViewModel(db) { Desc = string.Empty, Date = DateTime.Now};
        }
    }
}