using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addsomesampledata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SalesQouteHeader",
                columns: new[] { "Id", "CreationDate", "ExpierDate", "OfferDate", "OfferName", "userCode" },
                values: new object[,]
                {
                    { -4, new DateTime(2021, 2, 21, 11, 6, 10, 998, DateTimeKind.Local).AddTicks(8280), new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شراء قرطاسية", null },
                    { -3, new DateTime(2021, 2, 21, 11, 6, 10, 998, DateTimeKind.Local).AddTicks(8280), new DateTime(2021, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شراء شي", null },
                    { -2, new DateTime(2021, 2, 21, 11, 6, 10, 998, DateTimeKind.Local).AddTicks(8280), new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "something", null },
                    { -1, new DateTime(2021, 2, 21, 11, 6, 10, 998, DateTimeKind.Local).AddTicks(8280), new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "another thing", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
