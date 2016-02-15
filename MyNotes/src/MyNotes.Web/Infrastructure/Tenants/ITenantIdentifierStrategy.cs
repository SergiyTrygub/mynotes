using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Infrastructure.Tenants
{
    public interface ITenantIdentifierStrategy
    {
        string GetCurrentTenant(HttpContext context);
    }
}
