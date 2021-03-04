using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 6, 53, 908, DateTimeKind.Local).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 6, 53, 908, DateTimeKind.Local).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 6, 53, 908, DateTimeKind.Local).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 6, 53, 908, DateTimeKind.Local).AddTicks(3283));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompleteActivity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityDate = table.Column<DateTime>(nullable: false),
                    ActivityVenue = table.Column<string>(nullable: true),
                    BookNumber = table.Column<string>(nullable: true),
                    CoordinatorName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DoneTasks = table.Column<string>(nullable: true),
                    OrderCode = table.Column<int>(nullable: false),
                    ProjectName = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompleteActivity", x => x.Id);
                });



            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 15, 20, 40, 39, DateTimeKind.Local).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 15, 20, 40, 39, DateTimeKind.Local).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 15, 20, 40, 39, DateTimeKind.Local).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 15, 20, 40, 39, DateTimeKind.Local).AddTicks(9688));

        }
    }
}
