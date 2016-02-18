using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using MyNotes.Web.MultiTenancy;
using MyNotes.Web.MultiTenancy.Resolvers;

namespace MyNotes.Web.Infrastructure.Tenants
{
    // You may need to install the Microsoft.AspNet.Http.Abstractions package into your project
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public TenantMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<TenantMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (_logger.BeginScope("TenantResolverMiddleware"))
            {
                var tenant = httpContext.GetTenant();
                if (tenant == null)
                {
                    var service = httpContext.RequestServices.GetService(typeof(ITenantResolver)) as ITenantResolver;
                    tenant = await service.ResolveAsync(httpContext);
                    if (tenant != null)
                    {
                        httpContext.SetTenant(tenant);
                    }
                }
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TenantMiddlewareExtensions
    {
        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
