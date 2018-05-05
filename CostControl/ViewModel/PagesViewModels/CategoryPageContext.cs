﻿using CostControl.Helpers;
using CostControl.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CostControl.ViewModel.PagesViewModels
{

    public class CategoryPageContext : BasePageContext
    {
        private const string _pageUri = "\\Views\\StatPages\\CategoryFilterStatPage.xaml";
        public ObservableCollection<CostViewModel> Costs;

        public CategoryPageContext(ObservableCollection<CostViewModel> costs, DataBaseWorker db) : base(new System.Uri(_pageUri, UriKind.Relative), db)
        {
            Costs = costs;
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

        public string StartDate
        {
            get
            {
                if (!Costs.Any())
                {
                    return null;
                }
                return Costs.Select(o => o.Date).Min().ToShortDateString();
            }
        }

        public string EndDate
        {
            get
            {
                if (!Costs.Any())
                {
                    return null;
                }
                return Costs.Select(o => o.Date).Max().ToShortDateString();
            }
        }

        public double Sum
        {
            get
            {
                if (!Costs.Any())
                {
                    return 0;
                }
                return Costs.Select(o => o.Price).Sum();
            }
        }

        public double Avg
        {
            get
            {
                if (!Costs.Any())
                {
                    return 0;
                }
                return _db.GetAvgByCategory(Costs.First().Category);
            }
        }

        public double Max
        {
            get
            {
                if (!Costs.Any())
                {
                    return 0;
                }
                return Costs.Select(o => o.Price).Max();
            }
        }

        public override ObservableCollection<KeyValuePair<string, double>> GetStatData()
        {
            List<KeyValuePair<string, double>> lis = new List<KeyValuePair<string, double>>();
            var res = Costs.Select(o => o.Date).Distinct().OrderBy(o => o);
            res.ForEachCustom(obj => lis.Add(new KeyValuePair<string, double>(obj.ToShortDateString(), _db.GetSumByDate(obj))));
            return new ObservableCollection<KeyValuePair<string, double>>(lis);
        }
    }
}