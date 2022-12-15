using HumanResourse.Entitiy.Concrete;
using HumanResourse.UI.Areas.Islemler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HumanResourse.Entitiy.Enums;
using HumanResourse.Business.Concrete;
using HumanResourse.Business.Abstract;
using HumanResourse.DataAccess.Context;

namespace HumanResourse.UI.Areas.Islemler.Controllers
{
	[Area("Islemler")]
	public class IzinController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IPermitService _permitService;

        private readonly HumanResoursesContext _humanResoursesContext;

        public IzinController(UserManager<AppUser> userManager, IPermitService permitService, HumanResoursesContext humanResoursesContext)
        {
            _userManager = userManager;
            _permitService = permitService;
            _humanResoursesContext = humanResoursesContext;
        }



        public async Task<IActionResult> IzinListele()
		{
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            var izinler = _humanResoursesContext.Permits.Where(x => x.AppUserID == personel.Id).ToList();
            var onayli = _humanResoursesContext.Permits.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Onaylandi).ToList();
            var rededildi = _humanResoursesContext.Permits.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Rededildi).ToList();
            var bekleyen = _humanResoursesContext.Permits.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Beklemede).ToList();
            ViewBag.izinList = izinler;
            ViewBag.Onay = onayli;
            ViewBag.Reddedilen = rededildi;
            ViewBag.Bekleyen = bekleyen;
            await BilgiAlma();
            return View();
        }


        //public async Task<IActionResult> IzinListele()
        //{
        //    var personel = await _userManager.FindByNameAsync(User.Identity.Name);

        //    var izinler = _humanResoursesContext.Permits.Where(x => x.AppUserID == personel.Id).ToList();

        //    ViewBag.izinList = izinler;
        //    ViewBag.mesaj = izinler.Count;
        //    await BilgiAlma();
        //    return View();

        //}

        public async Task<IActionResult> IzinEkle()
		{
            await BilgiAlma();
            return View();
		}

		[HttpPost]
        public async Task<IActionResult> IzinEkle(PermitCreateModel model)
        {
            var personel = await _userManager.FindByNameAsync(User.Identity.Name);
			int izinHakki = IzinGunuHesapla(personel);
			
            Permit izin = new Permit();

            izin.AppUserID = personel.Id;

            if (personel.Gender == Gender.Kadin && model.permitType == PermitTypee.Babalıkİzni )
			{
                TempData["mesaj"] = "Bu izin tipini seçemezsiniz";
				return View();

            }
			if (personel.Gender == Gender.Erkek && model.permitType == PermitTypee.Sütİzni || model.permitType == PermitTypee.Doğumİzni)
			{
                TempData["mesaj"] = "Bu izin tipini seçemezsiniz";
				return View();
            }
			else
			izin.permitType = model.permitType;

            double gunSayisi = (model.EndDate - model.StartDate).TotalDays;

            if (izin.permitType == PermitTypee.Babalıkİzni)
			{
				if (gunSayisi>5)
				{
                    TempData["mesaj"] = "Babalık izni için en fazla 5 gün seçebilirsiniz";
                    return View();

                }
			}
            if (izin.permitType == PermitTypee.Evlenmeİzni)
            {
                if (gunSayisi > 3)
                {
                    TempData["mesaj"] = "Evlilik izni için en fazla 3 gün seçebilirsiniz";
                    return View();

                }
            }
            if (izin.permitType == PermitTypee.Sütİzni)
            {
				//Burada süt izni haftad
                if (gunSayisi > 2)
                {
                    TempData["mesaj"] = "Haftalık süt izni için en fazla 2 gün seçebilirsiniz";
                    return View();

                }
            }
            if (izin.permitType == PermitTypee.Doğumİzni)
            {
                if (gunSayisi > 180)
                {
                    TempData["mesaj"] = "Doğum izni için en fazla 6 ay (180 gün) seçebilirsiniz";
                    return View();

                }
            }
            if (izin.permitType == PermitTypee.Ücretsizİzin)
            {
                if (gunSayisi > 90)
                {
                    TempData["mesaj"] = "Ücretsiz izin en fazla 3 ay (90 gün) seçebilirsiniz";
                    return View();

                }
            }
            if (izin.permitType == PermitTypee.Mazeretİzni)
            {
                if (gunSayisi > 3)
                {
                    TempData["mesaj"] = "Mazeret izni için en fazla 3 gün seçebilirsiniz";
                    return View();

                }
            }
            if (izin.permitType == PermitTypee.İşAramaİzni)
            {
                if (gunSayisi > 1)
                {
                    TempData["mesaj"] = "İş arama izni için en fazla 1 gün seçebilirsiniz";
                    return View();

                }
            }


            if (gunSayisi<0)
			{
				TempData["mesaj"] = "Başlangıç tarihinden eski bir tarih seçemezsiniz.";
                return View();
            }

            izin.StartDate = model.StartDate;
            izin.EndDate = model.EndDate;
            izin.PermitDay = izinHakki;

			if (gunSayisi > izinHakki)
			{
				TempData["mesaj"] = "Bu izin için yeterli hakkınız bulunmamaktadır";
				return View();
            }

			//bu kısım izin onaylandığında düşülecektir. Şimdilik bu şekilde.
            izin.PermitDay = (int)(izinHakki - gunSayisi);

            _permitService.TAdd(izin);

            TempData["mesaj"] = "İzin talebiniz alınmıştır";

            return RedirectToAction("IzinListele");
        }

        
		public int IzinGunuHesapla(AppUser personel)
		{
			int izinHakki=0;
			var calismaZamani = (DateTime.Now - personel.HiredDate).TotalDays;

			if (calismaZamani < 1825)
				izinHakki = 14;	
			
			if (calismaZamani >= 1825 && calismaZamani < 5475)
				izinHakki = 20;

			if (calismaZamani>=5475)
			
				izinHakki = 26;
				

			return izinHakki;
        }


        public async Task BilgiAlma()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["resim"] = personel.ImageURL;
            ViewData["isim"] = personel.Name.ToUpper() +" "+personel.Surname.ToUpper();
        }
    }
}
