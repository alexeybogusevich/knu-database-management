using KNU.IT.DbManager.Connections;
using KNU.IT.DBMSGraphQLAPI.Constants;
using KNU.IT.DbServices.Services.DatabaseService;
using KNU.IT.DbServices.Services.RowService;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using KNU.IT.DBMSGraphQLAPI.Schemas;
using GraphQL.Server.Ui.Playground;
using System;
using GraphQL.Server;
using KNU.IT.DBMSGraphQLAPI.Middleware;
using GraphQL.Server.Ui.GraphiQL;

namespace KNU.IT.DBMSGraphQLAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AzureSqlDbContext>(options
                 => options.UseSqlServer(Configuration.GetConnectionString(ConfigurationConstants.DatabaseConntectionString)));

            services.AddScoped<IDatabaseService, SqlDatabaseService>();
            services.AddScoped<ITableService, SqlTableService>();
            services.AddScoped<IRowService, SqlRowService>();

            services.AddScoped<DBMSSchema>();

            services.AddGraphQL(x => x.EnableMetrics = true)
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddSystemTextJson()
                .AddWebSockets()
                .AddDataLoader();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KNU IT Web API Doc.", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGraphQL<DBMSSchema>("/graphql");
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseGraphiQLServer(new GraphiQLOptions());

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KNU GraphQL API Doc. V1");

                c.DocumentTitle = "This swagger doc describes methods in KNU.IT.DBMS";
                c.DocExpansion(DocExpansion.None);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

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
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
