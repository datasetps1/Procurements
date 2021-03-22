using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addcompanydefinitiontogeneralpreftable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company_Logo",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company_Name",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 18, 12, 17, 44, 971, DateTimeKind.Local).AddTicks(5955));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 18, 12, 17, 44, 976, DateTimeKind.Local).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 18, 12, 17, 44, 976, DateTimeKind.Local).AddTicks(6742));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 18, 12, 17, 44, 976, DateTimeKind.Local).AddTicks(6750));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_Logo",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Company_Name",
                table: "TblGeneralPreference");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 14, 10, 40, 5, 123, DateTimeKind.Local).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 14, 10, 40, 5, 126, DateTimeKind.Local).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 14, 10, 40, 5, 126, DateTimeKind.Local).AddTicks(5627));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 14, 10, 40, 5, 126, DateTimeKind.Local).AddTicks(5633));
        }
    }
}
