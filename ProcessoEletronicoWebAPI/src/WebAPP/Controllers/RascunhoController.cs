using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class RascunhoController : BaseController
    {   
        private IRascunhoService _service;        

        public RascunhoController(IRascunhoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {            
            IEnumerable<GetRascunhoProcessoViewModel> rascunhosPorOrganizacao = _service.GetRascunhosOrganizacao();
            return View("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            RascunhoProcessoModeloNegocio rascunho =  _service.PostRascunho();
            AutuacaoInicioViewModel formularioInicial = _service.GetFormularioInicioAutuacao();
            formularioInicial.IdRascunho = rascunho.Id;

            return View(formularioInicial);
        }
    }
}
