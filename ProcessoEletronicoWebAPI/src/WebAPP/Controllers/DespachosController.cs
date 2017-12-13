using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Controllers
{
    public class DespachosController : BaseController
    {
        private IDespachoService _service;
        private IProcessoService _processoService;
        private IRascunhoDespachoAppService _rascunhoDespachoAppService;

        public DespachosController(
            IDespachoService service, 
            IProcessoService processoService,
            IRascunhoDespachoAppService rascunhoDespachoAppService
            )
        {
            _service = service;
            _processoService = processoService;
            _rascunhoDespachoAppService = rascunhoDespachoAppService;
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
            SetMensagens(result.Mensagens);

            return View("DespacharProcesso", result.Entidade);
        }

        [HttpGet]
        public IActionResult OpcoesCarregamento(CarregamentoRascunhoDespachoViewModel opcoes, bool delete = false)
        {
            return PartialView("_OpcoesCarregamentoRascunhoDespacho", opcoes);
        }

        [HttpPost]
        public IActionResult Despachar(int idProcesso, int idRascunhoDespacho)
        {
            ResultViewModel<GetDespachoViewModel> result = _service.Despachar(idProcesso, idRascunhoDespacho);
            SetMensagens(result.Mensagens);

            if (result.Success)
                _rascunhoDespachoAppService.Delete(idRascunhoDespacho);

            return Json(result);
        }
    }
}
