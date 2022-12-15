using HumanResourse.Entitiy.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.DataAccess.Context
{
    public class HumanResoursesContext:IdentityDbContext<AppUser,AppRole, int>
    {
      
        public HumanResoursesContext(DbContextOptions<HumanResoursesContext> options) : base(options)
        {

        }

        public DbSet<AdvancePayment>AdvancePayments { get; set; }
        public DbSet<Expenses>Expenses { get; set; }
        public DbSet<Permit>Permits { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
