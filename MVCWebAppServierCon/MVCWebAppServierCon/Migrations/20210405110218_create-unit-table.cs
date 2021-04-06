using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class createunittable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 2, 17, 535, DateTimeKind.Local).AddTicks(2741));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 2, 17, 538, DateTimeKind.Local).AddTicks(2422));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 2, 17, 538, DateTimeKind.Local).AddTicks(2458));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 5, 14, 2, 17, 538, DateTimeKind.Local).AddTicks(2465));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 1, 10, 52, 50, 961, DateTimeKind.Local).AddTicks(2264));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 1, 10, 52, 50, 964, DateTimeKind.Local).AddTicks(8669));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 1, 10, 52, 50, 964, DateTimeKind.Local).AddTicks(8712));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 1, 10, 52, 50, 964, DateTimeKind.Local).AddTicks(8719));
        }
    }
}
