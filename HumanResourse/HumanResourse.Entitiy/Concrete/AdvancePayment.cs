using HumanResourse.Entitiy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.Entitiy.Concrete
{
    public class AdvancePayment
    {
        public int AdvancePaymentID { get; set; }
        public AdvancePaymentType advancePaymentType { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? ReponseDate { get; set; }
        public Position Statu { get; set; } = Position.Beklemede;
        public double ExpensesAmount { get; set; }
        public string CurrencyUnit { get; set; }

        public string Description { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUsers { get; set; }
    }
}
