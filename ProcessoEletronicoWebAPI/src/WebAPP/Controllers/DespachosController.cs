using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Controllers
{
    public class DespachosController : BaseController
    {
        private IDespachoService _service;

        public DespachosController(IDespachoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Search(int id)
        {
            ResultViewModel<GetDespachoViewModel> despachoResultViewModel = _service.Search(id);
            return PartialView("_VisualizacaoDespacho", despachoResultViewModel.Entidade);
        }
    }
}
