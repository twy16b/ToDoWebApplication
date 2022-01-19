using Library.ToDo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ToDoWebAPI.Persistence
{
   public class Database
   {
      private IMongoDatabase _database;

      private static Database instance;
      public static Database Current
      {
         get
         {
            if (instance == null)
            {
               var settings = MongoClientSettings.FromConnectionString("mongodb+srv://DBuser1:nzvfqz98@cluster0.kdalk.mongodb.net/todo?retryWrites=true&w=majority");
               var client = new MongoClient(settings);
               var _db = client.GetDatabase("todo");
               instance = new Database(_db);
            }

            return instance;
         }
      }

      public List<ItemBase> GetItems()
      {
         List<ItemBase> list = new List<ItemBase>();
         var tasksCollection = _database.GetCollection<BsonDocument>("tasks");
         var tasks = tasksCollection.Find(_ => true).ToList();
         foreach (var item in tasks)
         {
            list.Add(BsonSerializer.Deserialize<Task>(item));
         }

         var apptCollection = _database.GetCollection<BsonDocument>("appointments");
         var appts = apptCollection.Find(_ => true).ToList();
         foreach (var item in appts)
         {
            list.Add(BsonSerializer.Deserialize<Appointment>(item));
         }
         return list;
      }

      public void AddOrUpdate(ItemBase item)
      {
         if (string.IsNullOrEmpty(item._id))
         {
            item._id = ObjectId.GenerateNewId().ToString();
         }

         //mapping for collections
         IMongoCollection<BsonDocument> collection;
         if (item is Task)
         {
            collection = _database.GetCollection<BsonDocument>("tasks");
            var result = collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(item._id)));
            collection.InsertOne(item.ToBsonDocument());
         }
         else if (item is Appointment)
         {
            collection = _database.GetCollection<BsonDocument>("appointments");
            collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(item._id)));
            collection.InsertOne(item.ToBsonDocument());
         }
         else
         {
            throw new TypeNotSupportedException(item.GetType().ToString());
         }

         return;
      }

      public void Delete(ItemBase item)
      {
         IMongoCollection<BsonDocument> collection;
         if (item is Task)
         {
            collection = _database.GetCollection<BsonDocument>("tasks");
            collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(item._id)));
         }
         else if (item is Appointment)
         {
            collection = _database.GetCollection<BsonDocument>("appointments");
            collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(item._id)));
         }
         else
         {
            throw new TypeNotSupportedException(item.GetType().ToString());
         }
      }


      private Database(IMongoDatabase db)
      {
         _database = db;
      }

   }

   public class TypeNotSupportedException : Exception
   {
      private string _type;
      public TypeNotSupportedException(string type)
      {
         _type = type;
      }
      public override string Message => $"Attempt was made to persist an unsupported type: {_type}";
   }
}
