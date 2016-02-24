using MyNotes.Web.Infrastructure;
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
        private readonly IDbContextUnitOfWork _dbContext;

        public TenantsService(IDbContextUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> CreateAsync()
        {
            try
            {
                AppTenant tenant = new AppTenant { Id = Guid.NewGuid().ToString() };
                _dbContext.TenantsRepository.Insert(tenant);
                await _dbContext.SaveChangesAsync();

                return ActionResult.Success(tenant);
            }
            catch(Exception ex)
            {
                return ActionResult.Failed(ex);
            }
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
