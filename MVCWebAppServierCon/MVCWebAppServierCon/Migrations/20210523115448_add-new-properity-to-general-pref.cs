using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addnewproperitytogeneralpref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Show_ToEmployee",
                table: "TblGeneralPreference",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Show_ToEmployee",
                table: "TblGeneralPreference");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 5, 23, 13, 56, 17, 939, DateTimeKind.Local).AddTicks(5215));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 5, 23, 13, 56, 17, 942, DateTimeKind.Local).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 5, 23, 13, 56, 17, 942, DateTimeKind.Local).AddTicks(5933));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 5, 23, 13, 56, 17, 942, DateTimeKind.Local).AddTicks(5940));
        }
    }
}
