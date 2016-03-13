﻿using Microsoft.Data.Entity;
using MyNotes.Web.Infrastructure;
using MyNotes.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Services
{
    public interface INoteDaysService
    {
        Task<IEnumerable<WishDay>> GetAsync(string tenantId);
        Task<WishDay> GetWishDayAsync(string tenantId, DateTime date);

        Task<ActionResult> SaveAsync(string tenantId, WishDay noteDay);
        Task<ActionResult> DeleteAsync(int id);
    }

    public class NoteDaysService : INoteDaysService
    {
        IDbContextUnitOfWork _dbContext;
        public NoteDaysService(IDbContextUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var item = _dbContext.NoteDaysRepository.Query(n => n.Id == id).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.NoteDaysRepository.Delete(item);
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
            return Query().Where(n => n.TenantId == tenantId);
        }

        public async Task<WishDay> GetWishDayAsync(string tenantId, DateTime date)
        {
            return Query().FirstOrDefault(n => n.Date == date);
        }

        public async Task<ActionResult> SaveAsync(string tenantId, WishDay wishDay)
        {
            try
            {
                wishDay.TenantId = tenantId;
                var item = _dbContext.NoteDaysRepository.Query(l => l.Id == wishDay.Id).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.NoteDaysRepository.Delete(item);
                }
                _dbContext.NoteDaysRepository.Insert(wishDay);
                await _dbContext.SaveChangesAsync();

                return ActionResult.Success(wishDay);
            }
            catch (Exception ex)
            {
                return ActionResult.Failed();
            }
        }

        private IEnumerable<WishDay> Query()
        {
            return _dbContext.NoteDaysRepository.Query(n => !n.IsDeleted);
        }
    }
}
