using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using KNU.IT.DBMSWebApi.Constants;
using KNU.IT.DBMSWebApi.Middleware;
using KNU.IT.DbServices.Models;
using KNU.IT.DbServices.Services.DatabaseService;
using KNU.IT.DbServices.Services.RowService;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net.Http;

namespace KNU.IT.DBMSWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AzureSqlDbContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString(ConfigurationConstants.DatabaseConntectionString)));

            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IRowService, RowService>();

            services.AddHATEOAS(options =>
            {
                options.AddLink<RowDTO>("self",
                    RouteNames.RowGet,
                    HttpMethod.Get,
                    (x) => new { id = x.Id });

                options.AddLink<RowDTO>("by table",
                   RouteNames.RowGetByTable,
                   HttpMethod.Get,
                   (x) => new { tableId = x.Id });

                options.AddLink<RowDTO>("create",
                    RouteNames.RowCreate,
                    HttpMethod.Post, null);

                options.AddLink<RowDTO>("delete",
                    RouteNames.RowDelete,
                    HttpMethod.Delete,
                    (x) => new { id = x.Id });

                options.AddLink<RowDTO>("update",
                    RouteNames.RowUpdate,
                    HttpMethod.Post, null);

                options.AddLink<TableDTO>("self",
                    RouteNames.TableGet,
                    HttpMethod.Get,
                    (x) => new { id = x.Id });

                options.AddLink<TableDTO>("by database",
                   RouteNames.TableGetByDatabase,
                   HttpMethod.Get,
                   (x) => new { databaseId = x.Id });

                options.AddLink<TableDTO>("search",
                    RouteNames.TableSearch,
                    HttpMethod.Get,
                    (x) => new { tableId = x.Id });

                options.AddLink<TableDTO>("delete",
                    RouteNames.TableDelete,
                    HttpMethod.Delete,
                    (x) => new { id = x.Id });

                options.AddLink<TableDTO>("create",
                    RouteNames.TableCreate,
                    HttpMethod.Post, null);

                options.AddLink<TableDTO>("update",
                    RouteNames.TableUpdate,
                    HttpMethod.Post, null);

                options.AddLink<Database>("self",
                  RouteNames.DatabaseGet,
                  HttpMethod.Get,
                  (x) => new { id = x.Id });

                options.AddLink<Database>("all",
                   RouteNames.DatabaseGetAll,
                   HttpMethod.Get, null);

                options.AddLink<Database>("delete",
                    RouteNames.DatabaseDelete,
                    HttpMethod.Delete,
                    (x) => new { id = x.Id });

                options.AddLink<Database>("create",
                    RouteNames.DatabaseCreate,
                    HttpMethod.Post, null);

                options.AddLink<Database>("update",
                    RouteNames.DatabaseUpdate,
                    HttpMethod.Post, null );
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KNU IT Web API Doc.", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SVC Portal API Doc. V1");

                c.DocumentTitle = "This swagger doc describes methods in SB.SVC.Portal";
                c.DocExpansion(DocExpansion.None);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
