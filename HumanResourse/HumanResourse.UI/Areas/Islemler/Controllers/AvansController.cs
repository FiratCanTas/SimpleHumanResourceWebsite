using HumanResourse.Business.Abstract;
using HumanResourse.DataAccess.Context;
using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using HumanResourse.UI.Areas.Islemler.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourse.UI.Areas.Islemler.Controllers
{
    [Area("Islemler")]
    public class AvansController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IAdvancePaymentService _advancePaymentService;
        private readonly HumanResoursesContext _humanResoursesContext;
        public AvansController(IAdvancePaymentService advancePaymentService, HumanResoursesContext humanResoursesContext, UserManager<AppUser> usermanager)
        {

            _advancePaymentService = advancePaymentService;
            _humanResoursesContext = humanResoursesContext;
            _userManager = usermanager;
        }

        //Avans Taleplerini Listele

        //public async Task<IActionResult> Index()
        //{
        //    var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
        //    var avans = _humanResoursesContext.AdvancePayments.Where(x => x.AppUserID == personel.Id).ToList();
        //    ViewBag.AvansList = avans;
        //    ViewBag.mesaj = avans.Count;
        //    await BilgiAlma();
        //    return View();


        //}

        [HttpGet]
        public async Task<IActionResult> AdvanceRequest()
        {
            await BilgiAlma();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdvanceRequest(AdvanceCreateModel model)
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                AdvancePayment advancePayment = new AdvancePayment();

                advancePayment.ExpensesAmount = (double)model.ExpensesAmount;
                advancePayment.advancePaymentType = model.advancePaymentType;
                switch (model.CurrencyUnit.ToString())
                {
                    case "1":
                        advancePayment.CurrencyUnit = "Türk Lirası";
                        break;
                    case "2":
                        advancePayment.CurrencyUnit = "Amerikan Doları";
                        break;
                    case "3":
                        advancePayment.CurrencyUnit = "Euro";
                        break;
                    case "4":
                        advancePayment.CurrencyUnit = "İngiliz Sterlini";
                        break;
                }

                //advancePayment.CurrencyUnit = model.CurrencyUnit;
                advancePayment.Description = model.Description;
                advancePayment.AppUserID = personel.Id;

                _advancePaymentService.TAdd(advancePayment);

                return RedirectToAction("Index");

            }

            return View();
        }


        public async Task<IActionResult> Index()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            var avans = _humanResoursesContext.AdvancePayments.Where(x => x.AppUserID == personel.Id).ToList();
            var onayli = _humanResoursesContext.AdvancePayments.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Onaylandi).ToList();
            var rededildi = _humanResoursesContext.AdvancePayments.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Rededildi).ToList();
            var bekleyen = _humanResoursesContext.AdvancePayments.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Beklemede).ToList();
            ViewBag.AvansList = avans;
            ViewBag.Onay = onayli;
            ViewBag.Reddedilen = rededildi;
            ViewBag.Bekleyen = bekleyen;    
            await BilgiAlma();
            return View();
        }
        public async Task BilgiAlma()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["resim"] = personel.ImageURL;
            ViewData["isim"] = personel.Name.ToUpper() +" "+ personel.Surname.ToUpper();
        }


    }
}
