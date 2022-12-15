using HumanResourse.Entitiy.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.Entitiy.Concrete
{
    public class AppUser: IdentityUser<int>
    {
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public string IdentityNumber { get; set; }
        public Gender Gender { get; set; }
        public string? ImageURL { get; set; }
        public Departman Departman { get; set; }
        public string Job { get; set; }
        public decimal? Salary { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public DateTime HiredDate { get; set; } = DateTime.Now;
        public DateTime? FiredDate { get; set; }
        public string? Address { get; set; }
        public Status statu { get; set; } = Status.Active;

        public Company Company { get; set; }
        public virtual int? CompanyID { get; set; }
        public List<Expenses> Expensess { get; set; }
        public List<Permit> Permits { get; set; }
        public List<AdvancePayment> AdvancePayments { get; set; }
    }
}
