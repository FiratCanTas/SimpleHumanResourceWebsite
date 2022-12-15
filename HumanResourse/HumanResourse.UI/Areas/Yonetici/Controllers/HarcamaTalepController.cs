using HumanResourse.Business.Abstract;
using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourse.UI.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
    public class HarcamaTalepController : Controller
    {
       private readonly IExpensesService _expensesService;
        private readonly UserManager<AppUser> _userManager;

        public HarcamaTalepController(IExpensesService expensesService, UserManager<AppUser> userManager)
        {
            _expensesService = expensesService;
            _userManager = userManager;
        }

        public async Task<IActionResult> HarcamaTalepListesi()
        {

            var onaysiz = _expensesService.TGetListByUserName().Where(x => x.Statu == Position.Beklemede).OrderByDescending(X => X.RequestDate).ToList();
            var onayli = _expensesService.TGetListByUserName().Where(x => x.Statu != Position.Beklemede).OrderByDescending(X => X.RequestDate).ToList();
            var liste = onaysiz.Concat(onayli).ToList();

            if (liste.Count() != 0)
            {
                ViewBag.HarcamaTalep = liste;
            }
            else
            {
                ViewBag.HarcamaTalep = "Harcama talep listesi boş!";
            }

            await BilgiAlma();
            return View();
        }

        public async Task<IActionResult> IzinOnayla(int id)
        {
            var personel = _expensesService.TGetByID(id);
            personel.Statu = Position.Onaylandi;
            personel.ReponseDate = DateTime.Now;
            _expensesService.TUpdate(personel);

            return RedirectToAction("HarcamaTalepListesi");
        }

        public async Task<IActionResult> IzinRed(int id)
        {
            var personel = _expensesService.TGetByID(id);
            personel.Statu = Position.Rededildi;
            personel.ReponseDate = DateTime.Now;
            _expensesService.TUpdate(personel);

            return RedirectToAction("HarcamaTalepListesi");
        }

        public async Task BilgiAlma()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["resim"] = personel.ImageURL.ToString();
            ViewData["isim"] = personel.Name.ToUpper() + " " + personel.Surname.ToUpper();
        }

    }
}
