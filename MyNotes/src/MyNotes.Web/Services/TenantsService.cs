using Microsoft.AspNet.Mvc;
using MyNotes.Web.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Services
{
    public interface ITenantsService
    {
        Task<IEnumerable<AppTenant>> GetAsync();
        Task<AppTenant> GetByIdAsync(string Id);
        Task<ActionResult> CreateAsync();
        Task<ActionResult> DeleteAsync(string Id);
    }

    public class TenantsService : ITenantsService
    {
        public Task<ActionResult> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> DeleteAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppTenant>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppTenant> GetByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
