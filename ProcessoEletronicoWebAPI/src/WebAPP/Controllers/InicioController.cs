using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPP.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }        

        public async Task<IActionResult>  Logout()
        {
            await HttpContext.Authentication.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Inicio");
        }
    }
}
