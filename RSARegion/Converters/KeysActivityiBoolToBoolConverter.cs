using System;
using System.Globalization;
using System.Windows.Data;

namespace RSARegion.Converters
{
    public class KeysActivityiBoolToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var result = false;
            if (values.Length == 2)
            {
                foreach (var item in values)
                {
                    if (!(item is bool))
                        return false;
                    if ((bool)item) result = true;
                }
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
