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
                        if ((Price <= 0))
                        {
                            _error = "Цена должна быть больше 0";
                        }
                        break;
                    case nameof(Desc):
                        if (Desc==String.Empty || Desc.Any(obj => Char.IsNumber(obj)))
                        {
                            _error = "Описание не должно содержать цифр или быть пустым";
                        }
                        break;
                    case nameof(Category):
                        if (String.IsNullOrEmpty(Category))
                        {
                            _error = "Выберите категорию";
                        }
                        break;
                    case nameof(Date):
                        if (Date < new DateTime(2000, 1, 1) || Date > DateTime.Now)
                        {
                            _error = "Дата не может быть меньше 1.1.2000 или больше " + DateTime.Now.ToShortDateString();
                        }
                        break;
                }
                RaisePropertyChanged(nameof(Error));
                return _error;
            }
        }

        internal void InsertIntoDB()
        {
            Cost = GetCurrenCost(this);
            Cost.InsertCurrentRecord(_db);
            IsModifed = false;
        }

        public string Error
        {
            get { return _error; }
        }

        public Cost Cost
        {
            get { return _cost; }
            set
            {
                if (_cost == null)
                {
                    _cost = value;
                }
                else { throw new ArgumentException("Cost was initialized"); }
            }
        }

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
                }, () => IsModifed
                ));
            }
        }

        public bool IsModifed
        {
            get { return _isModifed; }
            set
            {
                _isModifed = value;
                SaveCost.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(IsModifed));
                RaisePropertyChanged(nameof(SaveCost));
            }
        }

        public CostViewModel(Cost cost, DataBaseWorker db) : this(db)
        {
            _cost = cost;
            _price = cost.Price;
            _desc = cost.Desc;
            _date = cost.PerformedDate;
            _category = cost.Category;
        }

        public CostViewModel(DataBaseWorker db)
        {
            _db = db;
        }

        private Cost GetCurrenCost(CostViewModel cost)
        {
            return new Cost(Date, Price, Desc, Category);
        }

    }
}
