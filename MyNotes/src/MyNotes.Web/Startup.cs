﻿using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyNotes.Web.Services;
using Microsoft.Extensions.Logging;
using MyNotes.Web.MultiTenancy;
using MyNotes.Web.MultiTenancy.Resolvers;
using MyNotes.Web.MultiTenancy.Sources;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json.Serialization;
using Glimpse;

namespace MyNotes.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddGlimpse();

            services.AddMultitenancy<MultiTenancyResolver>().Configure<MultiTenancyOptions>(opt =>
            {
                opt.Resolvers.Add(new UrlTenantResolver() { TenantsSources = new[] { new MemoryTenantsSource() } });
            });

            services.AddMvc(options => {
                var formatter = new JsonOutputFormatter
                {
                    SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                };
                options.OutputFormatters.Insert(0, formatter);
            });
            services.AddSingleton<IDbContextUnitOfWork, InMemoryCollectionContext>();
            services.AddTransient<ITenantsService, TenantsService>();
            services.AddTransient<IWishDaysService, WishDaysService>();
            services.AddTransient<IWishItemsService, WishItemsService>();
            services.AddInstance<IUserContextService>(new FakeUserContextService(Guid.NewGuid()));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseMultiTenancy();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "tanent_default",
                    template: "{tenant}/{controller=Notes}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            if (env.IsDevelopment())
            {
                app.UseGlimpse();
            }
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
