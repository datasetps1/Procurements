using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addtableleTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name2",
                table: "TblTest",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name3",
                table: "TblTest",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name2",
                table: "TblTest");

            migrationBuilder.DropColumn(
                name: "name3",
                table: "TblTest");
        }
    }
}
