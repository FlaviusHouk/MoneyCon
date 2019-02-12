using MoneyCon.Services;
using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using MoneyCon.ViewModel.Interfaces;
using MoneyCon.ViewModel.PagesViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MoneyCon.ViewModel
{
    public enum enumFilterTypes
    {
        NoFilter,
        DescFilter,
        DateFilter,
        DateSpanFilter,
        CategoryFilter
    }

    public class MainViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<CostViewModel> _costs = new ObservableCollection<CostViewModel>();
        private ObservableCollection<string> _tags = new ObservableCollection<string>();
        private Command _clearFiltersCommand;
        private Command _addItemCommand;
        private Command _removeItemCommand;
        private Command _addTagCommand;
        private Command _useFilterCommand;
        private Dictionary<int, IPageContext> _pageViewModels = new Dictionary<int, IPageContext>();

        private IDatabaseWorker _db;
        public IDatabaseWorker DB { get { return _db; } }
        public IWebService WebService { get; }
        public IDialogService DialogService { get; }
        #endregion

        public MainViewModel()
        {
            WebService = new WebService();
            DialogService = new DialogService();
        }

        #region Properties
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

                foreach (var v in _db.Categories)
                    _tags.Add(v.Value);

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
                OnPropertyChanged(nameof(SelectedCost));
                OnPropertyChanged(nameof(HasSelectedCost));
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
        #endregion

        #region Commands
        public Command ClearFiltersCommand
        {
            get
            {
                return _clearFiltersCommand ?? (_clearFiltersCommand = new Command(par =>
                {
                    ClearAllFilters();
                    //OnPropertyChanged(nameof(ActiveStat));
                }));
            }
        }

        public Command AddItemCommand
        {
            get
            {
                return _addItemCommand ?? (_addItemCommand = new Command(par =>
                {
                    /*var vm = new AddCostViewModel(_db);
                    var win = new AddCostWindow() { Owner = App.Current.MainWindow, DataContext = vm };
                    if (win.ShowDialog() == true)
                    {
                        Costs.Add(vm.Cost);
                    }
                    OnPropertyChanged(nameof(ActiveStat));*/

                }));
            }
        }
        public Command UseFilterCommand
        {
            get
            {
                return _useFilterCommand ?? (_useFilterCommand = new Command(par =>
                {
                    Costs.Clear();
                    IEnumerable<Cost> res = _db.GetRecords();

                    if (res != null)
                    {
                        if (SelectedFilter != null)
                        res = res.Where(cost => SelectedFilter.IsFiltered(cost));
            
                        foreach (var v in res)
                        {
                            Costs.Add(new CostViewModel(v, _db));
                        }
                    }

                    IsFiltered = true;
                    OnPropertyChanged(nameof(CountOfItems));
                    OnPropertyChanged(nameof(HasItems));
                    //OnPropertyChanged(nameof(ActiveStat));
                }));
            }
        }

        public Command AddTagCommand
        {
            get
            {
                return _addTagCommand ?? (_addTagCommand = new Command(par =>
                {
                    /*SelectedCost = null;
                    var w = new Views.CategoriesWindow { Owner = App.Current.MainWindow };
                    if (w.ShowDialog() == true)
                    {
                        System.Diagnostics.Debug.WriteLine("Категории сохранены");
                    }
                    OnPropertyChanged(nameof(ActiveStat));*/
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
                OnPropertyChanged(nameof(TypeOfChart));
            }
        }

        /*private Page _activeStat;
        public Page ActiveStat
        {
            get
            {
                _activeStat = Application.LoadComponent(_pageViewModels[SelectedFilter].PageUri) as Page;
                _activeStat.DataContext = _pageViewModels[SelectedFilter];
                _pageViewModels[SelectedFilter]?.Update();
                return _activeStat;
            }
        }*/

        public Command RemoveItemCommand
        {
            get
            {
                return _removeItemCommand ?? (_removeItemCommand = new Command(par =>
                {
                    _db.DeleteCost(SelectedCost.Cost);
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
        private string _tag;

        private TextFilterContext _textFilterContext;
        public TextFilterContext TextFilterContext
        {
            get
            {
                return _textFilterContext ?? (_textFilterContext = new TextFilterContext());
            }
        }

        private IPageContext _selectedFilter;
        public IPageContext SelectedFilter
        {
            get
            {
                return IsFiltersEnabled ? _selectedFilter : null;
            }
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OnPropertyChanged(nameof(SelectedFilter));
                }
            }
        }

        private bool _isFiltersEnabled = false;
        public bool IsFiltersEnabled
        {
            get { return _isFiltersEnabled; }
            set
            {
                _isFiltersEnabled = value;
                /*if (!value)
                {
                    ClearAllFilters();
                    SelectedFilter = 2;
                }*/
                OnPropertyChanged(nameof(IsFiltersEnabled));
                //OnPropertyChanged(nameof(ActiveStat));
                //OnPropertyChanged(nameof(HasItems));
            }
        }

        private bool _isFiltered = false;
        public bool IsFiltered
        {
            get { return _isFiltered; }
            set
            {
                _isFiltered = value;
                OnPropertyChanged(nameof(IsFiltered));
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

        #region methods

        private void ClearAllFilters()
        {
            _filterText = String.Empty;
            _aloneDate = DateTime.Now;
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;
            OnPropertyChanged(nameof(FilterText));
            OnPropertyChanged(nameof(AloneDate));
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(EndDate));
            OnPropertyChanged(nameof(Costs));
            IsFiltered = false;
            InitializeStartCosts();

        }

        private void InitializeStartCosts()
        {
            Costs.Clear();
            var firstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            var lastday = firstday.AddMonths(1).AddDays(1);
            var coll = _db.GetRecordsByDateSpan(firstday, lastday).Select(obj => new CostViewModel(obj, _db));

            foreach (var v in coll)
            {
                Costs.Add(v);
            }
        }

        #endregion
    }
}
