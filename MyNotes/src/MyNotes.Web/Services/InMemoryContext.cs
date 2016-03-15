using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyNotes.Web.Services
{
    public class InMemoryCollectionContext : IDbContextUnitOfWork
    {
        private InMemoryRepository<WishDay, int> _noteDaysRepository;
        public IRepository<WishDay> WishDaysRepository
        {
            get
            {
                if (_noteDaysRepository == null)
                {
                    _noteDaysRepository = new InMemoryRepository<WishDay, int>();
                }
                return _noteDaysRepository;
            }
        }

        private InMemoryRepository<AppTenant, string> _tenantRepository;
        public IRepository<AppTenant> TenantsRepository
        {
            get
            {
                if (_tenantRepository == null)
                {
                    _tenantRepository = new InMemoryRepository<AppTenant, string>();
                }
                return _tenantRepository;
            }
        }

        private InMemoryRepository<WishItem, int> _wishItemsRepository;
        public IRepository<WishItem> WishItemsRepository
        {
            get
            {
                if (_wishItemsRepository == null)
                {
                    _wishItemsRepository = new InMemoryRepository<WishItem, int>();
                }
                return _wishItemsRepository;
            }
        }

        public Task SaveChangesAsync()
        {
            //throw new NotImplementedException();
            return Task.FromResult(0);
        }
    }

    public class InMemoryRepository<T, K> : IRepository<T> where T : class, IEntity<K>
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
