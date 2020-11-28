using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KNU.IT.DbManager.Models
{
    public class Table
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [NotMapped]
        [JsonIgnore]
        public string ObjectId { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Schema { get; set; }
        public Guid DatabaseId { get; set; }
        [JsonIgnore]
        public Database Database { get; set; }
    }
}
