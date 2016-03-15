using Microsoft.Data.Entity;
using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Services
{
    public class MyWishesDbContext: DbContext
    {
        public DbSet<AppTenant> Tenants { get; set; }

        public DbSet<WishDay> WishDays { get; set; }

        public DbSet<WishItem> WishItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
