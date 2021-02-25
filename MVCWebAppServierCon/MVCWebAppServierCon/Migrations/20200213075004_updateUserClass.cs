using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class updateUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "departmentManagerCode",
                table: "TblUser");

            migrationBuilder.DropColumn(
                name: "finacnceDepartmentCode",
                table: "TblUser");

            migrationBuilder.DropColumn(
                name: "managmentAccesstanceCode",
                table: "TblUser");

            migrationBuilder.RenameColumn(
                name: "sectionManagerCode",
                table: "TblUser",
                newName: "userDepartmentCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userDepartmentCode",
                table: "TblUser",
                newName: "sectionManagerCode");

            migrationBuilder.AddColumn<int>(
                name: "departmentManagerCode",
                table: "TblUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "finacnceDepartmentCode",
                table: "TblUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "managmentAccesstanceCode",
                table: "TblUser",
                nullable: false,
                defaultValue: 0);
        }
    }
}
