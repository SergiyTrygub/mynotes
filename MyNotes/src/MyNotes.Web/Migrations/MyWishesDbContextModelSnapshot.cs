using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyNotes.Web.Repositories;

namespace MyNotes.Web.Migrations
{
    [DbContext(typeof(MyWishesDbContext))]
    partial class MyWishesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyNotes.Web.Models.WishDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatorId");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("TenantId")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("WishDays");
                });

            modelBuilder.Entity("MyNotes.Web.Models.WishItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("IsCompleted");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Position");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("WishDayId");

                    b.HasKey("Id");

                    b.HasIndex("WishDayId");

                    b.ToTable("WishItems");
                });

            modelBuilder.Entity("MyNotes.Web.Models.WishItemTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("WishItemId");

                    b.HasKey("Id");

                    b.HasIndex("WishItemId");

                    b.ToTable("WishItemTags");
                });

            modelBuilder.Entity("MyNotes.Web.MultiTenancy.AppTenant", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("DisplayName");

                    b.Property<string>("IpAddress");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPrivate");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("MyNotes.Web.Models.WishItem", b =>
                {
                    b.HasOne("MyNotes.Web.Models.WishDay")
                        .WithMany()
                        .HasForeignKey("WishDayId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyNotes.Web.Models.WishItemTag", b =>
                {
                    b.HasOne("MyNotes.Web.Models.WishItem")
                        .WithMany()
                        .HasForeignKey("WishItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
