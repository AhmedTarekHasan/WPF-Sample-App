using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WPFRssFeedReader.ValueConverters
{
    public class BoolToVisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility result = Visibility.Visible;

            if(value is bool)
            {
                if(!(bool)value)
                {
                    result = Visibility.Collapsed;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = true;

            if (value is Visibility)
            {
                if((Visibility)value == Visibility.Hidden)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
