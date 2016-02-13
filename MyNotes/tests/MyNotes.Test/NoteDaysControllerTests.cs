using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Controllers.Api.v1;
using MyNotes.Web.Models;
using MyNotes.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async void get()
        {
            var controller = GetNoteDaysController();
            var result = await controller.Get("test tenant");
            Assert.True(result != null);
        }

        [Fact]
        public async void post()
        {
            var controller = GetNoteDaysController();
            var cnt = (await controller.Get("test tenant"))?.Count() ?? 0;
            var result = await controller.Post(GetNewNoteDay());
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
            var controller = GetNoteDaysController();
            var result = await controller.Post(GetNewNoteDay());
            Assert.True(result != null);
            Assert.True((result as CreatedResult) != null);

            result = await controller.Delete(1);
            Assert.True(result != null);
            Assert.True((result as HttpOkObjectResult) != null);
            
            var cnt = (await controller.Get("testtenant"))?.Count() ?? 0;
            Assert.True(cnt == 0);
        }

        private NoteDaysController GetNoteDaysController()
        {
            var service = _serviceProvider.GetRequiredService<INoteDaysService>();
            var logger = _serviceProvider.GetRequiredService<ILogger<NoteDaysController>>();
            return new NoteDaysController(service, logger);
        }

        private NoteDay GetNewNoteDay()
        {
            return new NoteDay {
                CreatorId = null,
                Date = DateTime.Now,
                Id = 0 + 1,
                IsDeleted = false,
                TenantId = "testtenant",
                Notes = new List<Note> {
                    new Note { Id = 0 }
                }
            };
        }
    }
}
