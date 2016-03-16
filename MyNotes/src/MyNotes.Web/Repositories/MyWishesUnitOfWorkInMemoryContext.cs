using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyNotes.Web.Repositories
{
 
    public class MyWishesUnitOfWorkInMemoryContext : MyWishesUnitOfWorkContextBase
    {
        public MyWishesUnitOfWorkInMemoryContext(
            IRepository<AppTenant> tenantsRepository,
            IRepository<WishDay> wishDaysRepository,
            IRepository<WishItem> wishItemsRepository,
            IRepository<WishItemTag> wishItemTagsRepository) 
                :base(
                     tenantsRepository, 
                     wishDaysRepository, 
                     wishItemsRepository, 
                     wishItemTagsRepository)
        { }
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
