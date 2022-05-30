using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.Queries;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommanderGQL
{
    public class Startup
    {
        private readonly IConfiguration _configuration;


// only IWebHostEnviroment, IHostEnviroment and IConfiguration can be injected into the startup constructor 
// when using IHostBuilder
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // the AddPooledDbContext is more efficient than AddDbContext -- multilevel query
            services.AddPooledDbContextFactory<AppDbContext>(opt => {
                opt.UseSqlServer(_configuration.GetConnectionString("GraphQLPlatformDB"));
            });

            // Adding the graphql server
            services
                .AddGraphQLServer()
                .AddQueryType<Query>();




        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
              // add the graphql endpoing
              endpoints.MapGraphQL();
            });

            // user the graphhql voyager options to view schema
            app.UseGraphQLVoyager(new VoyagerOptions {
                GraphQLEndPoint = "/graphql"
                }, "/graphql-voyager"
            );


        }
    }
}
