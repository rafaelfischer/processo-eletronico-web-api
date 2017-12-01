using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;


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
