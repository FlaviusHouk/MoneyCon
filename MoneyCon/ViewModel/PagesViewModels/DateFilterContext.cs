using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel.PagesViewModels
{
    class DateFilterContext : BindableBase, IPageContext
    {
        private DateTime _dateTime = DateTime.Now;

        public DateTime Date
        {
            get
            {
                return _dateTime;
            }
            set
            {
                if (value != _dateTime)
                {
                    _dateTime = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public bool IsFiltered(Cost cost)
        {
            return Date != null ? cost.PerformedDate == Date : false;
        }
    }
}
