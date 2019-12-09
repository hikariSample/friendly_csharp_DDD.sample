using System;
using System.Collections.Generic;
using System.Linq;
using System.NetCore.Extensions.Swagger;
using System.Threading.Tasks;
using DDD.Core.Sample.Application;
using DDD.Core.Sample.Infrastructure;
using DDD.Core.Sample.Infrastructure.Interfaces;
using DDD.Core.Sample.Repository;
using DDD.Core.Sample.WebApi.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using NLog.Web;

namespace DDD.Core.Sample.WebApi
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
            DDD.Core.Sample.BootStrapper.Startup.AddConfigureServices(services, Configuration);

            //添加跨域请求
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins(Configuration["OtherConfig:Cors"].Split(',', StringSplitOptions.RemoveEmptyEntries))
                        .AllowAnyHeader().WithHeaders(HeaderNames.Authorization, HeaderNames.ContentType, HeaderNames.Accept, HeaderNames.Origin)
                        .AllowAnyMethod().WithMethods("PATCH").WithMethods("DELETE")
                        .AllowCredentials();

                });
            });

            services.AddSwaggerCustom();

            services.AddMvcCore().AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = Configuration["OtherConfig:Authority"];
                    options.ApiName = "api1";
                    options.ApiSecret = "pwd_secret";
                });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ModelStateValidationAttribute));
                options.Filters.Add(typeof(ExceptionAttribute));
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            }).AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowSpecificOrigin");

            app.UseSwaggerCustom(provider);

            DDD.Core.Sample.BootStrapper.Startup.AddConfigure(loggerFactory);

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
