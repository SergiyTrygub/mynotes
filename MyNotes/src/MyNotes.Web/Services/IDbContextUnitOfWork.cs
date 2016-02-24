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
        private Repository<NoteDay, int> _noteDaysRepository;
        public IRepository<NoteDay> NoteDaysRepository
        {
            get
            {
                if (_noteDaysRepository == null)
                {
                    _noteDaysRepository = new Repository<NoteDay, int>();
                }
                return _noteDaysRepository;
            }
        }

        private Repository<AppTenant, string> _tenantRepository;
        public IRepository<AppTenant> TenantsRepository
        {
            get
            {
                if (_tenantRepository == null)
                {
                    _tenantRepository = new Repository<AppTenant, string>();
                }
                return _tenantRepository;
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
