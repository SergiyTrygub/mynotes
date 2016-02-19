using Microsoft.AspNet.Http;
using MyNotes.Web.MultiTenancy.Sources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Web.MultiTenancy.Resolvers
{
    public interface ITenantResolver
    {
        IEnumerable<ITenantsSource> TenantsSources { get; set; }

        Task<AppTenant> ResolveAsync(HttpContext context);
    }
}
