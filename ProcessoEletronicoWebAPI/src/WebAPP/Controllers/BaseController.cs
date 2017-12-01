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
        public BaseController()
        {            
        }

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
