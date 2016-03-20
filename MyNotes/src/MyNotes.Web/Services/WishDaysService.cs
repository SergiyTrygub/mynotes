using Microsoft.Data.Entity;
using MyNotes.Web.Infrastructure;
using MyNotes.Web.Models;
using MyNotes.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Services
{
    public interface IWishDaysService
    {
        Task<IEnumerable<WishDay>> GetAsync(string tenantId);
        Task<WishDay> GetWishDayAsync(string tenantId, DateTime date);

        Task<ActionResult> SaveAsync(string tenantId, WishDay noteDay);
        Task<ActionResult> DeleteAsync(int id);
    }

    public class WishDaysService : IWishDaysService
    {
        IUnitOfWorkContext _dbContext;
        public WishDaysService(IUnitOfWorkContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var item = _dbContext.WishDaysRepository.Query(n => n.Id == id).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.WishDaysRepository.Delete(item);
                    await _dbContext.SaveChangesAsync();
                }
                return ActionResult.Success();
            }
            catch (Exception ex)
            {
                return ActionResult.Failed(ex);
            }
        }

        public async Task<IEnumerable<WishDay>> GetAsync(string tenantId)
        {
            return _dbContext.WishDaysRepository.Query().Include(w => w.WishList).Where(n => n.TenantId == tenantId);
        }

        public async Task<WishDay> GetWishDayAsync(string tenantId, DateTime date)
        {
            return await _dbContext.WishDaysRepository.Query().Include(w => w.WishList).FirstOrDefaultAsync(n => n.Date.Date == date.Date);
        }

        public async Task<ActionResult> SaveAsync(string tenantId, WishDay wishDay)
        {
            try
            {
                wishDay.TenantId = tenantId;
                var item = _dbContext.WishDaysRepository.Query(l => l.Id == wishDay.Id).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.WishDaysRepository.Update(item);
                }
                else
                {
                    _dbContext.WishDaysRepository.Insert(wishDay);
                }
                await _dbContext.SaveChangesAsync();

                return ActionResult.Success(wishDay);
            }
            catch (Exception ex)
            {
                return ActionResult.Failed();
            }
        }
    }
}
