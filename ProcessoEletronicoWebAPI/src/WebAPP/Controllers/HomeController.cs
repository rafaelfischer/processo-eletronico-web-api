using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
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
        [Authorize]
        public IActionResult CaixaEntrada()
        {
            //IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao(User.Claims.First(a => a.Type ==""));
            IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao();
            return View("processosPorOrganizacao", processosPorOrganizacao);
        }       
        
    }
}
