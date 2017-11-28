using Microsoft.AspNetCore.Mvc;


namespace WebAPP.Controllers
{
    public class InicioController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
