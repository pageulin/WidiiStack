using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Test_API.DAL;
using Test_API.DAL.Service;

namespace Test_API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            MongoDBContext.init("localhost:27017", "root", "root", "testApi");
            services.Configure<MongoDBContext>(options =>
            {
                string dbServer = Configuration.GetSection("Database:DbServer").Value;
                string dbUser = Configuration.GetSection("Database:DbUser").Value;
                string dbPassword = Configuration.GetSection("Database:DbPassword").Value;
                string dbName = Configuration.GetSection("Database:DbName").Value;
                MongoDBContext.init(dbServer, dbUser, dbPassword, dbName);
            });
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new Info { Title="Test Api", Version="v1"});
                       // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test Api");
                options.RoutePrefix = "swagger";
            });

           if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
