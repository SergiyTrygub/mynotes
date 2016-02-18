using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace MyNotes.Web.MultiTenancy.Resolvers
{
    public interface ITenantResolver
    {
        Task<AppTenant> ResolveAsync(HttpContext context);
    }
}
