using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;

namespace MyNotes.Web.Repositories
{
    public class MyWishesUnitOfWorkDbContext : MyWishesUnitOfWorkContextBase
    {
        MyWishesDbContext _dbContext;

        protected MyWishesUnitOfWorkDbContext(
            MyWishesDbContext dbContext,
            IRepository<AppTenant> tenantsRepository, 
            IRepository<WishDay> wishDaysRepository, 
            IRepository<WishItem> wishItemsRepository, 
            IRepository<WishItemTag> wishItemTagsRepository) 
                : base(tenantsRepository, wishDaysRepository, wishItemsRepository, wishItemTagsRepository)
        {
            _dbContext = dbContext;
        }

        public override Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }

    public class DbRepository<T, K> : IRepository<T> where T : class, IEntity<K>
    {
        MyWishesDbContext _dbContext;
        public DbRepository(MyWishesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Delete(object id)
        {
            var item = _dbContext.Set<T>().Where(i => i.Id.Equals(id));
            if (item != null)
            {
                Delete(item);
            }
        }

        public void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().Where(i => !i.IsDeleted);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> query)
        {
            return Query().Where(query);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
