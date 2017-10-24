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
            IEnumerable<RascunhoProcessoViewModel> rascunhosPorOrganizacao = _service.GetRascunhosProcessoPorOrganizacao();
            return View("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Editar(int? id)
        {
            RascunhoProcessoViewModel rascunho = _service.EditRascunhoProcesso(id);
            return View("Editar", rascunho);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Editar(RascunhoProcessoViewModel rascunho)
        {
            _service.UpdateRascunhoProcesso(rascunho.Id, rascunho);
            return RedirectToAction("/Editar/"+ rascunho.Id);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Excluir(int id)
        {
            _service.DeleteRascunhoProcesso(id);

            return RedirectToAction("Index");

            //IEnumerable<GetRascunhoProcessoViewModel> rascunhosPorOrganizacao = _service.GetRascunhosOrganizacao();
            //return PartialView("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }
    }
}
