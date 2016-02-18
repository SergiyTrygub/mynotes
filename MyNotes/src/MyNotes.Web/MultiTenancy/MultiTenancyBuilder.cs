using MyNotes.Web.MultiTenancy.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.MultiTenancy
{
    public class MultiTenancyBuilder
    {
        public MultiTenancyBuilder()
        {
            Resolvers = new List<ITenantResolver>();
        }

        public List<ITenantResolver> Resolvers { get; private set; }

        public MultiTenancyBuilder AddResolver<TResolver>() where TResolver : ITenantResolver, new()
        {
            Resolvers.Add(new TResolver());
            return this;
        }
    }
}
