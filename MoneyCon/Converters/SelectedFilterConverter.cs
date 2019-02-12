using Avalonia.Controls;
using Avalonia.Markup;
using MoneyCon.ViewModel.PagesViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MoneyCon.Converters
{
    class TabTemplateSelector : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IPageContext cont = value as IPageContext;

            if (value != null)
            {
                if (value is TextFilterContext)
                    return App.CurrentInstance.MainWindow.Resources["TextFilterTemplate"];
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Control control = value as Control;

            return control?.DataContext;
        }
    }
}
