using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;

namespace MyNotes.Web.MultiTenancy
{
    public class AppTenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public AppTenantMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<AppTenantMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (_logger.BeginScope("TenantResolverMiddleware"))
            {
                var tenant = httpContext.GetTenant();
                if (tenant == null)
                {
                    var service = httpContext.RequestServices.GetService(typeof(IMultiTenancyResolver)) as IMultiTenancyResolver;
                    tenant = await service.ResolveAsync(httpContext);
                    if (tenant != null)
                    {
                        httpContext.SetTenant(tenant);
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 404;
                        return;
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
            return builder.UseMiddleware<AppTenantMiddleware>();
        }
    }
}
