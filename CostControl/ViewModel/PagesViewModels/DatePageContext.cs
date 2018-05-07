using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using CostControl.Helpers;

namespace CostControl.ViewModel.PagesViewModels
{

    public class DatePageContext : BasePageContext
    {
        private const string _pageUri = "\\Views\\StatPages\\DateFilterStatPage.xaml";
        private ObservableCollection<CostViewModel> Сosts;
        private ObservableCollection<string> Сats;


        public DatePageContext(ObservableCollection<CostViewModel> costs, ObservableCollection<string> cats, Model.DataBaseWorker db) : base(new Uri(_pageUri, UriKind.Relative), db)
        {
            Сosts = costs;
            Сats = cats;
        }

        private bool _typeOfChart;
        public bool TypeOfChart
        {
            get { return _typeOfChart; }
            set
            {
                _typeOfChart = value;
                RaisePropertyChanged(nameof(TypeOfChart));
            }
        }

        public string Date
        {
            get
            {
                if (!Сosts.Any())
                {
                    return null;
                }
                return Сosts.Select(o => o.Date).First().ToShortDateString();
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
                return _db.GetSumByDate(Сosts.First().Date); }
        }

        public double Avg
        {
            get
            {
                if (!Сosts.Any())
                {
                    return 0;
                }
                return _db.GetAvgByDate(Сosts.First().Date);
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
            List<KeyValuePair<string, double>> coll = new List<KeyValuePair<string, double>>();
            Сats.ForEachCustom(obj => coll.Add(new KeyValuePair<string, double>(obj, Сosts.Where(o => string.Compare(o.Category, obj) == 0).Sum(o => o.Price))));
            return new ObservableCollection<KeyValuePair<string, double>>(coll);
        }
    }
}