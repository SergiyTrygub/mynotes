using Microsoft.Data.Entity;
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
        Task<IEnumerable<NoteDay>> GetAsync(string tenantId);
        Task<NoteDay> GetByIdAsync(int id);

        Task<ActionResult> SaveAsync(NoteDay noteDay);
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
                var item = await _dbContext.NoteDaysRepository.Query(n => n.Id == id).FirstOrDefaultAsync();
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

        public async Task<IEnumerable<NoteDay>> GetAsync(string tenantId)
        {
            return Query().Where(n => n.TenantId == tenantId);
        }

        public async Task<NoteDay> GetByIdAsync(int id)
        {
            return Query().FirstOrDefault(n => n.Id == id);
        }

        public async Task<ActionResult> SaveAsync(NoteDay noteDay)
        {
            try
            {
                if (!noteDay.Notes.Any())
                {
                    throw new ArgumentException("No notes created");
                }

                var item = _dbContext.NoteDaysRepository.Query(l => l.Id == noteDay.Id).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.NoteDaysRepository.Delete(item);
                }
                _dbContext.NoteDaysRepository.Insert(noteDay);
                await _dbContext.SaveChangesAsync();

                return ActionResult.Success();
            }
            catch (Exception ex)
            {
                return ActionResult.Failed();
            }
        }

        private IEnumerable<NoteDay> Query()
        {
            return _dbContext.NoteDaysRepository.Query(n => !n.IsDeleted);
        }
    }
}
