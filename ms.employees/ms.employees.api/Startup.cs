using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ms.employees.application.HttpCommunications;
using ms.employees.application.Mappers;
using ms.employees.application.Queries;
using ms.employees.domain.Repositories;
using ms.employees.infrastructure.Data;
using ms.employees.infrastructure.Repositories;
using ms.rabbitmq.Producers;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Treblle.Net.Core;

namespace ms.employees.api
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
            services.AddScoped(typeof(IProducer), typeof(EventProducer));

            services.AddScoped(typeof(IDapperContext), typeof(EmployeesDapperContext));
            services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));

            var automapperConfig = new MapperConfiguration(mapperConfig =>
            { mapperConfig.AddMaps(typeof(EmployeesMapperProfile).Assembly); });
            IMapper mapper = automapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(typeof(GetAllEmployeeQuery).GetTypeInfo().Assembly);

            services.AddRefitClient<IAttendanceApiCommunication>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("Communication:External:AttendanceApiUrl").Value));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ms.employees.api", Version = "v1" });
            });

            var privateKey = Configuration.GetValue<string>("Authentication:JWT:Key");

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey)),
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1.1", new OpenApiInfo
                {
                    Title = "Employees Attendance Api",
                    Version = "v1"
                });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Cabecera de autorización JWT. \r\n Introduzca ['Bearer'] [espacio] [Token]"
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },new string[] { }
                    }
                });
            });

            services.AddTreblle(
                    Configuration["Treblle:ApiKey"],
                    Configuration["Treblle:ProjectId"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employees Attendance API V1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","Employees Attendance API V1"));

            app.UseTreblle(useExceptionHandler: true);
        }
    }
}
