﻿// <auto-generated />
using System;
using MVCWebAppServierCon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVCWebAppServierCon.Migrations
{
    [DbContext(typeof(SecondConnClass))]
    [Migration("20210228105414_update-salessuppliers")]
    partial class updatesalessuppliers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVCWebAppServierCon.Models.AmountSittingClass", b =>
                {
                    b.Property<int>("amountCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("amountCreationDate");

                    b.Property<int>("amountFrom");

                    b.Property<string>("amountNote");

                    b.Property<int>("amountStructure");

                    b.Property<int>("amountTo");

                    b.Property<string>("amountUserId")
                        .IsRequired();

                    b.HasKey("amountCode");

                    b.ToTable("TblAmountSitting");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.ApprovalClass", b =>
                {
                    b.Property<int>("ApprovalCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApprovalCreationDate");

                    b.Property<int>("ApprovalDeviceIp");

                    b.Property<int>("ApprovalHeaderCode");

                    b.Property<int>("ApprovalIsApproved");

                    b.Property<string>("ApprovalNote");

                    b.Property<string>("ApprovalUserId")
                        .IsRequired();

                    b.Property<string>("ToUser")
                        .IsRequired();

                    b.HasKey("ApprovalCode");

                    b.ToTable("TblApproval");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.Contracts", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("SupplierCode");

                    b.Property<DateTime>("ToDate");

                    b.HasKey("Code");

                    b.ToTable("TblContracts");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.Criteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Criteria_Rank");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Criteria");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.DepartmentClass", b =>
                {
                    b.Property<int>("departmentCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("departmentCreationDate");

                    b.Property<int>("departmentDeviceIp");

                    b.Property<string>("departmentFinancialCode")
                        .HasMaxLength(450);

                    b.Property<string>("departmentGeneralManagerCode")
                        .HasMaxLength(450);

                    b.Property<string>("departmentHeadCode")
                        .HasMaxLength(450);

                    b.Property<string>("departmentManagerCode")
                        .HasMaxLength(450);

                    b.Property<string>("departmentName")
                        .IsRequired();

                    b.Property<string>("departmentNote");

                    b.Property<string>("departmentProcurementSectionCode")
                        .HasMaxLength(450);

                    b.Property<string>("departmentUserId");

                    b.HasKey("departmentCode");

                    b.ToTable("TblDepartment");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.DepartmentUserClass", b =>
                {
                    b.Property<int>("departmentCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserFinancialName")
                        .HasMaxLength(450);

                    b.Property<string>("UserGeneralManagerName")
                        .HasMaxLength(450);

                    b.Property<string>("UserHeadName")
                        .HasMaxLength(450);

                    b.Property<string>("UserManagerName")
                        .HasMaxLength(450);

                    b.Property<string>("UserProcurementSectionName")
                        .HasMaxLength(450);

                    b.Property<string>("departmentName")
                        .IsRequired();

                    b.Property<string>("departmentNote");

                    b.HasKey("departmentCode");

                    b.ToTable("ViwDepUsers");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.Forms", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Code");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.GeneralPreferenceClass", b =>
                {
                    b.Property<int>("code")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("DeductionAmount");

                    b.Property<float>("QouteAmount");

                    b.HasKey("code");

                    b.ToTable("TblGeneralPreference");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.ItemClass", b =>
                {
                    b.Property<int>("itemCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("itemCreationDate");

                    b.Property<string>("itemName")
                        .IsRequired();

                    b.Property<string>("itemName2")
                        .IsRequired();

                    b.Property<string>("itemNote");

                    b.Property<float>("itemPrice");

                    b.Property<int>("itemUserId");

                    b.HasKey("itemCode");

                    b.ToTable("TblItem");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.OrderAnalysis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Percentage");

                    b.Property<int>("SalesCriteriasId");

                    b.Property<int>("SalesQouteHeaderId");

                    b.Property<int>("SalesSuppliersId");

                    b.Property<double>("Statement");

                    b.HasKey("Id");

                    b.ToTable("OrderAnalysis");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.OrderHeaderClass", b =>
                {
                    b.Property<int>("OrderHeaderCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("ActualTotalAmount");

                    b.Property<DateTime?>("ExpectedDate");

                    b.Property<bool>("Freaze");

                    b.Property<string>("FreazeNote");

                    b.Property<string>("OrderHeaderBudgetLineCode")
                        .IsRequired();

                    b.Property<DateTime>("OrderHeaderCreationDate");

                    b.Property<string>("OrderHeaderCurrencey")
                        .IsRequired();

                    b.Property<int>("OrderHeaderDeviceIp");

                    b.Property<string>("OrderHeaderNote");

                    b.Property<int>("OrderHeaderOrderTypeCode");

                    b.Property<string>("OrderHeaderProjectCode")
                        .IsRequired();

                    b.Property<float>("OrderHeaderRate");

                    b.Property<float>("OrderHeaderRealTotal");

                    b.Property<string>("OrderHeaderUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<DateTime>("OrderHeaderdate");

                    b.Property<int>("OrderHeaderdepartmentCode");

                    b.Property<string>("SupplierCode");

                    b.Property<string>("SupplierName");

                    b.HasKey("OrderHeaderCode");

                    b.ToTable("TblOrderHeader");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.OrderTypeClass", b =>
                {
                    b.Property<int>("orderTypeCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("orderTypeCreationDate");

                    b.Property<string>("orderTypeName")
                        .IsRequired();

                    b.Property<string>("orderTypeNote");

                    b.Property<int>("orderTypeNumDays");

                    b.Property<int?>("orderTypeNumDaysAfter");

                    b.Property<int>("orderTypePeriodInDays");

                    b.Property<bool>("orderTypeShowBudgetLine");

                    b.Property<bool>("orderTypeShowFunder");

                    b.Property<bool>("orderTypeShowTransDate");

                    b.Property<int>("orderTypeUserId");

                    b.HasKey("orderTypeCode");

                    b.ToTable("TblOrderType");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.SalesCriterias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CriteriaId");

                    b.Property<int>("Percentage");

                    b.Property<int>("salesQouteHeaderId");

                    b.HasKey("Id");

                    b.HasIndex("salesQouteHeaderId");

                    b.ToTable("SalesCriterias");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.SalesQouteHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("ExpierDate");

                    b.Property<DateTime>("OfferDate");

                    b.Property<string>("OfferName")
                        .IsRequired();

                    b.Property<string>("userCode");

                    b.HasKey("Id");

                    b.HasIndex("userCode");

                    b.ToTable("SalesQouteHeader");

                    b.HasData(
                        new
                        {
                            Id = -4,
                            CreationDate = new DateTime(2021, 2, 28, 12, 54, 13, 67, DateTimeKind.Local).AddTicks(6784),
                            ExpierDate = new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferName = "شراء قرطاسية"
                        },
                        new
                        {
                            Id = -3,
                            CreationDate = new DateTime(2021, 2, 28, 12, 54, 13, 71, DateTimeKind.Local).AddTicks(45),
                            ExpierDate = new DateTime(2021, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferName = "شراء شي"
                        },
                        new
                        {
                            Id = -2,
                            CreationDate = new DateTime(2021, 2, 28, 12, 54, 13, 71, DateTimeKind.Local).AddTicks(84),
                            ExpierDate = new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferName = "something"
                        },
                        new
                        {
                            Id = -1,
                            CreationDate = new DateTime(2021, 2, 28, 12, 54, 13, 71, DateTimeKind.Local).AddTicks(90),
                            ExpierDate = new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferName = "another thing"
                        });
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.SalesSuppliers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AttachmentPath");

                    b.Property<string>("SupplierName")
                        .IsRequired();

                    b.Property<int>("salesQouteHeaderId");

                    b.HasKey("Id");

                    b.HasIndex("salesQouteHeaderId");

                    b.ToTable("SalesSuppliers");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.StructureClass", b =>
                {
                    b.Property<int>("structureCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("structureCreationDate");

                    b.Property<string>("structureName")
                        .IsRequired();

                    b.Property<string>("structureName2")
                        .IsRequired();

                    b.Property<string>("structureNote");

                    b.Property<int>("structureRank");

                    b.Property<int>("structureUserId");

                    b.HasKey("structureCode");

                    b.ToTable("TblStructure");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.Test2Class", b =>
                {
                    b.Property<int>("testCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("creationDate");

                    b.Property<string>("name2")
                        .IsRequired();

                    b.Property<string>("name3")
                        .IsRequired();

                    b.Property<string>("testName")
                        .IsRequired();

                    b.HasKey("testCode");

                    b.ToTable("Test2Class");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.TestClass", b =>
                {
                    b.Property<int>("testCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("creationDate");

                    b.Property<string>("name2")
                        .IsRequired();

                    b.Property<string>("name3")
                        .IsRequired();

                    b.Property<string>("testName")
                        .IsRequired();

                    b.HasKey("testCode");

                    b.ToTable("TestClass");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.TransactionClass", b =>
                {
                    b.Property<int>("TransactionCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("TransactionCreationDate");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<int>("TransactionDeviceIp");

                    b.Property<int>("TransactionItemCode");

                    b.Property<string>("TransactionNote");

                    b.Property<int>("TransactionOrderHeaderCode");

                    b.Property<float>("TransactionPrice");

                    b.Property<double>("TransactionQty");

                    b.Property<string>("TransactionUserId")
                        .IsRequired();

                    b.HasKey("TransactionCode");

                    b.ToTable("TblTransaction");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.UploadClass", b =>
                {
                    b.Property<int>("uploadCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("uploadCreationDate");

                    b.Property<int>("uploadDeviceIp");

                    b.Property<int>("uploadHeaderCode");

                    b.Property<string>("uploadNote");

                    b.Property<string>("uploadPath")
                        .IsRequired();

                    b.Property<string>("uploadUserId")
                        .IsRequired();

                    b.HasKey("uploadCode");

                    b.ToTable("TblUploads");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.UserClass", b =>
                {
                    b.Property<string>("userCode")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("userActive");

                    b.Property<DateTime>("userCreationDate");

                    b.Property<int>("userDepartmentCode");

                    b.Property<string>("userEmail")
                        .IsRequired();

                    b.Property<string>("userName")
                        .IsRequired();

                    b.Property<string>("userNote");

                    b.Property<int>("userTypeCode");

                    b.Property<string>("userUserId")
                        .IsRequired();

                    b.HasKey("userCode");

                    b.ToTable("TblUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.SalesCriterias", b =>
                {
                    b.HasOne("MVCWebAppServierCon.Models.SalesQouteHeader", "salesQouteHeader")
                        .WithMany("salesCriterias")
                        .HasForeignKey("salesQouteHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.SalesQouteHeader", b =>
                {
                    b.HasOne("MVCWebAppServierCon.Models.UserClass", "User")
                        .WithMany()
                        .HasForeignKey("userCode");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.SalesSuppliers", b =>
                {
                    b.HasOne("MVCWebAppServierCon.Models.SalesQouteHeader")
                        .WithMany("salesSuppliers")
                        .HasForeignKey("salesQouteHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
