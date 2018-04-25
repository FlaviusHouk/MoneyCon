using GalaSoft.MvvmLight;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using CostControl.Helpers;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Data;
using System;
using GalaSoft.MvvmLight.CommandWpf;

namespace CostControl.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region fieilds
        private ObservableCollection<CostViewModel> _costs = new ObservableCollection<CostViewModel>();
        private RelayCommand _clearFilters;
        private RelayCommand _addItem;
        private RelayCommand _removeItem;
        #endregion
        #region properties
        public ObservableCollection<CostViewModel> Costs
        {
            get { return _costs; }
            set { _costs = value; }
        }

        public CollectionView CostsView { get; private set; }

        private CostViewModel _selectedCost;
        public CostViewModel SelectedCost
        {
            get { return _selectedCost; }
            set
            {
                _selectedCost = value;
                RaisePropertyChanged(nameof(SelectedCost));
                RaisePropertyChanged(nameof(HasSelectedCost));
            }
        }

        public bool HasSelectedCost
        {
            get { return _selectedCost!=null; }
        }


        public RelayCommand ClearFilters
        {
            get
            {
                return _clearFilters ?? (_clearFilters = new RelayCommand(() =>
                {
                    _filterText = String.Empty;
                    _aloneDate = DateTime.Now;
                    _startDate = DateTime.Now;
                    _endDate = DateTime.Now;
                    RaisePropertyChanged(nameof(FilterText));
                    RaisePropertyChanged(nameof(AloneDate));
                    RaisePropertyChanged(nameof(StartDate));
                    RaisePropertyChanged(nameof(EndDate));
                    RaisePropertyChanged(nameof(Costs));
                    IsFiltered = false;
                    _flag = false;
                    CostsView.Refresh();
                }));
            }
        }

        public RelayCommand AddItem
        {
            get
            {
                return _addItem ?? (_addItem = new RelayCommand(() =>
                {
                    Costs.Add(new CostViewModel(0, "Расход", DateTime.Now));
                }));
            }
        }

        public RelayCommand RemoveItem
        {
            get
            {
                return _removeItem ?? (_removeItem = new RelayCommand(() =>
                {
                    Costs.Remove(SelectedCost);
                }));
            }
        }

        #endregion
        #region filtersitems
        private string _filterText;
        private DateTime _aloneDate = DateTime.Now.Date;
        private DateTime _startDate = DateTime.Now.Date;
        private DateTime _endDate = DateTime.Now.Date;
        private bool _flag = true;

        int _selectedFilt;
        public int SelectedFilter { get { return _selectedFilt; }
            set
            {
                _selectedFilt = value;
                RaisePropertyChanged(nameof(SelectedFilter));
            }
        }

        private bool _isFiltered = false;
        public bool IsFiltered
        {
            get { return _isFiltered; }
            set
            {
                _isFiltered = value;
                RaisePropertyChanged(nameof(IsFiltered));
            }
        } 

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                RaiseFilters();
            }

        }

        public DateTime AloneDate
        {
            get { return _aloneDate; }
            set
            {
                _aloneDate = value;
                RaiseFilters();
            }

        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaiseFilters();
            }

        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                RaiseFilters();
            }

        }

        public int CountOfItems
        {
            get { return CostsView.Count; }
        }

        #endregion
        #region ctors
        public MainViewModel()
        {
            _costs.Add(new CostViewModel(15, "Хлеб", new System.DateTime(2018,11,30)));
            _costs.Add(new CostViewModel(25, "Молоко", new System.DateTime(2018, 12, 30)));
            _costs.Add(new CostViewModel(20, "Вода", new System.DateTime(2018, 10, 30)));
            CostsView = (CollectionView)CollectionViewSource.GetDefaultView(Costs);
            CostsView.Filter = OnFilterMovie;
        }
        #endregion
        #region methods
        public void RaiseFilters()
        {
            _flag = true;
            CostsView.Refresh();
            RaisePropertyChanged(nameof(CountOfItems));
            RaisePropertyChanged(nameof(Costs));
            
        }
        private bool OnFilterMovie(object obj)
        {
            if (_flag)
            {
                return ApplySelectedCrit(obj as CostViewModel);
            }
            return true;
        }



        private bool ApplySelectedCrit(CostViewModel item)
        {
            IsFiltered = true;
            if (SelectedFilter == 0 && !String.IsNullOrEmpty(FilterText))
            {
                return item.Header.ToLower().Contains(FilterText?.ToLower());
            }
            else if (SelectedFilter == 1)
            {
                return item.Date == AloneDate;
            }
            else if (SelectedFilter ==2)
            {
                return item.Date > StartDate && item.Date < EndDate;
            }
            else
            {
                IsFiltered = false;
                return true;
            }
        }
        #endregion
    }
}