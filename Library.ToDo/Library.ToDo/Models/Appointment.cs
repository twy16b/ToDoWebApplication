using System;
using System.Collections.Generic;

namespace Library.ToDo.Models
{
   public class Appointment : ItemBase
   {
      public override bool IsCompleted { get => DateTime.Now > Stop; set { } }
      public override DateTime CompareTime { get => Start; }
      public DateTime Start { get; set; }
      public DateTime Stop { get; set; }
      public List<string> Attendees { get; set; }

      public Appointment()
      {
         Start = DateTime.Now;
         Stop = DateTime.Now;
         Attendees = new List<string>();
      }

      public override void CopyData(ItemBase item)
      {
         Name = item.Name;
         Description = item.Description;
         Priority = item.Priority;
         CreatedTime = item.CreatedTime;
         if (item is Appointment)
         {
            Start = (item as Appointment).Start;
            Stop = (item as Appointment).Stop;
            Attendees = (item as Appointment).Attendees;
         }
      }

      public override string ToString()
      {
         string apptString =
         $"Name: {Name}\n" +
         $"Description: {Description}\n" +
         $"Start: {Start}\n" +
         $"Stop: {Stop}\n" +
         "Attendees:\n";
         foreach(string attendee in Attendees)
         {
            apptString += $"{attendee}\n";
         }
         return apptString;
      }
   }
}
