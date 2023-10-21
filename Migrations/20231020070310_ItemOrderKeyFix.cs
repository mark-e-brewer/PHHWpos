using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PHHWpos.Migrations
{
    public partial class ItemOrderKeyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the old primary key constraints
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder");

            // Add a new column "Id" and set it as the primary key
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ItemOrder",
                type: "serial", // Use an appropriate data type
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder",
                column: "Id");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the new foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_Items_ItemsId",
                table: "ItemOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_Orders_OrdersId",
                table: "ItemOrder");

            // Drop the new primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder");

            // Add back the old primary key constraints for "ItemsId" and "OrdersId"
            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder",
                columns: new[] { "ItemsId", "OrdersId" });
        }

    }
}
