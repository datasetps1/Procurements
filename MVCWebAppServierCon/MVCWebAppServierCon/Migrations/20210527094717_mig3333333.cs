using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class mig3333333 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderHeaderCurrencey",
                table: "TblOrderHeader",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 5, 27, 12, 47, 16, 938, DateTimeKind.Local).AddTicks(5629));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 5, 27, 12, 47, 16, 938, DateTimeKind.Local).AddTicks(5629));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 5, 27, 12, 47, 16, 938, DateTimeKind.Local).AddTicks(5629));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 5, 27, 12, 47, 16, 938, DateTimeKind.Local).AddTicks(5629));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderHeaderCurrencey",
                table: "TblOrderHeader",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 5, 27, 11, 14, 17, 350, DateTimeKind.Local).AddTicks(5373));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 5, 27, 11, 14, 17, 350, DateTimeKind.Local).AddTicks(5373));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 5, 27, 11, 14, 17, 350, DateTimeKind.Local).AddTicks(5373));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 5, 27, 11, 14, 17, 350, DateTimeKind.Local).AddTicks(5373));
        }
    }
}
