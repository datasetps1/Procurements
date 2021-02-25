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
    [Migration("20200225132135_addtableleTest2")]
    partial class addtableleTest2
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

                    b.Property<string>("amountStructure")
                        .IsRequired();

                    b.Property<int>("amountTo");

                    b.Property<int>("amountUserId");

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

                    b.Property<int>("ApprovalUserId");

                    b.HasKey("ApprovalCode");

                    b.ToTable("TblApproval");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.DepartmentClass", b =>
                {
                    b.Property<int>("departmentCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("departmentCreationDate");

                    b.Property<int>("departmentDeviceIp");

                    b.Property<int>("departmentFinancialCode");

                    b.Property<int>("departmentGeneralManagerCode");

                    b.Property<int>("departmentHeadCode");

                    b.Property<int>("departmentManagerCode");

                    b.Property<string>("departmentName")
                        .IsRequired();

                    b.Property<string>("departmentNote");

                    b.Property<int>("departmentProcurementSectionCode");

                    b.Property<int>("departmentUserId");

                    b.HasKey("departmentCode");

                    b.ToTable("TblDepartment");
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

            modelBuilder.Entity("MVCWebAppServierCon.Models.OrderHeaderClass", b =>
                {
                    b.Property<int>("OrderHeaderCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderHeaderBudgetLineCode");

                    b.Property<DateTime>("OrderHeaderCreationDate");

                    b.Property<string>("OrderHeaderCurrencey")
                        .IsRequired();

                    b.Property<int>("OrderHeaderDeviceIp");

                    b.Property<string>("OrderHeaderNote");

                    b.Property<int>("OrderHeaderOrderTypeCode");

                    b.Property<int>("OrderHeaderProjectCode");

                    b.Property<float>("OrderHeaderRate");

                    b.Property<float>("OrderHeaderRealTotal");

                    b.Property<int>("OrderHeaderUserId");

                    b.Property<DateTime>("OrderHeaderdate");

                    b.Property<int>("OrderHeaderdepartmentCode");

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

                    b.Property<int>("orderTypePeriodInDays");

                    b.Property<int>("orderTypeUserId");

                    b.HasKey("orderTypeCode");

                    b.ToTable("TblOrderType");
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

                    b.Property<int>("TransactionDeviceIp");

                    b.Property<int>("TransactionItemCode");

                    b.Property<string>("TransactionNote");

                    b.Property<int>("TransactionOrderHeaderCode");

                    b.Property<float>("TransactionPrice");

                    b.Property<int>("TransactionQty");

                    b.Property<int>("TransactionUserId");

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

                    b.Property<int>("uploadUserId");

                    b.HasKey("uploadCode");

                    b.ToTable("TblUploads");
                });

            modelBuilder.Entity("MVCWebAppServierCon.Models.UserClass", b =>
                {
                    b.Property<int>("userCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("userActive");

                    b.Property<DateTime>("userCreationDate");

                    b.Property<int>("userDepartmentCode");

                    b.Property<string>("userEmail")
                        .IsRequired();

                    b.Property<string>("userName")
                        .IsRequired();

                    b.Property<string>("userNote");

                    b.Property<int>("userTypeCode");

                    b.Property<int>("userUserId");

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
