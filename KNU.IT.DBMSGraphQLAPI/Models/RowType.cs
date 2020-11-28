using GraphQL.Types;
using KNU.IT.DbServices.Models;

namespace KNU.IT.DBMSGraphQLAPI.Models
{
    public class RowType : ObjectGraphType<RowResponse>
    {
        public RowType()
        {
            Field(r => r.Id);
            Field(r => r.TableId);
            Field(r => r.TableName);
            Field(r => r.Content);
        }
    }
}
