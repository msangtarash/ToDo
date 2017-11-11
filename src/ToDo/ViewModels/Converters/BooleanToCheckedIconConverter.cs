using System;
using Xamarin.Forms;

namespace ToDo.ViewModels.Converters
{
    public class BooleanToCheckedIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value == false)
                return MaterialIcons.FormsPlugin.Abstractions.MaterialIcons.ic_check_box_outline_blank;
            else
                return MaterialIcons.FormsPlugin.Abstractions.MaterialIcons.ic_check_box;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
