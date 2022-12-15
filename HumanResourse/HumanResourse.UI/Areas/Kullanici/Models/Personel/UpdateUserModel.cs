using System.ComponentModel.DataAnnotations;

namespace HumanResourse.UI.Areas.Kullanici.Models.Personel
{
    public class UpdateUserModel
    {
        public IFormFile ImageURL { get; set; }

        [DataType(DataType.PhoneNumber)]       
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
