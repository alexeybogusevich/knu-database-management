using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KNU.IT.DbManager.Models
{
    public class Row
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [NotMapped]
        public string ObjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid TableId { get; set; }
        [JsonIgnore]
        public Table Table { get; set; }
    }
}
