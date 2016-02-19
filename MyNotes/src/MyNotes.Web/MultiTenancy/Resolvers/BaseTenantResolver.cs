using Microsoft.AspNet.Http;
using MyNotes.Web.MultiTenancy.Sources;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace MyNotes.Web.MultiTenancy.Resolvers
{
    public abstract class BaseTenantResolver : ITenantResolver
    {
        protected IEnumerable<AppTenant> Tenants {
            get
            {
                var tenants = new List<AppTenant>();
                foreach(var source in TenantsSources)
                {
                    tenants.AddRange(source.GetTenants());
                }
                return tenants;
            }
        }

        public IEnumerable<ITenantsSource> TenantsSources { get; set; }

        public abstract Task<AppTenant> ResolveAsync(HttpContext context);
    }
}
