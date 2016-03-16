using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using System.Threading.Tasks;

namespace MyNotes.Web.Repositories
{
    public abstract class MyWishesUnitOfWorkContextBase : IUnitOfWorkContext
    {
        protected MyWishesUnitOfWorkContextBase(
             IRepository<AppTenant> tenantsRepository,
             IRepository<WishDay> wishDaysRepository,
             IRepository<WishItem> wishItemsRepository,
             IRepository<WishItemTag> wishItemTagsRepository)
        {
            TenantsRepository = tenantsRepository;
            WishDaysRepository = wishDaysRepository;
            WishItemsRepository = wishItemsRepository;
            WishItemTagsRepository = wishItemTagsRepository;
        }

        public IRepository<AppTenant> TenantsRepository { get; private set; }

        public IRepository<WishDay> WishDaysRepository { get; private set; }

        public IRepository<WishItem> WishItemsRepository { get; private set; }

        public IRepository<WishItemTag> WishItemTagsRepository { get; private set; }

        public virtual Task SaveChangesAsync()
        {
            return Task.FromResult(0);
        }
    }
}
