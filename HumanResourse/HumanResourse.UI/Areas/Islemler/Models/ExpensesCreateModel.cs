using HumanResourse.Entitiy.Enums;

namespace HumanResourse.UI.Areas.Islemler.Models
{
    public class ExpensesCreateModel
    {
        public AdvancePaymentType ExpensesType { get; set; }
        public double ExpensesAmount { get; set; }
        public string CurrencyUnit { get; set; }
        public IFormFile Documentation { get; set; }
    }
}
