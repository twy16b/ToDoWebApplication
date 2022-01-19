using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.ToDo.Models
{
   public abstract class ItemBase : IComparable
   {
      [JsonProperty]
      [BsonId]
      [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
      public string _id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public int Priority { get; set; }
      public abstract bool IsCompleted { get; set; }
      public DateTime CreatedTime { get; protected set; }
      public abstract DateTime CompareTime { get; }

      public ItemBase()
      {
         _id = "";
         CreatedTime = DateTime.Now;
      }

      public abstract void CopyData(ItemBase item);

      public static string SerializeItems(List<ItemBase> items)
      {
         return JsonConvert.SerializeObject(items, new JsonSerializerSettings
         {
            TypeNameHandling = TypeNameHandling.All
         });
      }

      public static List<ItemBase> DeserializeItems(string items)
      {
         return JsonConvert.DeserializeObject<List<ItemBase>>(items, new JsonSerializerSettings
         {
            TypeNameHandling = TypeNameHandling.All
         });
      }

      public int CompareTo(object obj)
      {
         ItemBase otherItem = obj as ItemBase;
         return CompareTime.CompareTo(otherItem.CompareTime);
      }
   }
}
