using HumanResourse.Entitiy.Enums;

namespace HumanResourse.UI.Areas.Islemler.Models
{
    public class PermitCreateModel
    {
        public PermitTypee permitType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
