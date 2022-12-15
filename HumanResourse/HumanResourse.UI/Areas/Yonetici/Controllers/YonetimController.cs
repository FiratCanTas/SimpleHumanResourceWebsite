using HumanResourse.DataAccess.Context;
using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using HumanResourse.UI.Areas.Kullanici.Models.Personel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HumanResourse.UI.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
    public class YonetimController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly HumanResoursesContext _humanResoursesContext;

        public YonetimController(UserManager<AppUser> userManager, HumanResoursesContext humanResoursesContext)
        {
            _userManager = userManager;
            _humanResoursesContext = humanResoursesContext;
        }


        public async Task<IActionResult> Index()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);

            ViewBag.Permits = _humanResoursesContext.Permits.Where(x => x.Statu == Position.Beklemede).Include("AppUsers").ToList();
            ViewBag.AdvancePayments = _humanResoursesContext.AdvancePayments.Where(x => x.Statu == Position.Beklemede).Include("AppUsers").ToList();
            ViewBag.Expenses = _humanResoursesContext.Expenses.Where(x => x.Statu == Position.Beklemede).Include("AppUsers").ToList();


            await BilgiAlma();
            return View(personel);
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
                    personel.ImageURL = "/userimages/" + imageName;
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

                return RedirectToAction("Index");
            }

            return View();
        }


        public async Task BilgiAlma()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["resim"] = personel.ImageURL.ToString();
            ViewData["isim"] = personel.Name.ToUpper() + " " + personel.Surname.ToUpper();
        }


    }
}
