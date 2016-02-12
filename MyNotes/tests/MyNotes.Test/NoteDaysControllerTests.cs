using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyNotes.Test
{
    public class NoteDaysControllerTests
    {
        private readonly TestServer _server;

        public NoteDaysControllerTests()
        {
            _server = CreateServer();
        }

        [Fact]
        public async void get_many_empty()
        {
            using (var client = _server.CreateClient().AcceptJson())
            {
                var response = await client.GetAsync("/api/habits/");
                var result = await response.Content.ReadAsStringAsync();// ReadAsJsonAsync<List<NoteDay>>();

                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }

        private static TestServer CreateServer(Action<IServiceCollection> configureServices = null, Func<HttpContext, Task> testpath = null, Uri baseAddress = null)
        {
            var builder = new WebHostBuilder()
                .UseStartup<MyNotes.Web.Startup>();
            var server = new TestServer(builder);
            server.BaseAddress = baseAddress;
            return server;
        }
    }
}
