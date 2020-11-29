using GraphQL.Types;
using KNU.IT.DbManager.Models;

namespace KNU.IT.DBMSGraphQLAPI.Models
{
    public class RowType : ObjectGraphType<Row>
    {
        public RowType()
        {
            Field(r => r.Id);
            Field(r => r.TableId);
            Field(r => r.Content);
        }
    }
}
