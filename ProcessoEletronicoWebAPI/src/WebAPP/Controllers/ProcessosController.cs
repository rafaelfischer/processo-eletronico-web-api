using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPP.Controllers
{
    public class ProcessosController : BaseController
    {
        private IProcessoService _service;

        public ProcessosController(IProcessoService service)
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

        [HttpGet]
        public IActionResult Search(int id)
        {
            ResultViewModel<GetProcessoViewModel> getProcessoResult = _service.Search(id);

            return View("VisualizacaoProcesso", getProcessoResult.Entidade);
        }

        [HttpPost]
        public IActionResult ConsultaProcessoPorNumero(string numero)
        {
            GetProcessoBasicoViewModel getProcesso = _service.GetProcessoPorNumero(numero);
            return View("ConsultaProcesso", getProcesso);
        }        

        [HttpGet]
        public IActionResult SearchByOrganizacao()
        {
            IEnumerable<GetProcessoBasicoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao();
            return View("processosPorOrganizacao", processosPorOrganizacao);
        }       
        
    }
}
