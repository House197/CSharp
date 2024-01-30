using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using API.Models;
using API.Services;

namespace mongodb_dotnet_example
{
    public class Startup
    {

        private readonly string USER = string.Empty;
        private readonly string PWD = string.Empty;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            USER = configuration["DB_USER"] ?? string.Empty;
            PWD = configuration["DB_PASSWORD"] ?? string.Empty;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.Configure<DatabaseSettings>(settings =>
            {
                settings.RecipesCollectionName = Configuration["DatabaseSettings:RecipesCollectionName"] ?? string.Empty;
                settings.IngredientsCollectionName = Configuration["DatabaseSettings:IngredientsCollectionName"] ?? string.Empty;
                settings.DatabaseName = Configuration["DatabaseSettings:DatabaseName"] ?? string.Empty;
            });

            services.AddSingleton<IDatabaseSettings>(sp =>
                new DatabaseSettings(USER, PWD)
            );

            services.AddSingleton<RecipesService>();
            services.AddSingleton<IngredientsService>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
            }).AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}