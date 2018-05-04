﻿using System.Collections.ObjectModel;
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


        public DatePageContext(ObservableCollection<CostViewModel> costs, ObservableCollection<string> cats) : base(new Uri(_pageUri, UriKind.Relative))
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
            get { return Сosts.Select(o => o.Date).First().ToShortDateString(); }
        }

        public double Sum
        {
            get { return Сosts.Select(o => o.Price).Sum(); }
        }

        public double Max
        {
            get { return Сosts.Select(o => o.Price).Max(); }
        }

        public override ObservableCollection<KeyValuePair<string, double>> GetStatData()
        {
            List<KeyValuePair<string, double>> coll = new List<KeyValuePair<string, double>>();
            Сats.ForEachCustom(obj => coll.Add(new KeyValuePair<string, double>(obj, Сosts.Where(item => item.Category == obj).Sum(o => o.Price))));
            return new ObservableCollection<KeyValuePair<string, double>>(coll);
        }
    }
}