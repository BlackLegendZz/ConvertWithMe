using System.Globalization;
using System.Windows.Data;

namespace ConvertWithMe.UI.Views.Converters
{
    class DivideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal num = System.Convert.ToDecimal(value);
            decimal num2 = System.Convert.ToDecimal(parameter);
            return Math.Round(num / num2, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
