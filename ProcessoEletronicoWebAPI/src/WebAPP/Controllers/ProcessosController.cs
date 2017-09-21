using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Controllers
{
    public class ProcessosController : Controller
    {
        private IProcessoService _service;

        public ProcessosController(IProcessoService service)
        {
            _service = service;
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
    }
}
