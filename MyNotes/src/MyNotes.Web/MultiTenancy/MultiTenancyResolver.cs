using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MyNotes.Web.MultiTenancy.Resolvers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Web.MultiTenancy
{
    public interface IMultiTenancyResolver
    {
        Task<AppTenant> ResolveAsync(HttpContext context);
    }

    public class MultiTenancyResolver : IMultiTenancyResolver
    {
        private IOptions<MultiTenancyOptions> _options;
        public MultiTenancyResolver(IOptions<MultiTenancyOptions> options)
        {
            _options = options;
        }

        public async Task<AppTenant> ResolveAsync(HttpContext context)
        {
            AppTenant tenant = null;
            foreach(var resolver in _options.Value.Resolvers)
            {
                tenant = await resolver.ResolveAsync(context);
                if (tenant != null)
                {
                    break;
                }
            }
            return tenant;
        }
    }

    public class MultiTenancyOptions
    {
        public MultiTenancyOptions()
        {
            Resolvers = new List<ITenantResolver>();
        }
        public List<ITenantResolver> Resolvers { get; private set; }
    }
}
