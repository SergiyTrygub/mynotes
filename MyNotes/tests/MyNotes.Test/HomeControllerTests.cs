using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Controllers;
using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using MyNotes.Web.Repositories;
using MyNotes.Web.Services;
using System;
using Xunit;

namespace MyNotes.Test
{
    public class HomeControllerTests
    {
        private readonly IServiceProvider _serviceProvider;

        public HomeControllerTests()
        {
            var services = new ServiceCollection();

            services.AddLogging();
            services.AddTransient<IRepository<AppTenant>, InMemoryRepository<AppTenant, string>>();
            services.AddTransient<IRepository<WishDay>, InMemoryRepository<WishDay, int>>();
            services.AddTransient<IRepository<WishItem>, InMemoryRepository<WishItem, int>>();
            services.AddTransient<IRepository<WishItemTag>, InMemoryRepository<WishItemTag, int>>();
            services.AddTransient<IUnitOfWorkContext, MyWishesUnitOfWorkDbContext>();
            services.AddTransient<IUnitOfWorkContext, MyWishesUnitOfWorkInMemoryContext>();
            services.AddTransient<ITenantsService, TenantsService>();
            
            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async void Create_new_tenant()
        {
            var controller = GetController();
            var result = await controller.CreateList();
            Assert.True(result is RedirectResult);
        }

        private HomeController GetController()
        {
            var service = _serviceProvider.GetRequiredService<ITenantsService>();
            var logger = _serviceProvider.GetRequiredService<ILogger<HomeController>>();
            return new HomeController(service);
        }

    }
}
