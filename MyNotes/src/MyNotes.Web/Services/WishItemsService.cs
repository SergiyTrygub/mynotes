using MyNotes.Web.Infrastructure;
using MyNotes.Web.Models;
using MyNotes.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Services
{
    public interface IWishItemsService
    {
        Task<IEnumerable<WishItem>> GetAsync(int dayId);
        Task<WishItem> GetByIdAsync(int id);

        Task<ActionResult> SaveAsync(WishItem wishItem);
        Task<ActionResult> DeleteAsync(int id);
    }

    public class WishItemsService : IWishItemsService
    {
        IUnitOfWorkContext _dbContext;
        public WishItemsService(IUnitOfWorkContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var item = _dbContext.WishItemsRepository.Query(n => n.Id == id).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.WishItemsRepository.Delete(item);
                    await _dbContext.SaveChangesAsync();
                }
                return ActionResult.Success();
            }
            catch (Exception ex)
            {
                return ActionResult.Failed(ex);
            }
        }

        public async Task<IEnumerable<WishItem>> GetAsync(int dayId)
        {
            return Query().Where(n => n.WishDayId == dayId);
        }

        public async Task<WishItem> GetByIdAsync(int id)
        {
            return Query().FirstOrDefault(n => n.Id == id);
        }

        public async Task<ActionResult> SaveAsync(WishItem wishItem)
        {
            try
            {
                var item = _dbContext.WishItemsRepository.Query(l => l.Id == wishItem.Id).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.WishDaysRepository.Delete(item);
                }
                _dbContext.WishItemsRepository.Insert(wishItem);
                await _dbContext.SaveChangesAsync();

                return ActionResult.Success(wishItem);
            }
            catch (Exception ex)
            {
                return ActionResult.Failed();
            }
        }

        private IEnumerable<WishItem> Query()
        {
            return _dbContext.WishItemsRepository.Query(n => !n.IsDeleted);
        }
    }
}
