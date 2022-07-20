using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MISA.WEB05.CORE;
using MISA.WEB05.INFRASTRUCTURE;
using MISA.WEB05.CORE.Interface.Repostory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB05.CORE.Interface.Sevice;
using MISA.WEB05.INFRASTRUCTURE.Repostory;
using MISA.WEB05.CORE.Sevice;
using System.IO;

namespace MISA.WEB05.API
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
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
                {
                    policy.WithOrigins("http://localhost:8080", "http://localhost:8081").AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.WEB05.API", Version = "v1" });
            });
            services.AddControllers();
            services.AddScoped<IDepartmentRepostory,DepartmentRepostory>();
            services.AddScoped<IDepartmentSevice, DepartmentSevice>();
            services.AddScoped<IEmployeeRepostory, EmployeeRepostory>();
            services.AddScoped<IEmployeeSevice, EmployeeSevice>();
            services.AddScoped<IPositionRepostory, PositionRepostory>();
            services.AddScoped<IPositionSevice, PositionSevice>();
            // show comment swager
            services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "YourApiName.xml");
                c.IncludeXmlComments(filePath);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.WEB05.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("_myAllowSpecificOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
