using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class updateitemtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit_Id",
                table: "TblItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 39, 10, 820, DateTimeKind.Local).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 39, 10, 824, DateTimeKind.Local).AddTicks(5657));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 39, 10, 824, DateTimeKind.Local).AddTicks(5719));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 39, 10, 824, DateTimeKind.Local).AddTicks(5728));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit_Id",
                table: "TblItem");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 6, 27, 97, DateTimeKind.Local).AddTicks(8965));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 6, 27, 102, DateTimeKind.Local).AddTicks(2832));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 6, 27, 102, DateTimeKind.Local).AddTicks(2871));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 6, 27, 102, DateTimeKind.Local).AddTicks(2878));
        }
    }
}
