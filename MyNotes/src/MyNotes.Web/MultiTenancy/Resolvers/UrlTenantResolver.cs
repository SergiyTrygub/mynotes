using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.MultiTenancy.Resolvers
{
    public class UrlTenantResolver : BaseTenantResolver
    {
        public override Task<AppTenant> ResolveAsync(HttpContext context)
        {
            var tenant = Tenants.FirstOrDefault(t => (context.Request.Path.Value?.Contains(t.Id) ?? false) || 
                                                    context.Request.Path.StartsWithSegments(new PathString("/" + t.DisplayName)));
            return Task.FromResult(tenant);
        }
    }
}
