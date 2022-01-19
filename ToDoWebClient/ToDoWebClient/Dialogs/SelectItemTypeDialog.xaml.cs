using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDoWeb.Dialogs
{
   public sealed partial class SelectItemTypeDialog : ContentDialog
   {
      public readonly List<string> TypeStrings;
      public string Selection { get; private set; }

      public SelectItemTypeDialog()
      {
         InitializeComponent();
         TypeStrings = new List<string> { "Task", "Appointment" };
      }

      private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
         if (comboBoxTypeSelection.SelectedItem != null)
         {
            Selection = comboBoxTypeSelection.SelectedItem.ToString();
         }
      }

      private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }
   }
}
