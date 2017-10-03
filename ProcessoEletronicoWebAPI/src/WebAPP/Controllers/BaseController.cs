﻿using Apresentacao.APP.WorkServices.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
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
    }
}