using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.Web;
using MyNotes.Web.MultiTenancy;
using MyNotes.Web.MultiTenancy.Resolvers;
using MyNotes.Web.MultiTenancy.Sources;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MyNotes.Test
{
    public class MultitenancyTests
    {
        [Fact]
        public async Task root_url_should_give_200_response()
        {
            var server = CreateServer(baseAddress: new Uri("http://localhost:5000/"));
            var client = server.CreateClient();
            var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/"));
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Should_give_200_Response()
        {
            var server = CreateServer(baseAddress: new Uri("http://localhost:5000/"));
            var client = server.CreateClient();
            var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/1"));
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Should_give_404_Response()
        {
            var server = CreateServer(baseAddress: new Uri("http://localhost:5000/"));
            var client = server.CreateClient();
            var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/555"));
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }

        private static TestServer CreateServer(Action<IServiceCollection> configureServices = null, Func<HttpContext, Task> testpath = null, Uri baseAddress = null)
        {
            var server = TestServer.Create(app =>
            {
                app.UseMultiTenancy();
                app.Use(async (context, next) =>
                {
                    var req = context.Request;
                    var res = context.Response;
                    var tenant = context.GetTenant();
                    if (req.Path == new PathString("/") || tenant != null)
                    {
                        res.StatusCode = 200;                        
                    }
                    else
                    {
                        await next();
                    }
                });
            },
            services =>
            {
                services.AddLogging();
                services.AddMultitenancy<MultiTenancyResolver>().Configure<MultiTenancyOptions>(opt =>
                {
                    opt.Resolvers.Add(new UrlTenantResolver() { TenantsSources = new[] { new MemoryTenantsSource() } });
                });

                if (configureServices != null)
                {
                    configureServices(services);
                }
            });
            server.BaseAddress = baseAddress;
            return server;
        }
    }
}
