using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KNU.IT.DbServices.Converters
{
    public static class TableConverter
    {
        public static TableResponse GetTableResponse(Table table)
        {
            return new TableResponse
            {
                Id = table.Id,
                Name = table.Name,
                DatabaseId = table.DatabaseId,
                Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema)
            };
        }
    }
}
