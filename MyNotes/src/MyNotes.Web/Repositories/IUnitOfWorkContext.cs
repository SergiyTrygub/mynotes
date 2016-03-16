using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MyNotes.Web.Repositories
{
    public interface IUnitOfWorkContext
    {
        IRepository<WishDay> WishDaysRepository { get; }
        IRepository<WishItem> WishItemsRepository { get; }
        IRepository<WishItemTag> WishItemTagsRepository { get; }
        IRepository<AppTenant> TenantsRepository { get; }

        Task SaveChangesAsync();
    }
}
