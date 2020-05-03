using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Backend.API.Infrastructure.Mappings.AutoFacExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

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

            builder.RegisterAssemblyModules(assemblies);
            builder.RegisterAutoMapper(assemblies);
        }

        private Assembly[] GetAllAssemblies()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // method is altered by reco from void to IServiceProvider, 
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrmInvoiceSample", Version = "v1" });;
                });

            services.AddCors(options => options.AddPolicy("AllowCors",
                policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            services.AddHttpClient();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddControllersAsServices();


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // create a Autofac container builder
            var builder = new ContainerBuilder();

            // read service collection to Autofac
            builder.Populate(services);
            ConfigureContainer(builder);
            // build the Autofac container
            var container = builder.Build();



            // creating the IServiceProvider out of the Autofac container
            return new AutofacServiceProvider(container);
        }

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

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crm Invoice App V1"); });
        }
    }

}
