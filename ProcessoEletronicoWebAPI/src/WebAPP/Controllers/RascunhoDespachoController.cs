using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class RascunhoDespachoController : BaseController
    {
        private IOrganogramaAppService _organogramaService;
        private IRascunhoDespachoAppService _rascunhoDespachoAppService;

        public RascunhoDespachoController(
            IOrganogramaAppService organogramaService, 
            IRascunhoDespachoAppService rascunhoDespachoAppService)
        {
            _organogramaService = organogramaService;
            _rascunhoDespachoAppService = rascunhoDespachoAppService;
        }
        
        public IActionResult Index()
        {
            ResultViewModel<ICollection<RascunhoDespachoViewModel>> result = _rascunhoDespachoAppService.Search();            
            SetMensagens(result.Mensagens);

            return View("Index", result.Entidade);
        }

        public IActionResult Search()
        {
            ResultViewModel<ICollection<RascunhoDespachoViewModel>> result = _rascunhoDespachoAppService.Search();
            SetMensagens(result.Mensagens);

            return PartialView("ListaImportacaoRascunhoDespacho", result.Entidade);
        }

        public IActionResult View(int id)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Search(id);
            SetMensagens(result.Mensagens);

            return View(result.Entidade);
        }

        public IActionResult Add(RascunhoDespachoViewModel rascunhoDespacho)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Add(rascunhoDespacho);
            result.Entidade.ListaOrganizacoes = _organogramaService.GetOrganizacoesPorPatriarca();

            string guidOrganizacao = result.Entidade.GuidOrganizacaoDestino;
            result.Entidade.ListaUnidades = !string.IsNullOrEmpty(guidOrganizacao) ? _organogramaService.GetUniadesPorOrganizacao(guidOrganizacao) : null;

            SetMensagens(result.Mensagens);

            return View("Update", result.Entidade);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Search(id);
            result.Entidade.ListaOrganizacoes = _organogramaService.GetOrganizacoesPorPatriarca();

            string guidOrganizacao = result.Entidade.GuidOrganizacaoDestino;
            result.Entidade.ListaUnidades = !string.IsNullOrEmpty(guidOrganizacao) ? _organogramaService.GetUniadesPorOrganizacao(guidOrganizacao) : null;

            SetMensagens(result.Mensagens);

            return View(result.Entidade);
        }

        [HttpPost]
        public IActionResult Update(RascunhoDespachoViewModel rascunhoDespacho)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Update(rascunhoDespacho);
            SetMensagens(result.Mensagens);            

            if(result.Entidade != null) { 
                result.Entidade.ListaOrganizacoes = _organogramaService.GetOrganizacoesPorPatriarca();
                string guidOrganizacao = result.Entidade.GuidOrganizacaoDestino;
                result.Entidade.ListaUnidades = !string.IsNullOrEmpty(guidOrganizacao) ? _organogramaService.GetUniadesPorOrganizacao(guidOrganizacao) : null;
            }

            return PartialView("FormDadosBasicos", result.Entidade);
        }

        public IActionResult Delete(int id)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Delete(id);
            SetMensagens(result.Mensagens);

            return View(result.Entidade);
        }
    }
}
