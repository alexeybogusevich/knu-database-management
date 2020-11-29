using GraphQL;
using GraphQL.Types;
using KNU.IT.DBMSGraphQLAPI.Models;
using KNU.IT.DbServices.Services.DatabaseService;
using KNU.IT.DbServices.Services.RowService;
using KNU.IT.DbServices.Services.TableService;
using System;
using System.Collections.Generic;

namespace KNU.IT.DBMSGraphQLAPI.Queries.Database
{
    public class DBMSQuery : ObjectGraphType
    {
        public DBMSQuery(IDatabaseService databaseService, ITableService tableService, IRowService rowService)
        {
            Name = nameof(DBMSQuery);

            Field<ListGraphType<DatabaseType>>("databases", "returns all the databases",
                arguments: new QueryArguments(new List<QueryArgument>
                {

                }),
                resolve: context =>
                {
                    return databaseService.GetAllAsync().Result;
                });

            Field<ListGraphType<TableType>>("tables", "returns database tables",
            arguments: new QueryArguments(new List<QueryArgument>
            {
                            new QueryArgument<StringGraphType>
                            {
                                Name = "databaseId"
                            }
            }),
            resolve: context =>
            {
                var databaseId = context.GetArgument<Guid?>("databaseId");
                return tableService.GetAllAsync((Guid)databaseId).Result;
            });

            Field<ListGraphType<RowType>>("rows", "returns table rows",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                                new QueryArgument<StringGraphType>
                                {
                                    Name = "tableId"
                                }
                }),
                resolve: context =>
                {
                    var tableId = context.GetArgument<Guid?>("tableId");
                    return rowService.GetAllAsync((Guid)tableId).Result;
                });

            Field<ListGraphType<RowType>>("search", "returns table rows by search keyword",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                                new QueryArgument<StringGraphType>
                                {
                                    Name = "tableId"
                                },
                                new QueryArgument<StringGraphType>
                                {
                                    Name = "keyword"
                                },
                                new QueryArgument<StringGraphType>
                                {
                                    Name = "column"
                                }
                }),
                resolve: context =>
                {
                    var tableId = context.GetArgument<Guid?>("tableId");
                    var keyword = context.GetArgument<string>("keyword");
                    var column = context.GetArgument<string>("column");
                    return rowService.SearchByKeywordAsync((Guid)tableId, keyword, column).Result;
                });
        }
    }
}
