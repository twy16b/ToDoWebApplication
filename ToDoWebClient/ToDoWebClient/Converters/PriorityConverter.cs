using System;
using Windows.UI.Xaml.Data;

namespace ToDoWeb.Converters
{
   public class PriorityConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         int priority = (int)value;
         if (priority <= 0) return "Low";
         else if (priority == 1) return "Medium";
         else if (priority == 2) return "High";
         else if (priority >= 3) return "Critical";
         else return "Invalid";
      }

      public object ConvertBack(object value, Type targetType, object parameter, string language)
      {
         throw new NotImplementedException();
      }
   }
}
