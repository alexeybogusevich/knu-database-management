using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KNU.IT.DbManager.Models
{
    public class Table
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [NotMapped]
        public string ObjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Schema { get; set; }
        public Guid DatabaseId { get; set; }
        [JsonIgnore]
        public Database Database { get; set; }
    }
}
