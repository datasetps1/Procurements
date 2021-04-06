using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class editorderheadertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cost3",
                table: "TblOrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cost4",
                table: "TblOrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Doner",
                table: "TblOrderHeader",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 6, 10, 17, 35, 85, DateTimeKind.Local).AddTicks(9727));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 6, 10, 17, 35, 90, DateTimeKind.Local).AddTicks(4579));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 6, 10, 17, 35, 90, DateTimeKind.Local).AddTicks(4645));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 6, 10, 17, 35, 90, DateTimeKind.Local).AddTicks(4656));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost3",
                table: "TblOrderHeader");

            migrationBuilder.DropColumn(
                name: "Cost4",
                table: "TblOrderHeader");

            migrationBuilder.DropColumn(
                name: "Doner",
                table: "TblOrderHeader");

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
    }
}
