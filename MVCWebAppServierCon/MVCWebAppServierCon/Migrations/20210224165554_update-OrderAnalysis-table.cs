using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class updateOrderAnalysistable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "OrderAnalysis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesQouteHeaderId = table.Column<int>(nullable: false),
                    SalesCriteriasId = table.Column<int>(nullable: false),
                    SalesSuppliersId = table.Column<int>(nullable: false),
                    Statement = table.Column<double>(nullable: false),
                    Percentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAnalysis", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 18, 55, 53, 680, DateTimeKind.Local).AddTicks(2893));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 18, 55, 53, 685, DateTimeKind.Local).AddTicks(6050));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 18, 55, 53, 685, DateTimeKind.Local).AddTicks(6111));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 18, 55, 53, 685, DateTimeKind.Local).AddTicks(6122));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAnalysis");

            migrationBuilder.CreateTable(
                name: "analysis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Percentage = table.Column<double>(nullable: false),
                    SalesCriteriasId = table.Column<int>(nullable: false),
                    SalesQouteHeaderId = table.Column<int>(nullable: false),
                    SalesSuppliersId = table.Column<int>(nullable: false),
                    Statement = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analysis", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 18, 38, 24, 661, DateTimeKind.Local).AddTicks(8770));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 18, 38, 24, 666, DateTimeKind.Local).AddTicks(7572));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 18, 38, 24, 666, DateTimeKind.Local).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 18, 38, 24, 666, DateTimeKind.Local).AddTicks(7639));
        }
    }
}
