using HumanResourse.Entitiy.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HumanResourse.Entitiy.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HumanResourse.Business.ValidationRules;
using FluentValidation.Results;
using HumanResourse.UI.Models;

namespace HumanResourse.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
 
        public async Task<IActionResult> Login(UserLoginMddel appUser)
        {
            //SignInValidator validationRules = new SignInValidator();
            //ValidationResult validationResult = validationRules.Validate(appUser);


            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(appUser.UserName, appUser.Password, false, true);
      

                if (result.Succeeded)
                {

                    return RedirectToAction("Anasayfa", "Personel", new { area = "Kullanici" });

                }

                ModelState.AddModelError("", "Kullanıcı adı veya şifreniz hatalı");
            }



            //else
            //{
            //    foreach (var item in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }

            //}

            return View();
        }


        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Login");
        }



    }
}
