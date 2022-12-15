using HumanResourse.Business.Abstract;
using HumanResourse.DataAccess.Context;
using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourse.UI.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
	public class IzinTalepController : Controller
	{
        private readonly IPermitService _permitService;
        private readonly UserManager<AppUser> _userManager;

        public IzinTalepController(IPermitService permitService, UserManager<AppUser> userManager)
        {
            _permitService = permitService;
            _userManager = userManager;
        }
        public async Task<IActionResult> IzinTalepListesi()
		{
            var izinTalepleri = _permitService.TGetListByUserName().Where(x=>x.Statu == Position.Beklemede).OrderByDescending(x => x.RequestDate).ToList();
            var izinTalepleri2 = _permitService.TGetListByUserName().Where(x => x.Statu != Position.Beklemede).OrderByDescending(x => x.RequestDate).ToList();
            var liste = izinTalepleri.Concat(izinTalepleri2).ToList();

            if (liste.Count() != 0)
            {

                ViewBag.izinTalep = liste;
            }
            else
            {
                ViewBag.izinTalep = "İzin talep listesi boş!";
            }


            await BilgiAlma();
            return View();
		}



        public IActionResult IzinOnayla(int id)
        {
            var onaylanacakTalep = _permitService.TGetByID(id);

            onaylanacakTalep.Statu = Position.Onaylandi;
            onaylanacakTalep.ReponseDate = DateTime.Now;
            _permitService.TUpdate(onaylanacakTalep);

            return RedirectToAction("IzinTalepListesi");
        }


        public IActionResult IzinReddet(int id)
        {
            var onaylanacakTalep = _permitService.TGetByID(id);

            onaylanacakTalep.Statu = Position.Rededildi;
            onaylanacakTalep.ReponseDate = DateTime.Now;
            _permitService.TUpdate(onaylanacakTalep);

            return RedirectToAction("IzinTalepListesi");
        }

        public async Task BilgiAlma()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["resim"] = personel.ImageURL.ToString();
            ViewData["isim"] = personel.Name.ToUpper() + " " + personel.Surname.ToUpper();
        }
    }
}
