using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Library.ToDo.Models;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDoWeb.Dialogs
{
   public sealed partial class TaskDialog : ContentDialog
   {
      public readonly Task TaskContext;
      public readonly List<string> PriorityStrings;

      public TaskDialog(Task task)
      {
         InitializeComponent();
         DataContext = this;
         TaskContext = task;
         PriorityStrings = new List<string> { "Low", "Medium", "High", "Critical" };
      }

      private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }

      private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }

      private void datePickDeadline_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
      {
         TaskContext.Deadline = new DateTime
         (
            args.NewDate.Value.Year,
            args.NewDate.Value.Month,
            args.NewDate.Value.Day,
            TaskContext.Deadline.Hour,
            TaskContext.Deadline.Minute,
            TaskContext.Deadline.Second
         );
      }

      private void timePickDeadline_TimeChanged(object sender, TimePickerValueChangedEventArgs args)
      {
         TaskContext.Deadline = new DateTime
            (
               TaskContext.Deadline.Year,
               TaskContext.Deadline.Month,
               TaskContext.Deadline.Day,
               args.NewTime.Hours,
               args.NewTime.Minutes,
               args.NewTime.Seconds
            );
      }
   }
}
