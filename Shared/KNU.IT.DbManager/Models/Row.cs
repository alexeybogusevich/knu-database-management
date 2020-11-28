using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KNU.IT.DbManager.Models
{
    public class Row
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [NotMapped]
        [JsonIgnore]
        public string ObjectId { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid TableId { get; set; }
        [JsonIgnore]
        public Table Table { get; set; }
    }
}
