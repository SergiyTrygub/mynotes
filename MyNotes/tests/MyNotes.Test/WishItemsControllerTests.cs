using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Controllers.Api.v1;
using MyNotes.Web.Models;
using MyNotes.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyNotes.Test
{
    public class WishItemsControllerTests
    {
        private readonly IServiceProvider _serviceProvider;

        public WishItemsControllerTests()
        {
            var services = new ServiceCollection();

            services.AddLogging();
            services.AddTransient<IDbContextUnitOfWork, InMemoryCollectionContext>();
            services.AddTransient<IWishItemsService, WishItemsService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async void get()
        {
            var controller = GetWishItemsController();
            var result = await controller.Get(1);
            Assert.True(result != null);
        }

        [Fact]
        public async void post()
        {
            var controller = GetWishItemsController();
            var cnt = (await controller.Get(1))?.Count() ?? 0;
            var result = await controller.Post(new WishItem { Text = "test", WishDayId = 1 });
            Assert.True(result != null);
            Assert.True((result as CreatedResult) != null);

            cnt = (await controller.Get(1))?.Count() ?? 0;
            Assert.True(cnt == 1);
        }

        [Fact]
        public async void delete()
        {
            var controller = GetWishItemsController();
            var result = await controller.Post(new WishItem { Text = "test" });
            Assert.True(result != null);
            Assert.True((result as CreatedResult) != null);

            result = await controller.Delete(1);
            Assert.True(result != null);
            Assert.True((result as HttpOkObjectResult) != null);

            var cnt = (await controller.Get(1))?.Count() ?? 0;
            Assert.True(cnt == 0);
        }

        private WishItemsController GetWishItemsController()
        {
            var service = _serviceProvider.GetRequiredService<IWishItemsService>();
            var logger = _serviceProvider.GetRequiredService<ILogger<WishItemsController>>();
            return new WishItemsController(service, logger);
        }
    }
}
