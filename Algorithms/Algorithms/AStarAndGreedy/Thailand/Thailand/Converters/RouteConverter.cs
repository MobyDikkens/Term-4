using System;
using System.Globalization;
using System.Windows.Data;

namespace Thailand.Converters
{
    public class RoutesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string from = (string)values[0];
            string to = (string)values[1];

            return (object)(from + "," + to);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
