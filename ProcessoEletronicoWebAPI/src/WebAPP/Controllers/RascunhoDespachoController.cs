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
        private IProcessoService _processoService;

        public RascunhoDespachoController(
            IOrganogramaAppService organogramaService, 
            IRascunhoDespachoAppService rascunhoDespachoAppService,
            IProcessoService processoService)
        {
            _organogramaService = organogramaService;
            _rascunhoDespachoAppService = rascunhoDespachoAppService;
            _processoService = processoService;
        }
        
        public IActionResult Index()
        {
            ResultViewModel<ICollection<RascunhoDespachoViewModel>> result = _rascunhoDespachoAppService.Search();            
            SetMensagens(result.Mensagens);

            return View("Index", result.Entidade);
        }

        public IActionResult SearchToImport(int? idProcesso, int? idAtividade)
        {
            ResultViewModel<ICollection<RascunhoDespachoViewModel>> result = _rascunhoDespachoAppService.Search();
            SetMensagens(result.Mensagens);

            foreach(var r in result.Entidade)
            {
                r.IdProcesso = idProcesso.Value;
                r.IdAtividade = idAtividade.Value;
            }

            return PartialView("ListaImportacaoRascunhoDespacho", result.Entidade);
        }

        public IActionResult Search()
        {
            ResultViewModel<ICollection<RascunhoDespachoViewModel>> result = _rascunhoDespachoAppService.Search();
            SetMensagens(result.Mensagens);

            return PartialView("ListaRascunhoDespacho", result.Entidade);
        }

        public IActionResult View(int id)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Search(id);
            SetMensagens(result.Mensagens);

            return View(result.Entidade);
        }

        public IActionResult Add(RascunhoDespachoViewModel rascunhoDespacho, int? idProcesso, int? idAtividade, bool ajax=false)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Add(rascunhoDespacho);
            SetMensagens(result.Mensagens);

            if (result.Entidade != null)
            {
                if (ajax)
                {
                    result.Entidade.IdProcesso = idProcesso.Value;
                    result.Entidade.IdAtividade = idAtividade.Value;

                    return UpdateFormDespacho(result.Entidade);
                }
                else
                {
                    return RedirectToAction("Update", new { id = result.Entidade.Id });
                }                
            }
            else
            {
                return RedirectToAction("Index");
            }            
        }

        [HttpGet]
        public IActionResult UpdateFormDespacho(RascunhoDespachoViewModel rascunhoDespacho)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Clone(rascunhoDespacho.Id);

            result.Entidade.ListaOrganizacoes = _organogramaService.GetOrganizacoesPorPatriarca();

            string guidOrganizacao = result.Entidade.GuidOrganizacaoDestino;
            result.Entidade.ListaUnidades = !string.IsNullOrEmpty(guidOrganizacao) ? _organogramaService.GetUniadesPorOrganizacao(guidOrganizacao) : null;

            result.Entidade.IdProcesso = rascunhoDespacho.IdProcesso;
            result.Entidade.IdAtividade = rascunhoDespacho.IdAtividade;
            result.Entidade.ListaTiposDocumentais = _processoService.GetTiposDocumentais(rascunhoDespacho.IdAtividade);

            SetMensagens(result.Mensagens);

            return PartialView("UpdateFormDespacho", result.Entidade);
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

        public IActionResult Delete(int id, bool ajax=false)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = _rascunhoDespachoAppService.Delete(id);
            SetMensagens(result.Mensagens);

            if (ajax)
            {
                return Search();
            }

            return Index();            
        }
    }
}
