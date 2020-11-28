using GraphQL.Types;
using KNU.IT.DbServices.Models;

namespace KNU.IT.DBMSGraphQLAPI.Models
{
    public class TableType : ObjectGraphType<TableResponse>
    {
        public TableType()
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.DatabaseId);
            Field(t => t.DatabaseName);
            Field(t => t.Schema);
        }
    }
}
