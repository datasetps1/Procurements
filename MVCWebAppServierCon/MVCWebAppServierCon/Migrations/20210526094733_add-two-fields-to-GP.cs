using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addtwofieldstoGP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Basic_Currency",
                table: "TblGeneralPreference",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Show_Currency_with_item",
                table: "TblGeneralPreference",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 5, 26, 12, 47, 33, 423, DateTimeKind.Local).AddTicks(5992));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 5, 26, 12, 47, 33, 423, DateTimeKind.Local).AddTicks(5992));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 5, 26, 12, 47, 33, 423, DateTimeKind.Local).AddTicks(5992));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 5, 26, 12, 47, 33, 423, DateTimeKind.Local).AddTicks(5992));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Basic_Currency",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Show_Currency_with_item",
                table: "TblGeneralPreference");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 5, 23, 14, 54, 47, 732, DateTimeKind.Local).AddTicks(3339));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 5, 23, 14, 54, 47, 735, DateTimeKind.Local).AddTicks(2491));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 5, 23, 14, 54, 47, 735, DateTimeKind.Local).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 5, 23, 14, 54, 47, 735, DateTimeKind.Local).AddTicks(2535));
        }
    }
}
