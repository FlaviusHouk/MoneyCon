using CostControl.Model;
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
        private double _price;
        private string _desc;
        private DateTime _date;
        private string _category;
        private Cost _cost;
        private RelayCommand _saveCost;
        private bool _isModifed;
        private string _error;
        private DataBaseWorker _db;

        public string this[string columnName]
        {
            get
            {
                _error = String.Empty;
                switch (columnName)
                {
                    case nameof(Price):
                        if ((Price < 0))
                        {
                            _error = "Цена должна быть больше 0";
                        }
                        break;
                    case nameof(Desc):
                        if (Desc.Any(obj => Char.IsNumber(obj)))
                        {
                            _error = "Имя не должно содержать символов/цифер";
                        }
                        break;
                    case nameof(Category):
                        if (String.IsNullOrEmpty(Category))
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

        public Cost Cost { get { return _cost; } }

        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                IsModifed = true;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public string Desc
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
                IsModifed = true;
                RaisePropertyChanged(nameof(Desc));
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                IsModifed = true;
                RaisePropertyChanged(nameof(Category));
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
                    _db.UpdateRecord(_cost, GetCurrenCost(this));
                    IsModifed = false;
                }));
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

        public CostViewModel(Cost cost, DataBaseWorker db)
        {
            _db = db;
            _cost = cost;
            _price = cost.Price;
            _desc = cost.Desc;
            _date = cost.PerformedDate;
            _category = cost.Category;
        }

        private Cost GetCurrenCost(CostViewModel cost)
        {
            return new Cost(Date, Price, Desc, Category);
        }
    }
}
