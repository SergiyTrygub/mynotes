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
        IRepository<WishDay> WishDaysRepository { get; }
        IRepository<WishItem> WishItemsRepository { get; }
        IRepository<AppTenant> TenantsRepository { get; }

        Task SaveChangesAsync();
    }
}
