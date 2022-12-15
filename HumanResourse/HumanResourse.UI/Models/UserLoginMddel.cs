using System.ComponentModel.DataAnnotations;

namespace HumanResourse.UI.Models
{
    public class UserLoginMddel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }
    }
}
