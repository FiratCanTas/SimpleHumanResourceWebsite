using HumanResourse.Entitiy.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourse.UI.Areas.Rol.Controllers
{
    [Area("Rol")]
    public class RollerController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RollerController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult RolListeleme()
        {

            var values = _roleManager.Roles.ToList();
            return View(values);
        }


        [HttpGet]
        public IActionResult RolEkle()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RolEkle(AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(appRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("RolListeleme");
                }
            }

            return View();
        }
    }
}
