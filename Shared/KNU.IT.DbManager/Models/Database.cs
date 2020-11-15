using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KNU.IT.DbManager.Models
{
    public class Database
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [NotMapped]
        public string ObjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
