using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace MyNotes.Web.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatorId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishDay", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AppTenant",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTenant", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "WishItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsCompleted = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(nullable: true),
                    WishDayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishItem_WishDay_WishDayId",
                        column: x => x.WishDayId,
                        principalTable: "WishDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "WishItemTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(nullable: true),
                    WishItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishItemTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishItemTag_WishItem_WishItemId",
                        column: x => x.WishItemId,
                        principalTable: "WishItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("WishItemTag");
            migrationBuilder.DropTable("AppTenant");
            migrationBuilder.DropTable("WishItem");
            migrationBuilder.DropTable("WishDay");
        }
    }
}
