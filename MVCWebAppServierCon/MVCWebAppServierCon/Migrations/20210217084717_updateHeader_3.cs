using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class updateHeader_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userCode",
                table: "SalesQouteHeader",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesQouteHeader_userCode",
                table: "SalesQouteHeader",
                column: "userCode");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesQouteHeader_TblUser_userCode",
                table: "SalesQouteHeader",
                column: "userCode",
                principalTable: "TblUser",
                principalColumn: "userCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesQouteHeader_TblUser_userCode",
                table: "SalesQouteHeader");

            migrationBuilder.DropIndex(
                name: "IX_SalesQouteHeader_userCode",
                table: "SalesQouteHeader");

            migrationBuilder.AlterColumn<string>(
                name: "userCode",
                table: "SalesQouteHeader",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
