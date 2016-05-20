using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Controllers.Api.v1;
using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using MyNotes.Web.Repositories;
using MyNotes.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyNotes.Test
{
    public class WishDaysControllerTests
    {
        private readonly IServiceProvider _serviceProvider;

        public WishDaysControllerTests()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();

            services.AddDbContext<MyWishesDbContext>(b => b.UseInMemoryDatabase().UseInternalServiceProvider(efServiceProvider));

            services.AddLogging();
            services.AddTransient<IRepository<AppTenant>, DbRepository<AppTenant, string>>();
            services.AddTransient<IRepository<WishDay>, DbRepository<WishDay, int>>();
            services.AddTransient<IRepository<WishItem>, DbRepository<WishItem, int>>();
            services.AddTransient<IRepository<WishItemTag>, DbRepository<WishItemTag, int>>();
            services.AddTransient<IUnitOfWorkContext, MyWishesUnitOfWorkDbContext>();

            services.AddTransient<IUnitOfWorkContext, MyWishesUnitOfWorkDbContext>();
            services.AddTransient<IWishDaysService, WishDaysService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async void get()
        {
            var controller = GetWishDaysController();
            var result = await controller.Get("test tenant");
            Assert.True(result != null);
        }

        [Fact]
        public async void post()
        {
            var controller = GetWishDaysController();
            var cnt = (await controller.Get("test tenant"))?.Count() ?? 0;
            var result = await controller.Post("testtenant", GetNewNoteDay());
            Assert.True(result != null);
            Assert.True((result as CreatedResult) != null);

            cnt = (await controller.Get("test tenant"))?.Count() ?? 0;
            Assert.True(cnt == 0);

            cnt = (await controller.Get("testtenant"))?.Count() ?? 0;
            Assert.True(cnt == 1);
        }

        [Fact]
        public async void delete()
        {
            var controller = GetWishDaysController();
            var result = await controller.Post("test tenant", GetNewNoteDay());
            Assert.True(result != null);
            Assert.True((result as CreatedResult) != null);

            result = await controller.Delete(1);
            Assert.True(result != null);
            Assert.True((result as OkObjectResult) != null);
            
            var cnt = (await controller.Get("testtenant"))?.Count() ?? 0;
            Assert.True(cnt == 0);
        }

        private WishDaysController GetWishDaysController()
        {
            var service = _serviceProvider.GetRequiredService<IWishDaysService>();
            var logger = _serviceProvider.GetRequiredService<ILogger<WishDaysController>>();
            return new WishDaysController(service, logger);
        }

        private WishDay GetNewNoteDay()
        {
            return new WishDay {
                CreatorId = null,
                Date = DateTime.Now,
                Id = 0 + 1,
                IsDeleted = false,
                TenantId = "testtenant",
                WishList = new List<WishItem> {
                    new WishItem { Id = 0 }
                }
            };
        }
    }
}
