using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel.Infrastructure
{
    public class PropertyChangedEventArgs : EventArgs
    {
        public string PropertyName { get; }

        public PropertyChangedEventArgs(string propName)
        {
            PropertyName = propName;
        }
    }

    public delegate void PropertyChangedEventHandler(BindableBase sender, PropertyChangedEventArgs e);

    public class BindableBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propName);
                PropertyChanged.Invoke(this, args);
            }
        }
    }
}
