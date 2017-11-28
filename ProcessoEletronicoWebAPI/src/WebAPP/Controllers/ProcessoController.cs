using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPP.Controllers
{
    public class ProcessoController : BaseController
    {
        private IProcessoService _service;

        public ProcessoController(IProcessoService service)
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
        public IActionResult SearchByOrganizacao()
        {
            IEnumerable<GetProcessoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao();
            return View("processosPorOrganizacao", processosPorOrganizacao);
        }       
        
    }
}
