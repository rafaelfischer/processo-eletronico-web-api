﻿using Microsoft.AspNetCore.Mvc;


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
    }
}
