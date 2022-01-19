using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Library.ToDo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using ToDoWebAPI.Persistence;

namespace ToDoWebAPI.Controllers
{
   [ApiController]
   [Route("ToDo")]
   public class ToDoController : ControllerBase
   {
      private List<ItemBase> Items;

      [HttpGet("GetAll")]
      public ActionResult<string> GetAll()
      {
         Items = Database.Current.GetItems(); 
         return Ok(ItemBase.SerializeItems(Items));
      }

      [HttpGet("GetNotCompleted")]
      public ActionResult<string> GetNotCompleted()
      {
         Items = Database.Current.GetItems();
         var query = Items.Where(item => !item.IsCompleted);
         return Ok(ItemBase.SerializeItems(new List<ItemBase>(query)));
      }

      [HttpGet("GetSearch/{term}")]
      public ActionResult<string> GetSearch(string term)
      {
         Items = Database.Current.GetItems();

         var query = Items.Where(item =>
            item.Name.ToUpper().Contains(term.ToUpper()) ||
            item.Description.ToUpper().Contains(term.ToUpper()) ||
            (item is Appointment && (item as Appointment).Attendees.Any(a => a.ToUpper().Contains(term.ToUpper()))));

         return Ok(ItemBase.SerializeItems(new List<ItemBase>(query)));
      }

      [HttpPost("AddTask")]
      public ActionResult<string> Post(Task item)
      {
         Database.Current.AddOrUpdate(item);
         return Ok(item._id);
      }

      [HttpPost("AddAppointment")]
      public ActionResult<string> Post(Appointment item)
      {
         Database.Current.AddOrUpdate(item);
         return Ok(item._id);
      }

      [HttpPost("UpdateTask/{id}")]
      public ActionResult<int> UpdateTask(Task newTaskData, string id)
      {
         Items = Database.Current.GetItems();
         Task editTask = (Task)Items.FirstOrDefault(item => item._id == id);
         editTask.CopyData(newTaskData);
         Database.Current.AddOrUpdate(editTask);
         return Ok(id);
      }

      [HttpPost("UpdateAppointment/{id}")]
      public ActionResult<int> UpdateAppt(Appointment newApptData, string id)
      {
         Items = Database.Current.GetItems();
         Appointment editAppt = (Appointment) Items.FirstOrDefault(item => item._id == id);
         editAppt.CopyData(newApptData);
         Database.Current.AddOrUpdate(editAppt);
         return Ok(id);
      }

      [HttpPost("DeleteItem/{id}")]
      public ActionResult<string> DeleteItem(string id)
      {
         Items = Database.Current.GetItems();
         ItemBase removeItem = Items.FirstOrDefault(item => item._id == id);
         Database.Current.Delete(removeItem);

         if (Items.Remove(removeItem))
            return Ok("Item Removed");
         else
            return Problem("Item Not Deleted");
      }
   }
}
