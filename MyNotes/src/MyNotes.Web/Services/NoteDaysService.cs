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
        private List<NoteDay> _noteDays = new List<NoteDay>();

        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var item = _noteDays.FirstOrDefault(l => l.Id == id);
                if (item != null)
                {
                    _noteDays.Remove(item);
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
                var item = _noteDays.FirstOrDefault(l => l.Id == noteDay.Id);
                if (item != null)
                {
                    _noteDays.Remove(item);
                }
                _noteDays.Add(noteDay);

                return ActionResult.Success();
            }
            catch (Exception ex)
            {
                return ActionResult.Failed();
            }
        }

        private IEnumerable<NoteDay> Query()
        {
            return _noteDays.Where(n => !n.IsDeleted);
        }
    }
}
