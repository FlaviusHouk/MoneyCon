using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        private RelayCommand _saveCost;
        private bool _isModifed;

        public int Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
                IsModifed = true;
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
                IsModifed = true;
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
                IsModifed = true;
                RaisePropertyChanged(nameof(Tag));
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                IsModifed = true;
                RaisePropertyChanged(nameof(Date));
            }
        }

        public RelayCommand SaveCost
        {
            get
            {
                return _saveCost ?? (_saveCost = new RelayCommand(() =>
                {
                    IsModifed = false;
                }, ()=> IsModifed ));
            }
        }

        public bool IsModifed
        {
            get { return _isModifed; }
            set
            {
                _isModifed = value;
                RaisePropertyChanged(nameof(IsModifed));
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
