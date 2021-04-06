using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class editgeneralpreferncetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Display_Name_Doner2",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Display_Name_cost3",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Display_Name_cost4",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Show_Doner2",
                table: "TblGeneralPreference",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Show_Unit",
                table: "TblGeneralPreference",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Show_cost3",
                table: "TblGeneralPreference",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Show_cost4",
                table: "TblGeneralPreference",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Table_Name_Doner2",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Table_Name_cost3",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Table_Name_cost4",
                table: "TblGeneralPreference",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 6, 10, 28, 53, 982, DateTimeKind.Local).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 6, 10, 28, 53, 985, DateTimeKind.Local).AddTicks(5660));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 6, 10, 28, 53, 985, DateTimeKind.Local).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 6, 10, 28, 53, 985, DateTimeKind.Local).AddTicks(5706));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Display_Name_Doner2",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Display_Name_cost3",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Display_Name_cost4",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Show_Doner2",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Show_Unit",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Show_cost3",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Show_cost4",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Table_Name_Doner2",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Table_Name_cost3",
                table: "TblGeneralPreference");

            migrationBuilder.DropColumn(
                name: "Table_Name_cost4",
                table: "TblGeneralPreference");

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
    }
}
