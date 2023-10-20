using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PHHWpos.Migrations
{
    public partial class UpdateItemOrderPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder",
                columns: new[] { "ItemsId", "OrdersId" }
            );

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateClosed", "Status" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateClosed", "Status" },
                values: new object[] { null, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder",
                column: "Id"
            );

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateClosed", "Status" },
                values: new object[] { new DateTime(2023, 10, 20, 0, 8, 30, 413, DateTimeKind.Local).AddTicks(8453), true });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateClosed", "Status" },
                values: new object[] { new DateTime(2023, 10, 20, 0, 8, 30, 413, DateTimeKind.Local).AddTicks(8502), true });
        }
    }
}
