using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace MyNotes.Web.MultiTenancy
{
    public class AppTenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IMultiTenancyResolver _tenantResolver;

        public AppTenantMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IMultiTenancyResolver tenantResolver)
        {
            _next = next;
            _tenantResolver = tenantResolver;
            _logger = loggerFactory.CreateLogger<AppTenantMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (_logger.BeginScope("TenantResolverMiddleware"))
            {
                _logger.LogInformation("Starting to get the tenant");
                if (httpContext.Request.Path != new PathString("/"))
                {
                    var tenant = httpContext.GetTenant();
                    if (tenant == null)
                    {
                        tenant = await _tenantResolver.ResolveAsync(httpContext);
                        if (tenant != null)
                        {
                            httpContext.SetTenant(tenant);
                        }
                        else
                        {
                            //httpContext.Response.StatusCode = 404;
                            //return;
                        }
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
