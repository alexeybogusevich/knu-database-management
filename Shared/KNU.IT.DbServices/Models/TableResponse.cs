using System;
using System.Collections.Generic;

namespace KNU.IT.DbServices.Models
{
    public class TableResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DatabaseId { get; set; }
        public Dictionary<string, string> Schema { get; set; }
    }
}
