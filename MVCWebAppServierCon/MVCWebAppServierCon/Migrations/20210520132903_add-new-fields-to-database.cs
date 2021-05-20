using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addnewfieldstodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "TblOrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Show_ToDate",
                table: "TblGeneralPreference",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ApprovalViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovalHeaderCode = table.Column<int>(nullable: false),
                    ApprovalIsApproved = table.Column<int>(nullable: false),
                    ApprovalStatus = table.Column<string>(nullable: true),
                    ApprovalNote = table.Column<string>(nullable: true),
                    ApprovalUserName = table.Column<string>(nullable: true),
                    ApprovalCreationDate = table.Column<DateTime>(nullable: false),
                    ApprovalDeviceIp = table.Column<int>(nullable: false),
                    ToUser = table.Column<string>(nullable: true),
                    ToUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalViewModel", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 5, 20, 16, 29, 2, 32, DateTimeKind.Local).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 5, 20, 16, 29, 2, 36, DateTimeKind.Local).AddTicks(5671));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 5, 20, 16, 29, 2, 36, DateTimeKind.Local).AddTicks(5731));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 5, 20, 16, 29, 2, 36, DateTimeKind.Local).AddTicks(5742));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalViewModel");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "TblOrderHeader");

            migrationBuilder.DropColumn(
                name: "Show_ToDate",
                table: "TblGeneralPreference");

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreationDate",
                value: new DateTime(2021, 4, 28, 11, 59, 33, 431, DateTimeKind.Local).AddTicks(9173));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreationDate",
                value: new DateTime(2021, 4, 28, 11, 59, 33, 439, DateTimeKind.Local).AddTicks(3500));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreationDate",
                value: new DateTime(2021, 4, 28, 11, 59, 33, 439, DateTimeKind.Local).AddTicks(3605));

            migrationBuilder.UpdateData(
                table: "SalesQouteHeader",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreationDate",
                value: new DateTime(2021, 4, 28, 11, 59, 33, 439, DateTimeKind.Local).AddTicks(3616));
        }
    }
}
