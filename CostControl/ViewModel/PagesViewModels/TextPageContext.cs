using CostControl.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CostControl.ViewModel.PagesViewModels
{
    class TextPageContext : BasePageContext
    {
        private const string _pageUri = "\\Views\\StatPages\\TextFilterStatPage.xaml";
        private ObservableCollection<CostViewModel> Сosts;
        private ObservableCollection<string> Сats;

        public TextPageContext(ObservableCollection<CostViewModel> costs, ObservableCollection<string> cats, Model.DataBaseWorker db) : base(new Uri(_pageUri, UriKind.Relative), db)
        {
            Сosts = costs;
            Сats = cats;
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
            get
            {
                if (!Сosts.Any())
                {
                    return null;
                }
                return Сosts.Select(o => o.Date).Min().ToShortDateString();
            }
        }

        public string EndDate
        {

            get
            {
                if (!Сosts.Any())
                {
                    return null;
                }
                return Сosts.Select(o => o.Date).Max().ToShortDateString();
            }
        }

        public double Sum
        {
            get
            {
                if (!Сosts.Any())
                {
                    return 0;
                }
                return Сosts.Select(o => o.Price).Sum();
            }
        }

        public double Max
        {
            get
            {
                if (!Сosts.Any())
                {
                    return 0;
                }
                return Сosts.Select(o => o.Price).Max();
            }
        }

        public override ObservableCollection<KeyValuePair<string, double>> GetStatData()
        {
            if (!_typeOfChart.HasValue)
            {
                return null;
            }
            //replace with SQL-interactivity    <----------------------------------
            List<KeyValuePair<string, Double>> coll = new List<KeyValuePair<string, double>>();
            if (_typeOfChart.Value)
            {
                var res = Сosts.Select(o => o.Date).Distinct().OrderBy(o => o);
                res.ForEachCustom(obj => coll.Add(new KeyValuePair<string, double>(obj.ToShortDateString(), Сosts.Where(item => item.Date == obj).Sum(o => o.Price))));
                return new ObservableCollection<KeyValuePair<string, double>>(coll);
            }

            Сats.ForEachCustom(obj => coll.Add(new KeyValuePair<string, double>(obj, Сosts.Where(item => item.Category == obj).Sum(o => o.Price))));
            return new ObservableCollection<KeyValuePair<string, double>>(coll);
        }
    }
}
