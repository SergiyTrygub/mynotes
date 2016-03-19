using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MyNotes.Web.Repositories;

namespace MyNotes.Web.Migrations
{
    [DbContext(typeof(MyWishesDbContext))]
    [Migration("20160319103420_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
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
                });

            modelBuilder.Entity("MyNotes.Web.Models.WishItem", b =>
                {
                    b.HasOne("MyNotes.Web.Models.WishDay")
                        .WithMany()
                        .HasForeignKey("WishDayId");
                });

            modelBuilder.Entity("MyNotes.Web.Models.WishItemTag", b =>
                {
                    b.HasOne("MyNotes.Web.Models.WishItem")
                        .WithMany()
                        .HasForeignKey("WishItemId");
                });
        }
    }
}
