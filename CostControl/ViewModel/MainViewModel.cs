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
using System.Collections;
using CostControl.Views;
using System.Windows.Controls;
using CostControl.ViewModel.PagesViewModels;
using System.Windows;

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
        private RelayCommand _useFilterCommand;
        private Dictionary<int, BasePageContext> _pageViewModels = new Dictionary<int, BasePageContext>();

        private DataBaseWorker _db;
        public DataBaseWorker DB { get { return _db; } }
        #endregion
        #region properties
        public ObservableCollection<CostViewModel> Costs
        {
            get { return _costs; }
            set { _costs = value; }
        }

        public ObservableCollection<string> Tags
        {
            get
            {
                _tags.Clear();
                _db.Categories.ForEachCustom(obj => _tags.Add(obj));
                return _tags;
            }
        }

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

        public bool HasItems
        {
            get { return Costs.Count == 0; }
        }


        public RelayCommand ClearFiltersCommand
        {
            get
            {
                return _clearFiltersCommand ?? (_clearFiltersCommand = new RelayCommand(() =>
                {
                    ClearAllFilters();
                    RaisePropertyChanged(nameof(ActiveStat));
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
                    var vm = new AddCostViewModel(_db);
                    var win = new AddCostWindow() { Owner = App.Current.MainWindow, DataContext = vm };
                    if (win.ShowDialog() == true)
                    {
                        Costs.Add(vm.Cost);
                    }
                    RaisePropertyChanged(nameof(ActiveStat));

                }));
            }
        }
        public RelayCommand UseFilterCommand
        {
            get
            {
                return _useFilterCommand ?? (_useFilterCommand = new RelayCommand(() =>
                {
                    Costs.Clear();
                    IEnumerable<Cost> res = null;
                    if (SelectedFilter == 0 && !string.IsNullOrEmpty(_filterText))
                    {
                        res = _db.GetRecordsByDescription(_filterText);
                    }
                    else if (SelectedFilter == 1)
                    {
                        res = _db.GetRecordsByDate(_aloneDate);
                    }
                    else if (SelectedFilter == 2)
                    {
                        res = _db.GetRecordsByDateSpan(_startDate, _endDate);
                    }
                    else if (SelectedFilter == 3)
                    {
                        res = _db.GetRecordsByCategory(_tag);
                    }
                    res?.ForEachCustom(obj => Costs.Add(new CostViewModel(obj, _db)));
                    IsFiltered = true;
                    RaisePropertyChanged(nameof(CountOfItems));
                    RaisePropertyChanged(nameof(HasItems));
                    RaisePropertyChanged(nameof(ActiveStat));
                },() =>
                {
                    if (SelectedFilter==3 && string.IsNullOrEmpty(SelectedTag))
                    {
                        return false;
                    }
                    return true;
                }
                ));
            }
        }

        public RelayCommand AddTagCommand
        {
            get
            {
                return _addTagCommand ?? (_addTagCommand = new RelayCommand(() =>
                {
                    var w = new Views.CategoriesWindow {Owner = App.Current.MainWindow };
                    if (w.ShowDialog() == true)
                    {
                        System.Diagnostics.Debug.WriteLine("Категории сохранены");
                    }
                    RaisePropertyChanged(nameof(ActiveStat));
                }));
            }
        }

       private bool? _typeOfChart = null;
       public bool? TypeOfChart
        {
            get { return _typeOfChart; }
            set
            {
                _typeOfChart = value;
                RaisePropertyChanged(nameof(TypeOfChart));
            }
        }

        private Page _activeStat;
        public Page ActiveStat
        {
            get
            {
                _activeStat = Application.LoadComponent(_pageViewModels[SelectedFilter].PageUri) as Page;
                _activeStat.DataContext = _pageViewModels[SelectedFilter];
                _pageViewModels[SelectedFilter]?.Update();
                return _activeStat;
            }
        }

        public RelayCommand RemoveItemCommand
        {
            get
            {
                return _removeItemCommand ?? (_removeItemCommand = new RelayCommand(() =>
                {
                    _db.DeleteCost(SelectedCost.Cost);
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
                if (!value)
                {
                    ClearAllFilters();
                    SelectedFilter = 2;
                }
                RaisePropertyChanged(nameof(IsFiltersEnabled));
                RaisePropertyChanged(nameof(ActiveStat));
                RaisePropertyChanged(nameof(HasItems));
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
            }

        }

        public DateTime AloneDate
        {
            get { return _aloneDate; }
            set
            {
                _aloneDate = value;
            }

        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
            }

        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
            }

        }

        public int CountOfItems
        {
            get { return Costs.Count; }
        }

        public string SelectedTag
        {
            get { return _tag; }
            set
            {
                _tag = value;
            }
        }

        #endregion
        #region ctors
        public MainViewModel()
        {
            _db = new DataBaseWorker();
            InitializeStartCosts();
            _pageViewModels.Add(0, new TextPageContext(Costs, Tags, _db));
            _pageViewModels.Add(1, new DatePageContext(Costs, Tags, _db));
            _pageViewModels.Add(2, new DateSpanPageContext(Costs,Tags, _db));
            _pageViewModels.Add(3, new CategoryPageContext(Costs, _db));
            SelectedFilter = 2;
        }
        #endregion
        #region methods
    
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
            InitializeStartCosts();
            
        }

        private void InitializeStartCosts()
        {
            Costs.Clear();
            var firstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            var lastday = firstday.AddMonths(1).AddDays(1);
            var coll = _db.GetRecordsByDateSpan(firstday, lastday).Select(obj => new CostViewModel(obj, _db));
            coll.ForEachCustom(obj => Costs.Add(obj));
            
        }

        #endregion
    }
}