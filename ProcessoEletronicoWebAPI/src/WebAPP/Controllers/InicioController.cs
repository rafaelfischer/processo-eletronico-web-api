using Microsoft.AspNetCore.Mvc;


namespace WebAPP.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
