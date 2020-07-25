using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfFit.Helpers
{
    public class ConditionConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            var avr = (int)value[0];
            var max = (int)value[1];
            var min = (int)value[2];
            var limit = avr * 0.2;
            bool condition = avr - min >= limit || max - avr >= limit; // deviation from the avr by 20%
            return  condition;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
