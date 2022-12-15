using HumanResourse.Business.Abstract;
using HumanResourse.DataAccess.Context;
using HumanResourse.Entitiy.Concrete;
using HumanResourse.Entitiy.Enums;
using HumanResourse.UI.Areas.Islemler.Models;
using HumanResourse.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourse.UI.Areas.Islemler.Controllers
{
    [Area("Islemler")]
    public class HarcamaController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IExpensesService _expensesService;
        private readonly HumanResoursesContext _humanResoursesContext;
        public HarcamaController(UserManager<AppUser> userManager, IExpensesService expensesService, HumanResoursesContext humanResoursesContext)
        {
            _userManager = userManager;
            _expensesService = expensesService;
            _humanResoursesContext = humanResoursesContext;
        }
        //Harcama Listeleme
        public async Task<IActionResult> Index()
        {

            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            var harcama = _humanResoursesContext.Expenses.Where(x => x.AppUserID == personel.Id).ToList();
            var onayli = _humanResoursesContext.Expenses.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Onaylandi).ToList();
            var rededildi = _humanResoursesContext.Expenses.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Rededildi).ToList();
            var bekleyen = _humanResoursesContext.Expenses.Where(x => x.AppUserID == personel.Id && x.Statu == Position.Beklemede).ToList();
            ViewBag.HarcamaList = harcama;
            ViewBag.Onay = onayli;
            ViewBag.Reddedilen = rededildi;
            ViewBag.Bekleyen = bekleyen;
            await BilgiAlma();
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> HarcamaSayfasi()
        {
            await BilgiAlma();
            return View();
        }



        //Model içerisinde IFormFile tipinde documantation bilgisi tutuyorum.
        //IFormFile dosyamın uzantısı, boyutu, başlık bilgisi gibi değerler alır.
        //Path.GetExtension ile içerisine gelen dosyamın isminden onu bulup uzantısını almış oldum(Örn jpg, pdf)
        //Benzersiz bir isim atamak için $ ile {} parantezleri yan yana yazarak bir benzersiz guid değeri yaratıp dosya uzantı ismiyle birleştirdim.(Örn "a1s2k48djd6f.jpg")
        //Path.Combine birleştirmek için kullandım. İlk olarak, Directory.GetCurrentDirectory() --> Projemin uygulamamın yolunu alan değerdir. İkinci olarak, proje dizinimi birleştirmek istediğim yeri "wwwroot/ExpenseFiles" olarak belirttim. Son olarak, randomName ile dosya adımı belirterek bu üç değeri birleştirmiş oldum. 
        //FileStream senkron veya asenkron okuma yazma işlemini yapar.
        //(Path, FileMode.Create) --> Gideceği yol ve yapacağı işi belirttim.
        //CopyToAsync(stream) --> Oluşturduğum stream i ilgili adrese kaydettim.

        [HttpPost]
        public async Task<IActionResult> HarcamaSayfasi(ExpensesCreateModel model)
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);


            Expenses expenses = new Expenses();


            expenses.ExpensesAmount = (double)model.ExpensesAmount;
            expenses.ExpensesType = model.ExpensesType;
            switch (model.CurrencyUnit.ToString())
            {
                case "1":
                    expenses.CurrencyUnit = "Türk Lirası";
                    break;
                case "2":
                    expenses.CurrencyUnit = "Amerikan Doları";
                    break;
                case "3":
                    expenses.CurrencyUnit = "Euro";
                    break;
                case "4":
                    expenses.CurrencyUnit = "İngiliz Sterlini";
                    break;
            }


            expenses.AppUserID = personel.Id;

            if (model.Documentation != null)
            {
                if (model.Documentation.ContentType == "application/pdf")
                {
                    var extension = Path.GetExtension(model.Documentation.FileName);
                    var filesName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documentation/", filesName);
                    var stream = new FileStream(location, FileMode.Create);
                    model.Documentation.CopyTo(stream);
                    expenses.Documentation = "/documentation/" + filesName;
                    _expensesService.TAdd(expenses);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mesaj = "Dosya uzantısı sadece pdf olabilir.";
                    
                }


            }
            else
            {
                ViewBag.Mesaj = "Dosya bilgisi boş geçilemez.";
            }

       
            return View();
          
      
        }
        public async Task BilgiAlma()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["resim"] = personel.ImageURL;
            ViewData["isim"] = personel.Name.ToUpper() + " " + personel.Surname.ToUpper();
        }
    }
}



 

