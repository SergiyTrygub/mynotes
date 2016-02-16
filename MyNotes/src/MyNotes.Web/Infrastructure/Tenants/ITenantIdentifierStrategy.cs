using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Infrastructure.Tenants
{
    public interface ITenantResolver
    {
        TenantInfo GetCurrentTenant(HttpContext context);
    }

    public class UrlTenantResolver : ITenantResolver
    {
        public TenantInfo GetCurrentTenant(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class IpAddrTenantResolver : ITenantResolver
    {
        public TenantInfo GetCurrentTenant(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }

    public interface ITenantResolverBuilder
    {
        ITenantResolverBuilder AddResolver<TResolver>() where TResolver : ITenantResolver, new();
    }

    public class TenantResolverBuilder : ITenantResolverBuilder
    {
        private List<ITenantResolver> _resolvers = new List<ITenantResolver>();

        public ITenantResolverBuilder AddResolver<TResolver>() where TResolver : ITenantResolver, new()
        {
            _resolvers.Add(new TResolver());
            return this;
            
        }
    }

    //public static class TenantResolverBuilder
    //{
    //    public static IServiceCollection AddTenantResolver(this IServiceCollection services, ITenantResolver resolver)
    //    {
    //        //services.Add()
    //        return services;
    //    }
    //}
}
