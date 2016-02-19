using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.Web;
using MyNotes.Web.MultiTenancy;
using MyNotes.Web.MultiTenancy.Resolvers;
using MyNotes.Web.MultiTenancy.Sources;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MyNotes.Test
{
    public class MultitenancyTests
    {
        [Fact]
        public async Task Should_give_200_Response()
        {
            var builder = TestServer.CreateBuilder()
                .UseServices(services => {
                    services.AddLogging();

                    services.AddMultitenancy<MultiTenancyResolver>().Configure<MultiTenancyOptions>(opt =>
                    {
                        opt.Resolvers.Add(new UrlTenantResolver() { TenantsSources = new[] { new MemoryTenantsSource() } });
                    });
                }).UseStartup(app => {
                    app.UseMultiTenancy();
                });
            var server = new TestServer(builder);
            var client = server.CreateClient();
            client.BaseAddress = new System.Uri("http://localhost:49630/");
            var request = new HttpRequestMessage(HttpMethod.Get, "/1");

            var result = await client.SendAsync(request);
            Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
