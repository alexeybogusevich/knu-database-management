using System;
using System.Collections.Generic;

namespace KNU.IT.DbServices.Models
{
    public class RowResponse
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public Dictionary<string, string> Content { get; set; }
    }
}
