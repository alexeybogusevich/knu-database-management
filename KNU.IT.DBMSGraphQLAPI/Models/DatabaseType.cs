using GraphQL.Types;
using KNU.IT.DbManager.Models;

namespace KNU.IT.DBMSGraphQLAPI.Models
{
    public class DatabaseType : ObjectGraphType<Database>
    {
        public DatabaseType()
        {
            Field(d => d.Id);
            Field(d => d.Name);
        }
    }
}
