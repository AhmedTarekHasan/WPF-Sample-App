using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using WPFRssFeedReader.Validation;

namespace WPFRssFeedReader.ValueConverters
{
    public class ValidationObjectSeverityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Brush result = new SolidColorBrush(Colors.Black);

            if(value is ValidationObjectSeverity)
            {
                switch((ValidationObjectSeverity)value)
                {
                    case ValidationObjectSeverity.Default:
                        result = new SolidColorBrush(Colors.Black);
                        break;
                    case ValidationObjectSeverity.Fatal:
                        result = new SolidColorBrush(Colors.Red);
                        break;
                    case ValidationObjectSeverity.Major:
                        result = new SolidColorBrush(Colors.DarkOrange);
                        break;
                    case ValidationObjectSeverity.Minor:
                        result = new SolidColorBrush(Colors.Purple);
                        break;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ValidationObjectSeverity result = ValidationObjectSeverity.Default;

            if (value is SolidColorBrush)
            {
                if(((SolidColorBrush)value).Color == Colors.Black)
                {
                    result = ValidationObjectSeverity.Default;
                }
                else if(((SolidColorBrush)value).Color == Colors.Red)
                {
                    result = ValidationObjectSeverity.Fatal;
                }
                else if (((SolidColorBrush)value).Color == Colors.DarkOrange)
                {
                    result = ValidationObjectSeverity.Major;
                }
                else if (((SolidColorBrush)value).Color == Colors.Purple)
                {
                    result = ValidationObjectSeverity.Minor;
                }
            }

            return result; 
        }
    }
}
