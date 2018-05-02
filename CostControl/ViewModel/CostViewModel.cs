using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostControl.ViewModel
{
    public class CostViewModel : ObservableObject, IDataErrorInfo
    {
        private int _cost;
        private string _header;
        private DateTime _date;
        private string _tag;
        private RelayCommand _saveCost;
        private bool _isModifed;
        private string _error;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Cost):
                        if ((Cost < 0))
                        {
                            _error = "Цена должна быть больше 0";
                        }
                        break;
                    case nameof(Header):
                        if (Header.Any(obj => Char.IsSeparator(obj) || Char.IsNumber(obj)))
                        {
                            _error = "Имя не должно содержать символов/цифер";
                        }
                        break;
                    case nameof(Tag):
                        if (String.IsNullOrEmpty(Tag))
                        {
                            _error = "Выберите категорию";
                        }
                        break;
                    case nameof(Date):
                        if (Date < new DateTime(2000, 1, 1) || Date > DateTime.Now.AddDays(1))
                        {
                            _error = "Дата не может быть меньше 1.1.2000 или больше завтрашнего дня";
                        }
                        break;
                }
                return _error;
            }
        }
        public string Error
        {
            get { return _error; }
        }
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
                }, () =>
                {
                    if (!IsModifed || String.IsNullOrEmpty(_error))
                    {
                        return false;
                    }

                    return true;
                }

                ));
            }
        }

        public bool IsModifed
        {
            get { return _isModifed; }
            set
            {
                _isModifed = value;
                RaisePropertyChanged(nameof(IsModifed));
                RaisePropertyChanged(nameof(SaveCost));
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
