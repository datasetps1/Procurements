using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebAppServierCon.Migrations
{
    public partial class createDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblAmountSitting",
                columns: table => new
                {
                    amountCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    amountFrom = table.Column<int>(nullable: false),
                    amountTo = table.Column<int>(nullable: false),
                    amountStructure = table.Column<int>(nullable: false),
                    amountUserId = table.Column<int>(nullable: false),
                    amountCreationDate = table.Column<DateTime>(nullable: false),
                    amountNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAmountSitting", x => x.amountCode);
                });

            migrationBuilder.CreateTable(
                name: "TblApproval",
                columns: table => new
                {
                    ApprovalCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovalHeaderCode = table.Column<int>(nullable: false),
                    ApprovalEmployeeCode = table.Column<int>(nullable: false),
                    ApprovalIsApproved = table.Column<int>(nullable: false),
                    ApprovalNote = table.Column<string>(nullable: true),
                    ApprovalUserId = table.Column<int>(nullable: false),
                    ApprovalCreationDate = table.Column<DateTime>(nullable: false),
                    ApprovalDeviceIp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblApproval", x => x.ApprovalCode);
                });

            migrationBuilder.CreateTable(
                name: "TblDepartment",
                columns: table => new
                {
                    departmentCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    departmentName = table.Column<string>(nullable: false),
                    departmentManagerCode = table.Column<int>(nullable: false),
                    departmentGeneralManagerCode = table.Column<int>(nullable: false),
                    departmentHeadCode = table.Column<int>(nullable: false),
                    departmentFinancialCode = table.Column<int>(nullable: false),
                    departmentProcurementSectionCode = table.Column<int>(nullable: false),
                    departmentUserId = table.Column<int>(nullable: false),
                    departmentDeviceIp = table.Column<int>(nullable: false),
                    departmentCreationDate = table.Column<DateTime>(nullable: false),
                    departmentNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDepartment", x => x.departmentCode);
                });

            migrationBuilder.CreateTable(
                name: "TblItem",
                columns: table => new
                {
                    itemCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    itemName = table.Column<string>(nullable: false),
                    itemName2 = table.Column<string>(nullable: false),
                    itemPrice = table.Column<float>(nullable: false),
                    itemUserId = table.Column<int>(nullable: false),
                    itemCreationDate = table.Column<DateTime>(nullable: false),
                    itemNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblItem", x => x.itemCode);
                });

            migrationBuilder.CreateTable(
                name: "TblOrderHeader",
                columns: table => new
                {
                    OrderHeaderCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderHeaderdepartmentCode = table.Column<int>(nullable: false),
                    OrderHeaderProjectCode = table.Column<int>(nullable: false),
                    OrderHeaderdate = table.Column<DateTime>(nullable: false),
                    OrderHeaderOrderTypeCode = table.Column<int>(nullable: false),
                    OrderHeaderBudgetLineCode = table.Column<int>(nullable: false),
                    OrderHeaderCurrencey = table.Column<string>(nullable: false),
                    OrderHeaderRate = table.Column<float>(nullable: false),
                    OrderHeaderRealTotal = table.Column<float>(nullable: false),
                    OrderHeaderNote = table.Column<string>(nullable: true),
                    OrderHeaderUserId = table.Column<int>(nullable: false),
                    OrderHeaderCreationDate = table.Column<DateTime>(nullable: false),
                    OrderHeaderDeviceIp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrderHeader", x => x.OrderHeaderCode);
                });

            migrationBuilder.CreateTable(
                name: "TblOrderType",
                columns: table => new
                {
                    orderTypeCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    orderTypeName = table.Column<string>(nullable: false),
                    orderTypePeriodInDays = table.Column<int>(nullable: false),
                    orderTypeUserId = table.Column<int>(nullable: false),
                    orderTypeCreationDate = table.Column<DateTime>(nullable: false),
                    orderTypeNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrderType", x => x.orderTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "TblStructure",
                columns: table => new
                {
                    structureCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    structureName = table.Column<string>(nullable: false),
                    structureName2 = table.Column<string>(nullable: false),
                    structureRank = table.Column<int>(nullable: false),
                    structureUserId = table.Column<int>(nullable: false),
                    structureCreationDate = table.Column<DateTime>(nullable: false),
                    structureNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblStructure", x => x.structureCode);
                });

            migrationBuilder.CreateTable(
                name: "TblTransaction",
                columns: table => new
                {
                    TransactionCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransactionOrderHeaderCode = table.Column<int>(nullable: false),
                    TransactionItemCode = table.Column<int>(nullable: false),
                    TransactionQty = table.Column<int>(nullable: false),
                    TransactionPrice = table.Column<float>(nullable: false),
                    TransactionNote = table.Column<string>(nullable: true),
                    TransactionUserId = table.Column<int>(nullable: false),
                    TransactionCreationDate = table.Column<DateTime>(nullable: false),
                    TransactionDeviceIp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTransaction", x => x.TransactionCode);
                });

            migrationBuilder.CreateTable(
                name: "TblUser",
                columns: table => new
                {
                    userCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userName = table.Column<string>(nullable: false),
                    userEmail = table.Column<string>(nullable: false),
                    userTypeCode = table.Column<int>(nullable: false),
                    departmentManagerCode = table.Column<int>(nullable: false),
                    managmentAccesstanceCode = table.Column<int>(nullable: false),
                    sectionManagerCode = table.Column<int>(nullable: false),
                    finacnceDepartmentCode = table.Column<int>(nullable: false),
                    userActive = table.Column<int>(nullable: false),
                    userUserId = table.Column<int>(nullable: false),
                    userCreationDate = table.Column<DateTime>(nullable: false),
                    userNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUser", x => x.userCode);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TblAmountSitting");

            migrationBuilder.DropTable(
                name: "TblApproval");

            migrationBuilder.DropTable(
                name: "TblDepartment");

            migrationBuilder.DropTable(
                name: "TblItem");

            migrationBuilder.DropTable(
                name: "TblOrderHeader");

            migrationBuilder.DropTable(
                name: "TblOrderType");

            migrationBuilder.DropTable(
                name: "TblStructure");

            migrationBuilder.DropTable(
                name: "TblTransaction");

            migrationBuilder.DropTable(
                name: "TblUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
