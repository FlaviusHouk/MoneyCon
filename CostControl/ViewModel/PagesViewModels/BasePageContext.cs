using CostControl.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CostControl.ViewModel
{
    public abstract class BasePageContext : ObservableObject
    {
        #region Fields
        private readonly Uri _pageUri;
        protected DataBaseWorker _db;
        #endregion

        #region Properties
        public Uri PageUri { get { return _pageUri; } }

        public ObservableCollection<KeyValuePair<string, double>> DataCharts
        {
            get
            {
                return GetStatData();
            }
        }
        #endregion

        #region Constructors

        protected BasePageContext(Uri pageUri, DataBaseWorker db)
        {
            _pageUri = pageUri;
            _db = db;
        }

        public virtual void Update()
        {
            RaisePropertyChanged(nameof(DataCharts));
        }

        public virtual ObservableCollection<KeyValuePair<string, double>> GetStatData()
        {
            // replace overrided methods with SQL-interactivity    <----------------------------------
            return null;
        }

        #endregion
    }
}