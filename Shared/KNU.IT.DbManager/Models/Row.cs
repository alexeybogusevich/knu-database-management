using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.IT.DbManager.Models
{
    public class Row
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid TableId { get; set; }
        public Table Table { get; set; }
    }
}
