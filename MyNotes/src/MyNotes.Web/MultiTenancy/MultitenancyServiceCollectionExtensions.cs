﻿using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using MyNotes.Web.MultiTenancy.Resolvers;
using MyNotes.Web.MultiTenancy.Sources;

namespace MyNotes.Web.MultiTenancy
{
    public static class MultitenancyServiceCollectionExtensions
    {
        public static IServiceCollection AddMultitenancy<TResolver>(this IServiceCollection services) 
            where TResolver : class, IMultiTenancyResolver
        {
            //Ensure.Argument.NotNull(services, nameof(services));

            services.AddScoped<IMultiTenancyResolver, TResolver>();

            // Make Tenant and TenantContext injectable
            services.AddScoped(prov => 
                prov.GetService<IHttpContextAccessor>()?.HttpContext?.GetTenant());

            // Ensure caching is available for caching resolvers
            services.AddCaching();

            return services;
        }
    }
}
