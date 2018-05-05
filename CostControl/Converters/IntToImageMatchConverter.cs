using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CostControl.Converters
{
    class IntToImageMatchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nores = App.Current.FindResource("NoFoundImage") as BitmapImage;
            var res = App.Current.FindResource("FoundImage") as BitmapImage;

            if ((int)value > 0)
            {
                return res;
            }
            return nores;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
