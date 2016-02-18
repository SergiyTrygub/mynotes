using Microsoft.AspNet.Http;
using MyNotes.Web.MultiTenancy.Sources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Web.MultiTenancy.Resolvers
{
    public abstract class BaseTenantResolver : ITenantResolver
    {
        protected readonly IEnumerable<AppTenant> Tenants;

        public BaseTenantResolver(ITenantsSource tenantsSource)
        {
            Tenants = tenantsSource.GetTenants();
        }

        public abstract Task<AppTenant> ResolveAsync(HttpContext context);
    }
}
