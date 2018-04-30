using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DMWorkshop.Handlers.Characters;
using MediatR;
using AutoMapper;
using MongoDB.Driver;
using DMWorkshop.Handlers.Mapping;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json.Converters;

namespace DMWorkshop.Web
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
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    var settings = options.SerializerSettings;
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.Converters.Add(new StringEnumConverter(true));
                });
            services.AddMediatR(typeof(RegisterCreatureCommandHandler).Assembly);
            services.AddAutoMapper(typeof(RegisterCreatureCommandHandler).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.SwaggerDoc("v1", new Info { Title = "DMWorkshop", Version = "v1" });
            });

            var connectionString = Configuration.GetConnectionString("DMWorkshop");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("dmworkshop");
            services.AddSingleton(database);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DMWorkshop V1");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            MongoMapping.Configure();
        }
    }
}
