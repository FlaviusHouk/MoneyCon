﻿using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MoneyCon.ViewModel
{
    public class CostViewModel : BindableBase, IDataErrorInfo
    {
        private double _price;
        private string _desc;
        private DateTime _date;
        private string _category;
        private Cost _cost;
        private Command _saveCost;
        private bool _isModifed;
        private string _error;
        private IDatabaseWorker _db;

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
                        if (Desc == String.Empty || Char.IsDigit(Desc.First()) || Char.IsSeparator(Desc.First()))
                        {
                            _error = "Описание не должно начинаться из цифр/символов или быть пустым";
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
                OnPropertyChanged(nameof(Error));
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
                OnPropertyChanged(nameof(Price));
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
                OnPropertyChanged(nameof(Desc));
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
                OnPropertyChanged(nameof(Category));
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                IsModifed = true;
                OnPropertyChanged(nameof(Date));
            }
        }

        public Command SaveCost
        {
            get
            {
                return _saveCost ?? (_saveCost = new Command(par =>
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
                SaveCost.CanExecuteFlag = value;
                OnPropertyChanged(nameof(IsModifed));
                OnPropertyChanged(nameof(SaveCost));
            }
        }

        public CostViewModel(Cost cost, IDatabaseWorker db) : this(db)
        {
            _cost = cost;
            _price = cost.Price;
            _desc = cost.Desc;
            _date = cost.PerformedDate;
            _category = cost.Category;
        }

        public CostViewModel(IDatabaseWorker db)
        {
            _db = db;
        }

        private Cost GetCurrenCost(CostViewModel cost)
        {
            return new Cost(Date, Price, Desc, Category);
        }

    }
}
