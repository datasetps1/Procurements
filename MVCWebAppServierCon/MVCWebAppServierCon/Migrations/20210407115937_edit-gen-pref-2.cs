using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class editgenpref2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Display_Name_Activityt",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Display_Name_Project",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 7, 14, 59, 36, 60, DateTimeKind.Local).AddTicks(8849));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 7, 14, 59, 36, 64, DateTimeKind.Local).AddTicks(5837));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 7, 14, 59, 36, 64, DateTimeKind.Local).AddTicks(5892));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 7, 14, 59, 36, 64, DateTimeKind.Local).AddTicks(5901));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Display_Name_Activityt",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Display_Name_Project",
                table: "TblGeneralPreference");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 7, 14, 58, 24, 902, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 7, 14, 58, 24, 907, DateTimeKind.Local).AddTicks(2566));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 7, 14, 58, 24, 907, DateTimeKind.Local).AddTicks(2636));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 7, 14, 58, 24, 907, DateTimeKind.Local).AddTicks(2804));
        }
    }
}
