using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel.PagesViewModels
{
    class DateSpanFilterContext : BindableBase, IPageContext
    {
        private DateTime _start;
        private DateTime _end;

        public DateTime StartDate
        {
            get
            {
                return _start;
            }
            set
            {
                if (_start != value)
                {
                    _start = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _end;
            }
            set
            {
                if (_end != value)
                {
                    _end = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public bool IsFiltered(Cost cost)
        {
            return cost.PerformedDate > StartDate && cost.PerformedDate < EndDate;
        }
    }
}
