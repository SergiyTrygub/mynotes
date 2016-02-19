using Microsoft.AspNet.Http;
using MyNotes.Web.MultiTenancy.Sources;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.MultiTenancy.Resolvers
{
    public class IpAddrTenantResolver : BaseTenantResolver
    {
        public override Task<AppTenant> ResolveAsync(HttpContext context)
        {
            var tenant = Tenants.FirstOrDefault(t =>  context.Request.Host.Value == t.IpAddress);
            return Task.FromResult(tenant);
        }
    }
}
