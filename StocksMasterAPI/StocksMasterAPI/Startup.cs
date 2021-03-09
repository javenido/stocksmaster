using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StocksMasterAPI.Models;
using StocksMasterAPI.Services;

namespace StocksMasterAPI
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
            services.AddControllers();

            /*var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("Connection2RDSStocksMasterDB"));
            builder.UserID = Configuration["DbUser"];
            builder.Password = Configuration["DbPassword"];
            var connection = builder.ConnectionString;
            services.AddDbContext<StocksMasterDBContext>(options => options.UseSqlServer(connection));*/

            //services.AddDbContext<StocksMasterDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connection2RDSStocksMasterDB"), providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddDbContext<StocksMasterDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Local")));

            services.AddMvc();
            services.AddMvc(options =>
            {
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                options.FormatterMappings.SetMediaTypeMappingForFormat("js", "application/json");
            }).AddXmlSerializerFormatters();

            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "StocksMaster", Version = "v1" }); });

            services.AddScoped<IStocksMasterRepository, StocksMasterRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "StocksMaster V1"); });

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