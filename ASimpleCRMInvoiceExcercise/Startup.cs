using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Backend.API.Infrastructure.DI;
using Backend.API.Infrastructure.Mappings;
using Backend.API.Infrastructure.Mappings.AutoFacExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Module = Autofac.Module;

namespace Backend.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }


        //is auto discovered
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblies = GetAllAssemblies();
            builder.RegisterInstance<IConfiguration>(Configuration);


            builder.RegisterProjectModules(assemblies);
            builder.RegisterAutoMapper(assemblies);


        }


        public Assembly[] GetAllAssemblies()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetFiles(path, "*.dll").Select((s, i) =>
            {
                try
                {
                    return Assembly.LoadFrom(s);
                }
                catch (Exception e)
                {
                    return null;
                }

            }).Where(x => x != null).ToArray();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // method is altered by reco from void to IServiceProvider, 
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrmInvoiceSample", Version = "v1" }); ;
                });

            services.AddCors(options => options.AddPolicy("AllowCors",
                policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            services.AddHttpClient();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddControllersAsServices().AddMvcOptions(x => x.EnableEndpointRouting = false);


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        }

        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crm Invoice App V1"); });

            app.UseMvc();
        }
    }

}
