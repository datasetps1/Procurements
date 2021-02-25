using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addtableleTest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TblTest",
                table: "TblTest");

            migrationBuilder.RenameTable(
                name: "TblTest",
                newName: "TestClass");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestClass",
                table: "TestClass",
                column: "testCode");

            migrationBuilder.CreateTable(
                name: "Test2Class",
                columns: table => new
                {
                    testCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    testName = table.Column<string>(nullable: false),
                    name2 = table.Column<string>(nullable: false),
                    name3 = table.Column<string>(nullable: false),
                    creationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test2Class", x => x.testCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test2Class");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestClass",
                table: "TestClass");

            migrationBuilder.RenameTable(
                name: "TestClass",
                newName: "TblTest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblTest",
                table: "TblTest",
                column: "testCode");
        }
    }
}
