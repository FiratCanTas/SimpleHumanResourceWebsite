using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HumanResourse.UI.Areas.Kullanici.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "İsim alanı boş geçilemez")]
        [MaxLength(20, ErrorMessage = "En fazla 20 karakter uzunluğunda olmalıdır.")]
        [MinLength(2, ErrorMessage = "En az 2 karakter uzunluğunda olmalıdır.")]
        public string Name { get; set; }

        public string? SecondName { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş geçilemez")]
        [MaxLength(20, ErrorMessage = "En fazla 20 karakter uzunluğunda olmalıdır.")]
        [MinLength(2, ErrorMessage = "En az 2 karakter uzunluğunda olmalıdır.")]
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }


        [Required(ErrorMessage = "Kimlik numarası boş geçilemez")]          
        public string IdentityNumber { get; set; }

        public Gender Gender { get; set; }

        public IFormFile? ImageURL { get; set; }

        public string? UserName { get; set; }

        [Required(ErrorMessage = "Telefon numarası boş geçilemez")]      
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Şifre boş geçilemez")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        public Departman Departman { get; set; }


        [Required(ErrorMessage = "Meslek alanı boş geçilemez")]
        public string Job { get; set; }

        [Required(ErrorMessage = "Doğum tarihi alanı boş geçilemez")]
        [Range(typeof(DateTime), "01/01/1957", "01/01/2004",ErrorMessage= "Yaş 18'den küçük 65'ten büyük olamaz.") ]        
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "Doğum yeri alanı boş geçilemez")]
        public string BirthPlace { get; set; }

        [Required(ErrorMessage = "Adress alanı boş geçilemez")]
        public string Address { get; set; }

  
    }
}
