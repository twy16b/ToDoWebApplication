using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Library.ToDo.Models;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDoWeb.Dialogs
{
   public sealed partial class AppointmentDialog : ContentDialog
   {
      public readonly Appointment ApptContext;
      public readonly List<string> PriorityStrings;

      public AppointmentDialog(Appointment appt)
      {
         InitializeComponent();
         DataContext = this;
         ApptContext = appt;
         PriorityStrings = new List<string> { "Low", "Medium", "High", "Critical" };
      }

      private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }

      private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }

      private void datePick_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
      {
         if (sender.Name == "datePickStart")
         {
            ApptContext.Start = new DateTime
            (
               args.NewDate.Value.Year,
               args.NewDate.Value.Month,
               args.NewDate.Value.Day,
               ApptContext.Start.Hour,
               ApptContext.Start.Minute,
               ApptContext.Start.Second
            );
         }
         else if (sender.Name == "datePickStop")
         {
            ApptContext.Stop = new DateTime
            (
               args.NewDate.Value.Year,
               args.NewDate.Value.Month,
               args.NewDate.Value.Day,
               ApptContext.Stop.Hour,
               ApptContext.Stop.Minute,
               ApptContext.Stop.Second
            );
         }
      }

      private void timePick_TimeChanged(object sender, TimePickerValueChangedEventArgs args)
      {
         TimePicker source = (TimePicker)sender;

         if (source.Name == "timePickStart")
         {
            ApptContext.Start = new DateTime
            (
               ApptContext.Start.Year,
               ApptContext.Start.Month,
               ApptContext.Start.Day,
               args.NewTime.Hours,
               args.NewTime.Minutes,
               args.NewTime.Seconds
            );
         }
         else if (source.Name == "timePickStop")
         {
            ApptContext.Stop = new DateTime
            (
               ApptContext.Stop.Year,
               ApptContext.Stop.Month,
               ApptContext.Stop.Day,
               args.NewTime.Hours,
               args.NewTime.Minutes,
               args.NewTime.Seconds
            );
         }
      }
   }
}
