using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.DbManagementSystem.Models
{
    public class RowViewModel
    {
        public Guid TableId { get; set; }
        public Dictionary<string, string> Content { get; set; }
    }
}
