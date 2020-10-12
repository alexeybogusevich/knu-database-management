using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.DbManagementSystem.Models
{
    public class TableViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DatabaseId { get; set; }
        public Dictionary<string, string> Schema { get; set; }
    }
}
