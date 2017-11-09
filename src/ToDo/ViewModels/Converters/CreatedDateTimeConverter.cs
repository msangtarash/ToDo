using Humanizer;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToDo.ViewModels.Converters
{
    public class CreatedDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset date = (DateTimeOffset)value;

            return date.Humanize();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
