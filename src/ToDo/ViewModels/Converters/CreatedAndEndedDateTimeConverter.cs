using Humanizer;
using System;
using System.Globalization;
using ToDo.Model;
using Xamarin.Forms;

namespace ToDo.ViewModels.Converters
{
    public class CreatedAndEndedDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            ToDoItem toDoItem = (ToDoItem)value;

            DateTimeOffset createdDateTime = toDoItem.CreatedDateTime;

            DateTimeOffset endedDateTime = toDoItem.EndedDateTime;

            if (toDoItem.IsFinished == false)
                return createdDateTime.Humanize();
            else
                return $"{endedDateTime.Humanize()}   Completed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
