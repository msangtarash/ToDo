using System;
using Xamarin.Forms;

namespace ToDo.ViewModels.Converters
{
    public class BooleanToCheckedIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value == false)
                return "fa-circle-o";
            else
                return "fa-check-circle";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
