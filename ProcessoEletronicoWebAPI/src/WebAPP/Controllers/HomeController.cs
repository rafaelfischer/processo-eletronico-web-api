using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPP.Controllers
{
    public class HomeController : BaseController
    {
        private IProcessoService _service;

        public HomeController(IProcessoService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {   
            return View();
        }

        [HttpGet]
        public IActionResult ConsultaProcesso()
        {
            return View(null);
        }

        [HttpPost]
        public IActionResult ConsultaProcessoPorNumero(string numero)
        {
            GetProcessoViewModel getProcesso = _service.GetProcessoPorNumero(numero);
            return View("ConsultaProcesso", getProcesso);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var token = HttpContext.Authentication.GetTokenAsync("access_token").Result;
            var id_token = HttpContext.Authentication.GetTokenAsync("id_token").Result;
            var tudo = HttpContext.Authentication.GetAuthenticateInfoAsync("processoeletronico");

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CaixaEntrada()
        {   
            IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao("3ca6ea0e-ca14-46fa-a911-22e616303722");
            return View("processosPorOrganizacao", processosPorOrganizacao);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CaixaRascunho()
        {
            IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao("3ca6ea0e-ca14-46fa-a911-22e616303722");
            return View("processosPorOrganizacao", processosPorOrganizacao);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CaixaSaida()
        {
            IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao("3ca6ea0e-ca14-46fa-a911-22e616303722");
            return View("processosPorOrganizacao", processosPorOrganizacao);
        }
    }
}
