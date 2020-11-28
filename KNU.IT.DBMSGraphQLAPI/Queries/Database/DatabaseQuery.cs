using GraphQL.Types;
using KNU.IT.DBMSGraphQLAPI.Models;
using KNU.IT.DbServices.Services.DatabaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.DBMSGraphQLAPI.Queries.Database
{
    public class DatabaseQuery : ObjectGraphType
    {
        public DatabaseQuery(IDatabaseService databaseService)
        {
            Field<ListGraphType<DatabaseType>>("databases",
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
