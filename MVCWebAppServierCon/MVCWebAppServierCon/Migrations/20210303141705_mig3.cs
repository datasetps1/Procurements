using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompleteActivityOffered",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Offered_to_us = table.Column<string>(nullable: true),
                    CompleteActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompleteActivityOffered", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompleteActivityOffered_CompleteActivity_CompleteActivityId",
                        column: x => x.CompleteActivityId,
                        principalTable: "CompleteActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 17, 4, 804, DateTimeKind.Local).AddTicks(2127));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 17, 4, 805, DateTimeKind.Local).AddTicks(2127));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 17, 4, 805, DateTimeKind.Local).AddTicks(2127));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 17, 4, 805, DateTimeKind.Local).AddTicks(2127));

            migrationBuilder.CreateIndex(
                name: "IX_CompleteActivityOffered_CompleteActivityId",
                table: "CompleteActivityOffered",
                column: "CompleteActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompleteActivityOffered");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 14, 39, 994, DateTimeKind.Local).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 14, 39, 994, DateTimeKind.Local).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 14, 39, 994, DateTimeKind.Local).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 3, 3, 16, 14, 39, 994, DateTimeKind.Local).AddTicks(8687));
        }
    }
}
