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
            }
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
                    CostsView.Refresh();
                }));
            }
        }

        #endregion
        #region filters

        private string _filterText;
        private DateTime _aloneDate = DateTime.Now;
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;

        int _selectedFilt;
        public int SelectedFilter { get { return _selectedFilt; }
            set
            {
                _selectedFilt = value;
                RaisePropertyChanged(nameof(SelectedFilter));
            }
        }

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                CostsView.Refresh();
                RaisePropertyChanged(nameof(Costs));
            }

        }

        public DateTime AloneDate
        {
            get { return _aloneDate; }
            set
            {
                _aloneDate = value;
                CostsView.Refresh();
                RaisePropertyChanged(nameof(Costs));
            }

        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                CostsView.Refresh();
                RaisePropertyChanged(nameof(Costs));
            }

        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                RaisePropertyChanged(nameof(Costs));
            }

        }

        #endregion

        public MainViewModel()
        {
            _costs.Add(new CostViewModel(15, "Хлеб", new System.DateTime(2018,11,30)));
            _costs.Add(new CostViewModel(25, "Молоко", new System.DateTime(2018, 12, 30)));
            _costs.Add(new CostViewModel(20, "Вода", new System.DateTime(2018, 10, 30)));
            CostsView = (CollectionView)CollectionViewSource.GetDefaultView(Costs);
            CostsView.Filter = OnFilterMovie;
        }

        private bool OnFilterMovie(object obj)
        {
            var item = obj as CostViewModel;
            return ApplySelectedCrit(item);
        }

        private bool ApplySelectedCrit(CostViewModel item)
        {
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
                return true;
            }
        }
    }
}