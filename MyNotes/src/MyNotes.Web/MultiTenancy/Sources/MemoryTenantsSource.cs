using System.Collections.Generic;

namespace MyNotes.Web.MultiTenancy.Sources
{
    public class MemoryTenantsSource : ITenantsSource
    {
        public IEnumerable<AppTenant> GetTenants()
        {
            return new List<AppTenant>() {
                new AppTenant { Id = "1", DisplayName="Tenant1" },
                new AppTenant { Id = "2", DisplayName="Tenant2" }
            };
        }
    }
}
