using HumanResourse.Business.Abstract;
using HumanResourse.DataAccess.Context;
using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourse.UI.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
    public class AvansTalepController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdvancePaymentService _advancePaymentService;
        private readonly HumanResoursesContext _humanResoursesContext;
        public AvansTalepController(UserManager<AppUser> userManager, IAdvancePaymentService advancePaymentService, HumanResoursesContext humanResoursesContext)
        {
            _userManager = userManager;
            _advancePaymentService = advancePaymentService;
            _humanResoursesContext = humanResoursesContext;
        }


        public async Task<IActionResult> AvansTalepListesi()
        {           
            var onayBekleyenTalepler = _advancePaymentService.TGetListByUserName().Where(x => x.Statu == Position.Beklemede).OrderByDescending(x=>x.RequestDate);
            var onaylananlar = _advancePaymentService.TGetListByUserName().Where(x => x.Statu != Position.Beklemede).OrderByDescending(x => x.RequestDate);

            var liste = onayBekleyenTalepler.Concat(onaylananlar).ToList();
            await BilgiAlma();

            if (liste.Count() != 0)
            {
                
                ViewBag.Sonuc = liste;
            }
            else
            {
                ViewBag.Sonuc = "Avans talep listesi boş!";
            }

            
            return View();
        }

        public IActionResult Onayla(int id)
        {
           var onaylanacakTalep =  _advancePaymentService.TGetByID(id);

            onaylanacakTalep.Statu = Position.Onaylandi;
            onaylanacakTalep.ReponseDate= DateTime.Now;
            _advancePaymentService.TUpdate(onaylanacakTalep);

            return RedirectToAction("AvansTalepListesi");
        }

        public IActionResult Reddet(int id)
        {
            var reddedilecekTalep = _advancePaymentService.TGetByID(id);

            reddedilecekTalep.Statu =Position.Rededildi;
            reddedilecekTalep.ReponseDate = DateTime.Now;
            _advancePaymentService.TUpdate(reddedilecekTalep);

            return RedirectToAction("AvansTalepListesi");
        }

        public async Task BilgiAlma()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["resim"] = personel.ImageURL.ToString();
            ViewData["isim"] = personel.Name.ToUpper() + " " + personel.Surname.ToUpper();
        }
    }
}
