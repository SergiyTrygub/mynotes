using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Controllers.Api.v1;
using MyNotes.Web.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyNotes.Test
{
    public class NoteDaysControllerTests
    {
        private readonly IServiceProvider _serviceProvider;

        public NoteDaysControllerTests()
        {
            var services = new ServiceCollection();

            services.AddLogging();
            services.AddSingleton<INoteDaysService, NoteDaysService>();
            //services.AddInstance<IUserContextService>(new FakeUserContextService(Guid.NewGuid()));

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async void get()
        {
            var service = _serviceProvider.GetRequiredService<INoteDaysService>();
            var logger = _serviceProvider.GetRequiredService<ILogger<NoteDaysController>>();
            var controller = new NoteDaysController(service, logger);
            var result = await controller.Get("test tenant");
            Assert.True(result != null);
        }

        [Fact]
        public async void post()
        {
            var service = _serviceProvider.GetRequiredService<INoteDaysService>();
            var logger = _serviceProvider.GetRequiredService<ILogger<NoteDaysController>>();
            var controller = new NoteDaysController(service, logger);
            var cnt = (await controller.Get("test tenant"))?.Count() ?? 0;
            var result = await controller.Post(new Web.Models.NoteDay { CreatorId = null, Date = DateTime.Now, Id = cnt + 1, IsDeleted = false, TenantId = "testtenant" });
            Assert.True(result != null);

            cnt = (await controller.Get("test tenant"))?.Count() ?? 0;
            Assert.True(cnt == 0);

            cnt = (await controller.Get("testtenant"))?.Count() ?? 0;
            Assert.True(cnt == 1);
        }
    }
}
