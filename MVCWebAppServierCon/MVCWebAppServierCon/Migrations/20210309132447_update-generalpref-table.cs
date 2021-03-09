using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class updategeneralpreftable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivitiyTable",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnecWith",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTable",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 9, 15, 24, 46, 859, DateTimeKind.Local).AddTicks(5007));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 9, 15, 24, 46, 864, DateTimeKind.Local).AddTicks(6168));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 9, 15, 24, 46, 864, DateTimeKind.Local).AddTicks(6246));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 9, 15, 24, 46, 864, DateTimeKind.Local).AddTicks(6259));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivitiyTable",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "ConnecWith",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "ProjectTable",
                table: "TblGeneralPreference");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 19, 2, 847, DateTimeKind.Local).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 19, 2, 847, DateTimeKind.Local).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 19, 2, 847, DateTimeKind.Local).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 19, 2, 847, DateTimeKind.Local).AddTicks(8543));
        }
    }
}
