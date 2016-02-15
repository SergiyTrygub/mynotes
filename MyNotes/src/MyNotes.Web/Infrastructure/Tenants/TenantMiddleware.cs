using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using MyNotes.Web.Models;
using Microsoft.Extensions.Logging;

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
        }

        public Task Invoke(HttpContext httpContext)
        {
            using (_logger.BeginScope("TenantResolverMiddleware"))
            {
                httpContext.Features[typeof(TenantInfo)] = new TenantInfo();
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TenantMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
