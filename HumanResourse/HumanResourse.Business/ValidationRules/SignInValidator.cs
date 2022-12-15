using FluentValidation;
using HumanResourse.Entitiy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HumanResourse.Business.ValidationRules
{
    public class SignInValidator:AbstractValidator<UserSigInModel>
    {
        //kullanıcı girişinde çalıştıramadım, dataantotion ile yaptım
        public SignInValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Email boş geçilemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez");
        }
    }
}

