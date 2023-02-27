using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CryptoApp.Converters
{
    class NumberToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isParsed = decimal.TryParse(value.ToString(), out decimal res);
            if(isParsed)
            {
                return res switch
                {
                    < 0 => new SolidColorBrush(Colors.IndianRed),
                    > 0 => new SolidColorBrush(Colors.ForestGreen),
                    _ => new SolidColorBrush(Colors.White)
                };
            }

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
