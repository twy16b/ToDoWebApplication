using System;
using Windows.UI.Xaml.Data;

namespace ToDoWeb.Converters
{
   public class TimeConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         DateTime newTime = (DateTime)value;
         return new TimeSpan(newTime.Hour, newTime.Minute, newTime.Second);
      }

      public object ConvertBack(object value, Type targetType, object parameter, string language)
      {
         throw new NotImplementedException();
      }
   }
}
