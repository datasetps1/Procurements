using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class connectorderwithpriceheader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderHeaderCode",
                table: "SalesQouteHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 1, 10, 52, 50, 961, DateTimeKind.Local).AddTicks(2264));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 1, 10, 52, 50, 964, DateTimeKind.Local).AddTicks(8669));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 1, 10, 52, 50, 964, DateTimeKind.Local).AddTicks(8712));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 1, 10, 52, 50, 964, DateTimeKind.Local).AddTicks(8719));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderHeaderCode",
                table: "SalesQouteHeader");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 21, 9, 26, 32, 187, DateTimeKind.Local).AddTicks(7715));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 21, 9, 26, 32, 191, DateTimeKind.Local).AddTicks(2773));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 21, 9, 26, 32, 191, DateTimeKind.Local).AddTicks(2809));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 21, 9, 26, 32, 191, DateTimeKind.Local).AddTicks(2815));
        }
    }
}
