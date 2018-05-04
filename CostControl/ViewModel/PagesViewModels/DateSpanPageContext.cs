using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using CostControl.Helpers;

namespace CostControl.ViewModel.PagesViewModels
{
    public class DateSpanPageContext : BasePageContext
    {
        private const string _pageUri = "\\Views\\StatPages\\DateSpanFilterStatPage.xaml";
        private ObservableCollection<CostViewModel> Costs;
        private ObservableCollection<string> Cats;

        public DateSpanPageContext(ObservableCollection<CostViewModel> costs, ObservableCollection<string> cats) : base(new Uri(_pageUri, UriKind.Relative))
        {
            Costs = costs;
            Cats = cats;
        }

        private bool? _typeOfChart;
        public bool? TypeOfChart
        {
            get { return _typeOfChart; }
            set
            {
                _typeOfChart = value;
                RaisePropertyChanged(nameof(TypeOfChart));
            }
        }

        public string StartDate
        {
            get { return Costs.Select(o => o.Date).Min().ToShortDateString(); }
        }

        public string EndDate
        {
            get { return Costs.Select(o => o.Date).Max().ToShortDateString(); }
        }

        public double Sum
        {
            get { return Costs.Select(o => o.Price).Sum(); }
        }

        public double Max
        {
            get { return Costs.Select(o => o.Price).Max(); }
        }

        public override ObservableCollection<KeyValuePair<string, double>> GetStatData()
        {
            if (!_typeOfChart.HasValue)
            {
                return null;
            }
            List<KeyValuePair<string, Double>> coll = new List<KeyValuePair<string, double>>();
            if (_typeOfChart.Value)
            {
                var res = Costs.Select(o => o.Date).Distinct().OrderBy(o => o);
                res.ForEachCustom(obj => coll.Add(new KeyValuePair<string, double>(obj.ToShortDateString(), Costs.Where(item => item.Date == obj).Sum(o => o.Price))));
                return new ObservableCollection<KeyValuePair<string, double>>(coll);
            }
            Cats.ForEachCustom(obj => coll.Add(new KeyValuePair<string, double>(obj, Costs.Where(item => item.Category == obj).Sum(o => o.Price))));
            return new ObservableCollection<KeyValuePair<string, double>>(coll);
        }
    }
}