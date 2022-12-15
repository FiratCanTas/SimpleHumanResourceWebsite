using HumanResourse.Entitiy.Enums;

namespace HumanResourse.UI.Areas.Islemler.Models
{
    public class AdvanceCreateModel
    {
        public double ExpensesAmount { get; set; }
        public string CurrencyUnit { get; set; }

        public string Description { get; set; }

        public AdvancePaymentType advancePaymentType { get; set; }
    }
}
