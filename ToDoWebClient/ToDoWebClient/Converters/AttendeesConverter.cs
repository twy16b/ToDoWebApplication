using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace ToDoWeb.Converters
{
   public class AttendeesConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         List<string> attendees = value as List<string>;
         string output = "";
         foreach (string attendee in attendees)
         {
            output += attendee + '\n';
         }
         return output;
      }

      public object ConvertBack(object value, Type targetType, object parameter, string language)
      {
         return (value as string).Split('\n').Select(s => s.Trim()).ToList();
      }
   }
}
