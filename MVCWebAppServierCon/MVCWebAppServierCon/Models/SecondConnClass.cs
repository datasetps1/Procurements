using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Models;
using MVCWebAppServierCon.ViewModels;
using MVCWebAppServierCon.Controllers;

namespace MVCWebAppServierCon.Models
{
    public class SecondConnClass : IdentityDbContext
    {
        public SecondConnClass(DbContextOptions<SecondConnClass> options) : base(options)
        {
        }

        public DbSet<StructureClass> TblStructure { get; set; }
        public DbSet<UserClass> TblUser { get; set; }
        public DbSet<OrderTypeClass> TblOrderType { get; set; }
        public DbSet<AmountSittingClass> TblAmountSitting { get; set; }
        public DbSet<DepartmentClass> TblDepartment { get; set; }
        public DbSet<ItemClass> TblItem { get; set; }
        public DbSet<TransactionClass> TblTransaction { get; set; }
        public DbSet<ApprovalClass> TblApproval { get; set; }
        public DbSet<OrderHeaderClass> TblOrderHeader { get; set; }
        public DbSet<UploadClass> TblUploads { get; set; }
        public DbSet<TestClass> TestClass { get; set; }
        public DbSet<Test2Class> Test2Class { get; set; }

        public DbSet<DepartmentUserClass> ViwDepUsers { get; set; }
        public DbSet<Forms> Forms { get; set; }
        public DbSet<GeneralPreferenceClass> TblGeneralPreference { get; set; }
        public DbSet<Contracts> TblContracts { get; set; }
        public DbQuery<ViewBudget> ViewBudget { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<SalesQouteHeader> SalesQouteHeader { get; set; }
        public DbSet<SalesSuppliers> SalesSuppliers { get; set; }
        public DbSet<SalesCriterias> SalesCriterias { get; set; }
        public DbSet<OrderAnalysis> OrderAnalysis { get; set; }
        public DbSet<CompleteActivity> CompleteActivity { get; set; }
        public DbSet<CompleteActivityOffered> CompleteActivityOffered { get; set; }
        public DbSet<CompleteActivityFiles> CompleteActivityFiles { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SalesQouteHeader>().HasData(
                new SalesQouteHeader()
                {
                    Id = -4,
                    CreationDate = DateTime.Now,
                    ExpierDate = new DateTime(2022, 10, 3),
                    OfferDate = new DateTime(),
                    OfferName = "شراء قرطاسية",

                },
                new SalesQouteHeader()
                {
                    Id = -3,
                    CreationDate = DateTime.Now,
                    ExpierDate = new DateTime(2021, 1, 3),
                    OfferDate = new DateTime(),
                    OfferName = "شراء شي"

                },
                new SalesQouteHeader()
                {
                    Id = -2,
                    CreationDate = DateTime.Now,
                    ExpierDate = new DateTime(2022, 10, 3),
                    OfferDate = new DateTime(),
                    OfferName = "something"

                },
                new SalesQouteHeader()
                {
                    Id = -1,
                    CreationDate = DateTime.Now,
                    ExpierDate = new DateTime(2022, 10, 3),
                    OfferDate = new DateTime(),
                    OfferName = "another thing"

                }
            );

            builder.Entity<Unit>().HasData(
                new Unit()
                {
                    Id = -4,
                    Name = "كيلو"
                },
                new Unit()
                {
                    Id = -3,
                    Name = "اوقية"
                }, 
                new Unit()
                {
                    Id = -2,
                    Name = "علبة"
                },
                new Unit()
                {
                    Id = -1,
                    Name = "دزينة"
                }
            );


            base.OnModelCreating(builder);
        }

        public DbSet<MVCWebAppServierCon.Controllers.ApprovalViewModel> ApprovalViewModel { get; set; }

    }
}
