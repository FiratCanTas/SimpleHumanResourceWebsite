using HumanResourse.Entitiy.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.Entitiy.Concrete
{
    public class Expenses
    {
        public int ExpensesID { get; set; }
        public AdvancePaymentType ExpensesType { get; set; }
        public double ExpensesAmount { get; set; }
        public string CurrencyUnit { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? ReponseDate { get; set; }
        public Position Statu { get; set; } = Position.Beklemede;

       /* [NotMapped]*/ //Code First yaklaşımında ilgili property Not Mapped  olarak işaretliyse veya get ya da set değerlerinden birisi verilmediyse Db tablosunda gösterilmez.
        public string? Documentation { get; set; }

        public int AppUserID { get; set; }
        public AppUser AppUsers { get; set; }
    }
}
