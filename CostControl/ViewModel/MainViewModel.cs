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
using CostControl.Model;

namespace CostControl.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region fieilds
        private ObservableCollection<CostViewModel> _costs = new ObservableCollection<CostViewModel>();
        private ObservableCollection<string> _tags = new ObservableCollection<string>();
        private RelayCommand _clearFiltersCommand;
        private RelayCommand _addItemCommand;
        private RelayCommand _removeItemCommand;
        private RelayCommand _addTagCommand;
        private RelayCommand _saveitemsCommand;

        private DataBaseWorker _db;
        #endregion
        #region properties
        public ObservableCollection<CostViewModel> Costs
        {
            get { return _costs; }
            set { _costs = value; }
        }

        public ObservableCollection<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
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
            get { return _selectedCost != null; }
        }


        public RelayCommand ClearFiltersCommand
        {
            get
            {
                return _clearFiltersCommand ?? (_clearFiltersCommand = new RelayCommand(() =>
                {
                    ClearAllFilters();
                }, () => IsFiltersEnabled              
                ));
            }
        }

        public RelayCommand AddItemCommand
        {
            get
            {
                return _addItemCommand ?? (_addItemCommand = new RelayCommand(() =>
                {
                    Costs.Add(new CostViewModel(0, "Расход", DateTime.Now, null));
                }));
            }
        }


        public RelayCommand AddTagCommand
        {
            get
            {
                return _addTagCommand ?? (_addTagCommand = new RelayCommand(() =>
                {
                    Tags.Add("Tag1");
                }));
            }
        }

        
        public RelayCommand SaveItemsCommand
        {
            get
            {
                return _saveitemsCommand ?? (_saveitemsCommand = new RelayCommand(() =>
                {
                    Costs.Where(obj => obj.IsModifed).ForEach(item => item.IsModifed = true);
                }, () => Costs.Any(obj => obj.IsModifed)
                ));
            }
        }

        public RelayCommand RemoveItemCommand
        {
            get
            {
                return _removeItemCommand ?? (_removeItemCommand = new RelayCommand(() =>
                {
                    Costs.Remove(SelectedCost);
                }, () => HasSelectedCost));
            }
        }

        #endregion
        #region filtersitems
        private string _filterText;
        private DateTime _aloneDate = DateTime.Now.Date;
        private DateTime _startDate = DateTime.Now.Date;
        private DateTime _endDate = DateTime.Now.Date;
        private string _tag;

        private bool _isFiltersEnabled = false;
        public bool IsFiltersEnabled
        {
            get { return _isFiltersEnabled; }
            set
            {
                _isFiltersEnabled = value;
                ClearAllFilters();
                SelectedFilter = 0;
                RaisePropertyChanged(nameof(IsFiltersEnabled));
            }
        }

        int _selectedFilt;
        public int SelectedFilter
        {
            get { return _selectedFilt; }
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

        public string SelectedTag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                RaiseFilters();
            }
        }

        #endregion
        #region ctors
        public MainViewModel()
        {
            _db = new DataBaseWorker();
            

            _costs.Add(new CostViewModel(15, "Хлеб", new System.DateTime(2018, 11, 30), "Еда"));
            _costs.Add(new CostViewModel(25, "Молоко", new System.DateTime(2018, 12, 30), "Еда"));
            _costs.Add(new CostViewModel(20, "Вода", new System.DateTime(2018, 10, 30), "Развлечения"));
            _tags.Add("Еда");
            _tags.Add("Развлечения");
            CostsView = (CollectionView)CollectionViewSource.GetDefaultView(Costs);
            CostsView.Filter = OnFilterMovie;
        }
        #endregion
        #region methods
        public void RaiseFilters()
        {
            CostsView.Refresh();
            RaisePropertyChanged(nameof(CountOfItems));
            RaisePropertyChanged(nameof(Costs));

        }
        private bool OnFilterMovie(object obj)
        {
            if (IsFiltersEnabled)
            {
                return ApplySelectedCrit(obj as CostViewModel);
            }
            return true;
        }

        private void ClearAllFilters()
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
            CostsView.Refresh();
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
            else if (SelectedFilter == 2)
            {
                return item.Date > StartDate && item.Date < EndDate;
            }
            else if (SelectedFilter == 3)
            {
                return String.Compare(item.Tag, SelectedTag) == 0;
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