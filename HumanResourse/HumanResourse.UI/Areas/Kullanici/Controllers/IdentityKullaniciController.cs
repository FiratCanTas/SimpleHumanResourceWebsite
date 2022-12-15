using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using HumanResourse.UI.Areas.Kullanici.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourse.UI.Areas.Kullanici.Controllers
{
    [Area("Kullanici")]
    public class IdentityKullaniciController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public IdentityKullaniciController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult KullanicilarListesi()
        {
            var values = _userManager.Users.ToList();
            ViewBag.user = values;
            return View();
        }

        [HttpGet]
        public IActionResult KullaniciEkle()
        {

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> KullaniciEkle(UserCreateModel model)
        {
            AppUser appUser = new AppUser();

            //if (model.SecondSurname== null || model.SecondName == null || model.UserName==null)
            //{
            //    model.SecondSurname = "fsdf";
            //    model.SecondName = "sfsf";
            //    model.UserName = "sdfdsf";
              


            //}

            if (model.ImageURL != null)
            {
                if (model.ImageURL.ContentType == "image/png" || model.ImageURL.ContentType == "image/jpeg")
                {
                    var extension = Path.GetExtension(model.ImageURL.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImages/", imageName);
                    var stream = new FileStream(location, FileMode.Create);
                    model.ImageURL.CopyTo(stream);
                    appUser.ImageURL = "/userimages/" + imageName;
                }
                else
                {
                    TempData["mesaj"] = "Dosya uzantısı sadece jpeg veya png olabilir";
                }


            }
            else
            {
                if (model.Gender == Gender.Erkek)
           
                    appUser.ImageURL = "/userimages/man_s.png";

                else
                 
                    appUser.ImageURL = "/userimages/woman.png";

            }

            if (ModelState.IsValid)
            {
              

                appUser.Name = model.Name;
                appUser.SecondName = model.SecondName;
                appUser.Surname = model.Surname;
                appUser.IdentityNumber = model.IdentityNumber;
                appUser.Gender = model.Gender;
                appUser.Email = model.Name.ToLower() + "." + model.Surname.ToLower() + "@bilgeadam.com.tr";
                appUser.UserName = appUser.Email;
                appUser.Departman = model.Departman;
                appUser.Job = model.Job;
                appUser.BirthDate = model.BirthDate;
                appUser.BirthPlace = model.BirthPlace;
                appUser.Address = model.Address;
                appUser.CompanyID = 1;




                if (model.Password == model.ConfirmPassword)
                {
                    var result = await _userManager.CreateAsync(appUser, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(appUser, "Personel");

                        return RedirectToAction("KullanicilarListesi");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Şifreler Uyuşmuyor");
                }
            }

            

            return View();
        }

        public async Task<IActionResult> KullaniciSil(int id) 
        {
            var personel = _userManager.Users.FirstOrDefault(x => x.Id == id);
            var result = await _userManager.DeleteAsync(personel);

            if (result.Succeeded)
            {
                return RedirectToAction("KullanicilarListesi");
            }
            return View();
        }

    }
}
