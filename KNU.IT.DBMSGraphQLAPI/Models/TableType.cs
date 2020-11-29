using GraphQL.Types;
using KNU.IT.DbManager.Models;

namespace KNU.IT.DBMSGraphQLAPI.Models
{
    public class TableType : ObjectGraphType<Table>
    {
        public TableType()
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.DatabaseId);
            Field(t => t.Schema);
        }
    }
}
