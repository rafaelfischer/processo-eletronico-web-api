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
        public BaseController()
        {            
        }

        public IActionResult Login()
        {
            return new ChallengeResult("processoeletronico", new AuthenticationProperties() { RedirectUri = "/" });
        }

        public IActionResult Sair()
        {
            //return SignOut("processoeletronico", CookieAuthenticationDefaults.AuthenticationScheme);
            var logout = CookieAuthenticationDefaults.AuthenticationScheme;

            return RedirectToAction("Index", "Inicio");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        protected void SetMensagens(ICollection<MensagemViewModel> mensagens)
        {
            if (mensagens != null)
            {
                var _mensagens = HttpContext.Items["messages"] as List<MensagemViewModel>;
                _mensagens = _mensagens ?? new List<MensagemViewModel>();
                _mensagens.AddRange(mensagens);
                HttpContext.Items["messages"] = _mensagens;
            }

            string mensagemJSON = JsonConvert.SerializeObject(HttpContext.Items["messages"] as ICollection<MensagemViewModel>);
            ViewBag.Mensagens = mensagemJSON;
        }        
    }
}
