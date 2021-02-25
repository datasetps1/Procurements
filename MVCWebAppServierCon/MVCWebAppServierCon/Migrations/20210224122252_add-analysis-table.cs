using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addanalysistable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesCriterias_SalesQouteHeader_SalesQouteHeaderId",
                table: "SalesCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesSuppliers_SalesQouteHeader_SalesQouteHeaderId",
                table: "SalesSuppliers");

            migrationBuilder.RenameColumn(
                name: "SalesQouteHeaderId",
                table: "SalesSuppliers",
                newName: "salesQouteHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesSuppliers_SalesQouteHeaderId",
                table: "SalesSuppliers",
                newName: "IX_SalesSuppliers_salesQouteHeaderId");

            migrationBuilder.RenameColumn(
                name: "SalesQouteHeaderId",
                table: "SalesCriterias",
                newName: "salesQouteHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesCriterias_SalesQouteHeaderId",
                table: "SalesCriterias",
                newName: "IX_SalesCriterias_salesQouteHeaderId");

            migrationBuilder.CreateTable(
                name: "analysis",
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
                    table.PrimaryKey("PK_analysis", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 14, 22, 51, 727, DateTimeKind.Local).AddTicks(3987));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 14, 22, 51, 732, DateTimeKind.Local).AddTicks(6607));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 14, 22, 51, 732, DateTimeKind.Local).AddTicks(6649));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 2, 24, 14, 22, 51, 732, DateTimeKind.Local).AddTicks(6655));

            migrationBuilder.AddForeignKey(
                name: "FK_SalesCriterias_SalesQouteHeader_salesQouteHeaderId",
                table: "SalesCriterias",
                column: "salesQouteHeaderId",
                principalTable: "SalesQouteHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesSuppliers_SalesQouteHeader_salesQouteHeaderId",
                table: "SalesSuppliers",
                column: "salesQouteHeaderId",
                principalTable: "SalesQouteHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesCriterias_SalesQouteHeader_salesQouteHeaderId",
                table: "SalesCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesSuppliers_SalesQouteHeader_salesQouteHeaderId",
                table: "SalesSuppliers");

            migrationBuilder.DropTable(
                name: "analysis");

            migrationBuilder.RenameColumn(
                name: "salesQouteHeaderId",
                table: "SalesSuppliers",
                newName: "SalesQouteHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesSuppliers_salesQouteHeaderId",
                table: "SalesSuppliers",
                newName: "IX_SalesSuppliers_SalesQouteHeaderId");

            migrationBuilder.RenameColumn(
                name: "salesQouteHeaderId",
                table: "SalesCriterias",
                newName: "SalesQouteHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesCriterias_salesQouteHeaderId",
                table: "SalesCriterias",
                newName: "IX_SalesCriterias_SalesQouteHeaderId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SalesSuppliers_SalesQouteHeader_SalesQouteHeaderId",
                table: "SalesSuppliers",
                column: "SalesQouteHeaderId",
                principalTable: "SalesQouteHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
