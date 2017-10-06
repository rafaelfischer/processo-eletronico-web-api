using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet]
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
            //IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao(User.Claims.First(a => a.Type ==""));
            IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao();
            return View("processosPorOrganizacao", processosPorOrganizacao);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CaixaRascunhos()
        {
            IEnumerable<GetRascunhoProcessoViewModel> rascunhosPorOrganizacao = _service.GetRascunhosOrganizacao();
            return View("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CaixaSaida()
        {
            
            IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao();
            return View("processosPorOrganizacao", processosPorOrganizacao);
        }
    }
}
