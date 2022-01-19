using System;
using Windows.UI.Xaml.Data;

namespace ToDoWeb.Converters
{
   public class DateConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         return new DateTimeOffset((DateTime)value);
      }

      public object ConvertBack(object value, Type targetType, object parameter, string language)
      {
         throw new NotImplementedException();
      }
   }
}
