using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Controllers;
using MyNotes.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            services.AddTransient<IDbContextUnitOfWork, MyWishesInMemoryContext>();
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
