using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHHWpos.Migrations
{
    public partial class UpdateUIDToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UID",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateClosed", "Type" },
                values: new object[] { new DateTime(2023, 10, 8, 16, 30, 43, 655, DateTimeKind.Local).AddTicks(6418), "Phone" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateClosed", "Type" },
                values: new object[] { new DateTime(2023, 10, 8, 16, 30, 43, 655, DateTimeKind.Local).AddTicks(6454), "In-Person" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UID",
                value: "b1s92873y7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "UID",
                value: "n0c2389o4n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UID",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateClosed", "Type" },
                values: new object[] { new DateTime(2023, 10, 8, 15, 44, 25, 763, DateTimeKind.Local).AddTicks(9874), "Dine-In" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateClosed", "Type" },
                values: new object[] { new DateTime(2023, 10, 8, 15, 44, 25, 763, DateTimeKind.Local).AddTicks(9913), "Takeout" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UID",
                value: 1001);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "UID",
                value: 1002);
        }
    }
}
