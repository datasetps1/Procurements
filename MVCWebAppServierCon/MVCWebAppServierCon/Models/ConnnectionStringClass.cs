using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Models;

namespace MVCWebAppServierCon.Models
{
    public class ConnnectionStringClass:IdentityDbContext
    {
        public ConnnectionStringClass(DbContextOptions<ConnnectionStringClass> options):base(options)
        {

        }

        // public DbSet<GrpClass> TBLGroups { get; set; }
        public DbSet<StructureClass> TblStructure { get; set; }

        public DbSet<UserClass> TblUser { get; set; }

        public DbSet<OrderTypeClass> TblOrderType { get; set; }

        public DbSet<AmountSittingClass> TblAmountSitting { get; set; }

        public DbSet<DepartmentClass> TblDepartment { get; set; }

        
    }
}
