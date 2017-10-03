using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFRssFeedReader.ValueConverters
{
    public class RelationalOperatorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = true;
            Double valueAsDouble;

            if(value != null && value is IComparable && Double.TryParse(value.ToString(), out valueAsDouble) && parameter != null && parameter is string && parameter.ToString().Contains(','))
            {
                string[] parts = parameter.ToString().Split(',');

                if (parts.Length > 1)
                {
                    string relationalOperatorDescription = parts[0].Trim().ToLower();
                    Double firstOperand = Double.Parse(parts[1].Trim());
                    Double secondOperand = default(Double);

                    if (parts.Length > 2)
                    {
                        secondOperand = Double.Parse(parts[2].Trim());
                    }

                    switch (relationalOperatorDescription)
                    {
                        case "greater than":
                            //>
                            result = (valueAsDouble > firstOperand);
                            break;
                        case "less than":
                            //<
                            result = (valueAsDouble < firstOperand);
                            break;
                        case "greater than or equal":
                            //>=
                            result = (valueAsDouble >= firstOperand);
                            break;
                        case "less than or equal":
                            //<=
                            result = (valueAsDouble <= firstOperand);
                            break;
                        case "equal":
                            //==
                            result = (valueAsDouble == firstOperand);
                            break;
                        case "not equal":
                            //!=
                            result = (valueAsDouble != firstOperand);
                            break;
                        case "between1":
                            // x < y < z
                            if (parts.Length > 2)
                            {
                                result = (firstOperand < valueAsDouble && valueAsDouble < secondOperand);
                            }
                            break;
                        case "between2":
                            // x <= y <= z
                            if (parts.Length > 2)
                            {
                                result = (firstOperand <= valueAsDouble && valueAsDouble <= secondOperand);
                            }
                            break;
                        case "between3":
                            // x <= y < z
                            if (parts.Length > 2)
                            {
                                result = (firstOperand <= valueAsDouble && valueAsDouble < secondOperand);
                            }
                            break;
                        case "between4":
                            // x < y < z
                            if (parts.Length > 2)
                            {
                                result = (firstOperand < valueAsDouble && valueAsDouble <= secondOperand);
                            }
                            break;
                    }
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
