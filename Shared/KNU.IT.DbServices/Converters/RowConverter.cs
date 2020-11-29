using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KNU.IT.DbServices.Converters
{
    public static class RowConverter
    {
        public static RowResponse GetRowResponse(Row row)
        {
            return new RowResponse
            {
                TableId = row.TableId,
                Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(row.Content)
            };
        }
    }
}
