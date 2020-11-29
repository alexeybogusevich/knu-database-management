using GraphQL.Types;
using GraphQL.Utilities;
using KNU.IT.DBMSGraphQLAPI.Queries.Database;
using System;

namespace KNU.IT.DBMSGraphQLAPI.Schemas
{
    public class DBMSSchema : Schema
    {
        public DBMSSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<DatabaseQuery>();
        }
    }
}
