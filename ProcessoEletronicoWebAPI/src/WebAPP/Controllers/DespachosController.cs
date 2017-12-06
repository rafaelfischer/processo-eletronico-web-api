using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Controllers
{
    public class DespachosController : BaseController
    {
        private IDespachoService _service;
        private IProcessoService _processoService;

        public DespachosController(IDespachoService service, IProcessoService processoService)
        {
            _service = service;
            _processoService = processoService;
        }

        [HttpGet]
        public IActionResult Search(int id)
        {
            ResultViewModel<GetDespachoViewModel> despachoResultViewModel = _service.Search(id);
            return PartialView("_VisualizacaoDespacho", despachoResultViewModel.Entidade);
        }

        [HttpGet]
        public IActionResult Despachar(int id)
        {
            ResultViewModel<GetProcessoViewModel> result = _processoService.Search(id);
            return View("DespacharProcesso", result.Entidade);
        }

        [HttpGet]
        public IActionResult OpcoesCarregamento()
        {            
            return PartialView("_opcoesCarregamentoRascunhoDespacho");
        }
    }
}
