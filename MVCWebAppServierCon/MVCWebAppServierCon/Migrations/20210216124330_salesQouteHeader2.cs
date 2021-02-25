using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class salesQouteHeader2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesQouteHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OfferName = table.Column<string>(nullable: false),
                    OfferDate = table.Column<DateTime>(nullable: false),
                    ExpierDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesQouteHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CriteriaId = table.Column<int>(nullable: false),
                    Percentage = table.Column<int>(nullable: false),
                    SalesQouteHeaderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesCriterias_SalesQouteHeader_SalesQouteHeaderId",
                        column: x => x.SalesQouteHeaderId,
                        principalTable: "SalesQouteHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierName = table.Column<string>(nullable: false),
                    AttachmentPath = table.Column<string>(nullable: true),
                    SalesQouteHeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesSuppliers_SalesQouteHeader_SalesQouteHeaderId",
                        column: x => x.SalesQouteHeaderId,
                        principalTable: "SalesQouteHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesCriterias_SalesQouteHeaderId",
                table: "SalesCriterias",
                column: "SalesQouteHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesSuppliers_SalesQouteHeaderId",
                table: "SalesSuppliers",
                column: "SalesQouteHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesCriterias");

            migrationBuilder.DropTable(
                name: "SalesSuppliers");

            migrationBuilder.DropTable(
                name: "SalesQouteHeader");
        }
    }
}
