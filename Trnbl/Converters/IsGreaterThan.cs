
using System.Globalization;
namespace Trnbl.Converters;
public class IsGreaterThan : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int number;
        if (Int32.TryParse((string)parameter, out number))
        {
            return (int)value > number;

        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("IsGreaterThan can only be used OneWay.");
    }
}

