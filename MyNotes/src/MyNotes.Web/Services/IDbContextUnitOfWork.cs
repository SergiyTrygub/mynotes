using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MyNotes.Web.Services
{
    public interface IDbContextUnitOfWork
    {
        IRepository<NoteDay> NoteDaysRepository { get; }
        IRepository<AppTenant> TenantsRepository { get; }

        Task SaveChangesAsync();
    }

    public class CollectionContext : IDbContextUnitOfWork
    {
        public IRepository<NoteDay> NoteDaysRepository
        {
            get
            {
                return new Repository<NoteDay, int>();
            }
        }

        public IRepository<AppTenant> TenantsRepository
        {
            get
            {
                return new Repository<AppTenant, string>();
            }
        }

        public Task SaveChangesAsync()
        {
            //throw new NotImplementedException();
            return Task.FromResult(0);
        }
    }

    public class Repository<T, K> : IRepository<T> where T : class, IEntity<K>
    {
        private List<T> _noteDays = new List<T>();

        public void Delete(T entity)
        {
            var item = _noteDays.FirstOrDefault(l => l.Id.Equals(entity.Id));
            if (item != null)
            {
                _noteDays.Remove(item);
            }
        }

        public void Delete(object id)
        {
            var item = _noteDays.FirstOrDefault(l => l.Id.Equals(id));
            if (item != null)
            {
                _noteDays.Remove(item);
            }
        }

        public void Insert(T entity)
        {
            var item = _noteDays.FirstOrDefault(l => l.Id.Equals(entity.Id));
            if (item != null)
            {
                _noteDays.Remove(item);
            }
            _noteDays.Add(entity);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query()
        {
            return _noteDays.Where(n => !n.IsDeleted).AsQueryable();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> query)
        {
            return _noteDays.Where(n => !n.IsDeleted).AsQueryable();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
