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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
 
using AutoMapper;
 

namespace Cdc.mmg.validator.WebApi
{
    public class Startup
    {
        private const string API_NAME = "mmg-validator API";
        private const string API_VERSION = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = API_NAME,
                    Version = API_VERSION,
                    Description = "The purpose of this API is to detect content-based validation errors with messages received by the CDC through the National Notifiable Diseases Surveillance System. This API is a prototype and not intended for production use.",
                    Contact = new OpenApiContact
                    {
                        Name = "Mohammed Lamtahri",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/Kodeistan/mmg-validator-api/tree/master")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

           

            // register the repository
           // services.AddScoped<IValueSetRepository, ValueSetRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles(); // will ensure index.html is returned when no resource is specified; this must come before the UseStaticFiles() line below
            app.UseStaticFiles(); // needed for the wwwroot folder so we can serve index.html

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{API_NAME} {API_VERSION}");
            });

            app.UseRouting();

            app.UseAuthorization();

            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    //cfg.CreateMap<ValueSet, ValueSetForRetrievalDto>()
            //    //    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src =>
            //    //        src.ValueSetCreatedDate))
            //    //    .ForMember(dest => dest.Definition, opt => opt.MapFrom(src =>
            //    //        src.DefinitionText))
            //    //    .ForMember(dest => dest.LastRevisionDate, opt => opt.MapFrom(src =>
            //    //        src.ValueSetLastRevisionDate));

            //    //cfg.CreateMap<ValueSetForInsertionDto, ValueSet>()
            //    //    .ForMember(dest => dest.DefinitionText, opt => opt.MapFrom(src =>
            //    //        src.Definition));
            //});

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
