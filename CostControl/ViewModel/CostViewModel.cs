using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostControl.ViewModel
{
    public class CostViewModel : ObservableObject
    {
        private int _cost;
        private string _header;
        private DateTime _date;
        private string _tag;
        public int Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
                RaisePropertyChanged(nameof(Cost));
            }
        }

        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                RaisePropertyChanged(nameof(Header));
            }
        }

        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
                RaisePropertyChanged(nameof(Tag));
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(nameof(Date));
            }
        }
        public CostViewModel(int price, string header, DateTime date, string tag)
        {
            _cost = price;
            _header = header;
            _date = date;
            _tag = tag;
        }
    }
}
