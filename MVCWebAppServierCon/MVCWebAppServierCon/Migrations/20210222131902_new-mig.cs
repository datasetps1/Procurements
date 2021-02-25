using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class newmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesCriterias_SalesQouteHeader_SalesQouteHeaderId",
                table: "SalesCriterias");

            migrationBuilder.AlterColumn<int>(
                name: "SalesQouteHeaderId",
                table: "SalesCriterias",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 2, 22, 15, 19, 0, 879, DateTimeKind.Local).AddTicks(2699));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 2, 22, 15, 19, 0, 883, DateTimeKind.Local).AddTicks(9623));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 2, 22, 15, 19, 0, 883, DateTimeKind.Local).AddTicks(9687));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 2, 22, 15, 19, 0, 883, DateTimeKind.Local).AddTicks(9696));

            migrationBuilder.AddForeignKey(
                name: "FK_SalesCriterias_SalesQouteHeader_SalesQouteHeaderId",
                table: "SalesCriterias",
                column: "SalesQouteHeaderId",
                principalTable: "SalesQouteHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesCriterias_SalesQouteHeader_SalesQouteHeaderId",
                table: "SalesCriterias");

            migrationBuilder.AlterColumn<int>(
                name: "SalesQouteHeaderId",
                table: "SalesCriterias",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 2, 21, 11, 6, 10, 998, DateTimeKind.Local).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 2, 21, 11, 6, 10, 998, DateTimeKind.Local).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 2, 21, 11, 6, 10, 998, DateTimeKind.Local).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 2, 21, 11, 6, 10, 998, DateTimeKind.Local).AddTicks(8280));

            migrationBuilder.AddForeignKey(
                name: "FK_SalesCriterias_SalesQouteHeader_SalesQouteHeaderId",
                table: "SalesCriterias",
                column: "SalesQouteHeaderId",
                principalTable: "SalesQouteHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
