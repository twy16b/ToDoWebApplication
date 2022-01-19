using System;
using Windows.UI.Xaml.Data;

namespace ToDoWeb.Converters
{
   public class ItemTypeConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         return value.GetType().Name;
      }

      public object ConvertBack(object value, Type targetType, object parameter, string language)
      {
         throw new NotImplementedException();
      }
   }
}
