﻿using Microsoft.EntityFrameworkCore;
using MyNotes.Web.Models;
using MyNotes.Web.MultiTenancy;

namespace MyNotes.Web.Repositories
{
    public class MyWishesDbContext : DbContext
    {
        public MyWishesDbContext(DbContextOptions<MyWishesDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppTenant> Tenants { get; set; }

        public DbSet<WishDay> WishDays { get; set; }

        public DbSet<WishItem> WishItems { get; set; }

        public DbSet<WishItemTag> WishItemTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppTenant>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<AppTenant>()
                .Property(p => p.Timestamp)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            modelBuilder.Entity<WishDay>()
                .Property(p => p.TenantId)
                .IsRequired();
            modelBuilder.Entity<WishDay>()
                .Property(p => p.Date)
                .IsRequired();
            modelBuilder.Entity<WishDay>()
                .Property(p => p.Timestamp)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            modelBuilder.Entity<WishItem>()
                .Property(p => p.Position)
                .IsRequired();
            //modelBuilder.Entity<WishItem>()
            //    .Property(p => p.Tags)
            //    .IsRequired();
            modelBuilder.Entity<WishItem>()
                .Property(p => p.Text)
                .IsRequired();
            modelBuilder.Entity<WishItem>()
                .Property(p => p.WishDayId)
                .IsRequired();
            modelBuilder.Entity<WishItem>()
                .Property(p => p.Timestamp)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            modelBuilder.Entity<WishItem>()
                        .HasOne(p => p.WishDay)
                        .WithMany(b => b.WishList)
                        .HasForeignKey(f => f.WishDayId)
                        .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

            modelBuilder.Entity<WishItemTag>()
                .Property(p => p.Text)
                .IsRequired();
            modelBuilder.Entity<WishItemTag>()
                .Property(p => p.WishItemId)
                .IsRequired();
            modelBuilder.Entity<WishItemTag>()
                .Property(p => p.Timestamp)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            //modelBuilder.Entity<WishItemTag>()
            //            .HasOne(p => p.WishItem)
            //            .WithMany(b => b.Tags)
            //            .HasForeignKey(f => f.WishItemId)
            //            .OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
