using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public IActionResult Login()
        {
            return new ChallengeResult("processoeletronico", new AuthenticationProperties() { RedirectUri = "/" });
        }

        public IActionResult Sair()
        {
            return SignOut("processoeletronico", CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        protected void SetMensagens (ICollection<MensagemViewModel> mensagens)
        {
            string mensagemJSON = JsonConvert.SerializeObject(mensagens);
            ViewBag.Mensagens = mensagemJSON;
        }
    }
}
