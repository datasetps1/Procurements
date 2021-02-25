using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class addUploadClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblUploads",
                columns: table => new
                {
                    uploadCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    uploadHeaderCode = table.Column<int>(nullable: false),
                    uploadPath = table.Column<string>(nullable: false),
                    uploadNote = table.Column<string>(nullable: true),
                    uploadUserId = table.Column<int>(nullable: false),
                    uploadCreationDate = table.Column<DateTime>(nullable: false),
                    uploadDeviceIp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUploads", x => x.uploadCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblUploads");
        }
    }
}
