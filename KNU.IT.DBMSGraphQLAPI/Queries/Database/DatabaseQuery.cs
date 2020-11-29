using GraphQL.Types;
using KNU.IT.DBMSGraphQLAPI.Models;
using KNU.IT.DbServices.Services.DatabaseService;
using System.Collections.Generic;

namespace KNU.IT.DBMSGraphQLAPI.Queries.Database
{
    public class DatabaseQuery : ObjectGraphType
    {
        public DatabaseQuery(IDatabaseService databaseService)
        {
            Name = "Query"; 

            Field<ListGraphType<DatabaseType>>("databases", "returns all the databases",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<StringGraphType>
                    {
                        Name = "id"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "name"
                    }
                }),
                resolve: context =>
                {
                    return databaseService.GetAllAsync().Result;
                });
        }
    }
}
