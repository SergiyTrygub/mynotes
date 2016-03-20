using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyNotes.Web.Repositories
{
    public class MyWishesUnitOfWorkDbContext : MyWishesUnitOfWorkContextBase
    {
        MyWishesDbContext _dbContext;

        public MyWishesUnitOfWorkDbContext(
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
            var item = _dbContext.Set<T>().FirstOrDefault(i => i.Id.Equals(id));
            if (item != null)
            {
                _dbContext.Set<T>().Remove(item);
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
            if (entity == null)
                return ;

            T existing = _dbContext.Set<T>().SingleOrDefault(i => i.Id.Equals(entity.Id));
            if (existing != null)
            {
                var dbProps = _dbContext.Entry(existing).Metadata.GetProperties();
                foreach(var dbp in dbProps)
                {
                    var p = entity.GetType().GetTypeInfo().GetDeclaredProperty(dbp.Name);
                    if (p!= null)
                    {
                        var newVal = p.GetValue(entity);
                        if (_dbContext.Entry(existing).Property(dbp.Name) != null &&
                            _dbContext.Entry(existing).Property(dbp.Name).CurrentValue != null && 
                            !_dbContext.Entry(existing).Property(dbp.Name).CurrentValue.Equals(newVal))
                        {
                            _dbContext.Entry(existing).Property(p.Name).CurrentValue = newVal;
                            _dbContext.Entry(existing).Property(p.Name).IsModified = true;
                            _dbContext.Entry(existing).State = Microsoft.Data.Entity.EntityState.Modified;
                        }
                    }
                }
            }
        }
    }

    public static class MyWishesUnitOfWorkDbContextExtensions
    {
        public static void AddMyWishesDbContext(this IServiceCollection services)
        {
            services.AddTransient<IRepository<AppTenant>, DbRepository<AppTenant, string>>();
            services.AddTransient<IRepository<WishDay>, DbRepository<WishDay, int>>();
            services.AddTransient<IRepository<WishItem>, DbRepository<WishItem, int>>();
            services.AddTransient<IRepository<WishItemTag>, DbRepository<WishItemTag, int>>();
            services.AddTransient<IUnitOfWorkContext, MyWishesUnitOfWorkDbContext>();
        }
    }

}
