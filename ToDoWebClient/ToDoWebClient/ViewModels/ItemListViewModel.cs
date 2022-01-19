using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using ToDoWeb.Dialogs;
using Library.ToDo.Models;
using Library.ToDo.WebHandler;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoWeb
{
   public class ItemListViewModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      private WebRequestHandler WebHandler { get; }
      public ObservableCollection<ItemBase> ShownItems { get; }
      public ItemBase SelectedItem { get; set; }
      public string SearchString { get; set; }

      private bool _showCompleted;
      public bool ShowCompleted 
      {
         get => _showCompleted;
         set
         {
            _showCompleted = value;
            UpdateShownItems();
         }
      }

      private bool _sortByPriority;
      public bool SortByPriority
      {
         get => _sortByPriority;
         set
         {
            _sortByPriority = value;
            UpdateShownItems();
         }
      }

      public ItemListViewModel()
      {
         WebHandler = new WebRequestHandler();
         ShownItems = new ObservableCollection<ItemBase>();
         _showCompleted = false;
         SearchString = "";
         UpdateShownItems();
      }

      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      public void UpdateShownItems()
      {
         ShownItems.Clear();

         List<ItemBase> query;
         IEnumerable<ItemBase> sorted;

         if (SearchString == "")
         {
            if(ShowCompleted)
            {
               string result = WebHandler.Get("http://localhost/ToDoWebAPI/ToDo/GetAll").Result;
               query = ItemBase.DeserializeItems(result ?? "") ?? new List<ItemBase>();
            }
            else{
               string result = WebHandler.Get("http://localhost/ToDoWebAPI/ToDo/GetNotCompleted").Result;
               query = ItemBase.DeserializeItems(result ?? "") ?? new List<ItemBase>();
            }
         }

         else
         {
            string result = WebHandler.Get("http://localhost/ToDoWebAPI/ToDo/GetSearch/" + SearchString).Result;
            query = ItemBase.DeserializeItems(result ?? "") ?? new List<ItemBase>();
         }

         if (SortByPriority)
         {
            sorted = query.OrderByDescending(item => item.Priority);
         }
         else
         {
            sorted = query.OrderBy(item => item.CompareTime);
         }

         foreach(ItemBase item in sorted)
         {
            ShownItems.Add(item);
         }

         NotifyPropertyChanged("ShownItems");
      }

      public async void AddItem()
      {
         var typeDialog = new SelectItemTypeDialog();
         await typeDialog.ShowAsync();

         if (typeDialog.Selection != null)
         {
            if (typeDialog.Selection == "Task")
            {
               Task temp = new Task();
               var addDialog = new TaskDialog(temp);
               await addDialog.ShowAsync();
               if (temp != null)
                  await WebHandler.Post("http://localhost/ToDoWebAPI/ToDo/AddTask", temp);
            }
            else if (typeDialog.Selection == "Appointment")
            {
               Appointment temp = new Appointment();
               var addDialog = new AppointmentDialog(temp);
               await addDialog.ShowAsync();
               if (temp != null)
                  await WebHandler.Post("http://localhost/ToDoWebAPI/ToDo/AddAppointment", temp);
            }
         }
         UpdateShownItems();
      }

      public async void EditItem()
      {
         if (SelectedItem is Task)
         {
            var editDialog = new TaskDialog(SelectedItem as Task);
            await editDialog.ShowAsync();
            await WebHandler.Post("http://localhost/ToDoWebAPI/ToDo/UpdateTask/" + SelectedItem._id, SelectedItem);
         }
         else if (SelectedItem is Appointment)
         {
            var editDialog = new AppointmentDialog(SelectedItem as Appointment);
            await editDialog.ShowAsync();
            await WebHandler.Post("http://localhost/ToDoWebAPI/ToDo/UpdateAppointment/" + SelectedItem._id, SelectedItem);
         }
         UpdateShownItems();
      }

      public void Search(string input)
      {
         SearchString = input;
         UpdateShownItems();
      }

      public async void DeleteItem()
      {
         await WebHandler.Post("http://localhost/ToDoWebAPI/ToDo/DeleteItem/" + SelectedItem._id, SelectedItem);
         UpdateShownItems();
      }

   }
}
