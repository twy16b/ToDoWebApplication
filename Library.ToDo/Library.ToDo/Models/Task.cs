using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.ToDo.Models
{
   public class Task : ItemBase
   {
      public DateTime Deadline { get; set; }
      public override bool IsCompleted { get; set; }
      public override DateTime CompareTime { get => Deadline; }

      public Task()
      {
         Deadline = DateTime.Now;
      }

      public override void CopyData(ItemBase item)
      {
         Name = item.Name;
         Description = item.Description;
         Priority = item.Priority;
         CreatedTime = item.CreatedTime;
         if (item is Task)
         {
            Deadline = (item as Task).Deadline;
            IsCompleted = (item as Task).IsCompleted;
         }
      }

      public override string ToString()
      {
         return
         $"Name: {Name}\n" +
         $"Description: {Description}\n" +
         $"Deadline: {Deadline}\n" +
         $"Completed: {IsCompleted}\n";
      }
      
   }
}
