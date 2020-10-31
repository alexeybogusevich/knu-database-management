using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.IT.DbManager.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Schema { get; set; }
        public Guid DatabaseId { get; set; }
        [JsonIgnore]
        public Database Database { get; set; }
    }
}
