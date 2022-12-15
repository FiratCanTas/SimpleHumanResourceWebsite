using HumanResourse.DataAccess.Context;
using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using HumanResourse.UI.Areas.Islemler.Models;
using HumanResourse.UI.Areas.Kullanici.Models.Personel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace HumanResourse.UI.Areas.Kullanici.Controllers
{
    [Area("Kullanici")]
    public class PersonelController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly HumanResoursesContext _humanResoursesContext;
        private readonly SignInManager<AppUser> _signInManager;

        public PersonelController(UserManager<AppUser> userManager, HumanResoursesContext humanResoursesContext, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _humanResoursesContext = humanResoursesContext;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Anasayfa( )
        {
            
            var personel = await _userManager.FindByNameAsync(User.Identity.Name);
            var rol = await _userManager.GetRolesAsync(personel);

            ViewBag.Permits= _humanResoursesContext.Permits.Where(x => x.AppUserID==personel.Id).ToList();  
            ViewBag.AdvancePayments= _humanResoursesContext.AdvancePayments.Where(x => x.AppUserID == personel.Id).ToList();
            ViewBag.Expenses = _humanResoursesContext.Expenses.Where(x => x.AppUserID == personel.Id).ToList();

            if (rol.Contains("Personel"))
            {
                await BilgiAlma();
                return View(personel);
            }
            else
            {
        
                return RedirectToAction("Index", "Yonetim", new { area = "Yonetici" });
            }

           
        }

        public async Task<IActionResult> Profilim()
        {
            await BilgiAlma();
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);           
            return View(personel);
        }

      

        [HttpGet]
        public async Task<IActionResult> PersonelGuncelle()
        {
            //var personel = await _userManager.FindByEmailAsync(User.Identity.Name);

            await BilgiAlma();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PersonelGuncelle(UpdateUserModel model)
        {

            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewBag.ID = personel.Id;

            personel.Address = model.Address;
            personel.PhoneNumber = model.PhoneNumber;

            if (model.ImageURL != null)
            {
                if (model.ImageURL.ContentType == "image/png" || model.ImageURL.ContentType == "image/jpeg")
                {
                    var extension = Path.GetExtension(model.ImageURL.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImages/", imageName);
                    var stream = new FileStream(location, FileMode.Create);
                    model.ImageURL.CopyTo(stream);
                    personel.ImageURL = "/userimages/"+imageName;
                }
                else
                {
                    TempData["mesaj"] = "Dosya uzantısı sadece jpeg veya png olabilir";
                }


            }
            else
            {
                personel.ImageURL = personel.ImageURL;

            }

            var result = await _userManager.UpdateAsync(personel);
            if (result.Succeeded)
            {
                
                return RedirectToAction("Anasayfa");
            }

            return View();
        }

        

        public  async Task BilgiAlma() 
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["resim"] = personel.ImageURL.ToString();
            ViewData["isim"] = personel.Name.ToUpper() +" "+ personel.Surname.ToUpper();
        }


     

    }
}
