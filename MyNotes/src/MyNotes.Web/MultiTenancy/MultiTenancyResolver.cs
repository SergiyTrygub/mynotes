using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;
using MyNotes.Web.MultiTenancy.Resolvers;
using MyNotes.Web.MultiTenancy.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
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
