using System.Collections.Generic;

namespace MyNotes.Web.MultiTenancy.Sources
{
    public interface ITenantsSource
    {
        IEnumerable<AppTenant> GetTenants();
    }
}
