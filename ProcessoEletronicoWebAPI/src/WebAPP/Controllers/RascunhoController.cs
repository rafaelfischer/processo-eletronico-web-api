using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class RascunhoController : BaseController
    {
        private IProcessoService _processo;
        private IRascunhoProcessoService _rascunho;
        private IRascunhoProcessoAbrangenciaService _municipioService;
        private IRascunhoProcessoSinalizacaoService _sinalizacaoService;
        private IRascunhoProcessoAnexoService _anexoService;
        private IOrganogramaAppService _organogramaService;
        private IRascunhoProcessoInteressadoService _interessadoService;

        public RascunhoController(
            IProcessoService processo,
            IRascunhoProcessoService rascunho,
            IRascunhoProcessoAbrangenciaService municipio,
            IRascunhoProcessoSinalizacaoService sinalizacao,
            IRascunhoProcessoAnexoService anexoService,
            IOrganogramaAppService organogramaService,
            IRascunhoProcessoInteressadoService interessadoService)
        {
            _processo = processo;
            _rascunho = rascunho;
            _municipioService = municipio;
            _sinalizacaoService = sinalizacao;
            _anexoService = anexoService;
            _organogramaService = organogramaService;
            _interessadoService = interessadoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<RascunhoProcessoViewModel> rascunhosPorOrganizacao = _rascunho.GetRascunhosProcessoPorOrganizacao();
            return View("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpPost]
        public IActionResult Visualizar(int id)
        {
            var rascunho = _rascunho.GetRascunhoProcesso(id);
            ViewBag.Mensagens = JsonConvert.SerializeObject(rascunho.Mensagens);

            if (rascunho.Entidade!=null){                
                return PartialView("RascunhoVisualizar", rascunho.Entidade);
            }
            else
            {            
                return PartialView("RascunhoVisualizar", null);
            }
            
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            RascunhoProcessoViewModel rascunho = _rascunho.EditRascunhoProcesso(id);
            return View("Editar", rascunho);
        }

        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Edit")]
        public IActionResult EditarBasico(RascunhoProcessoViewModel rascunho)
        {
            _rascunho.UpdateRascunhoProcesso(rascunho.Id, rascunho);

            if (rascunho.Sinalizacoes != null && rascunho.Sinalizacoes.Count > 0)
                _sinalizacaoService.PostSinalizacao(rascunho.Id, rascunho.Sinalizacoes);

            ResultViewModel<RascunhoProcessoViewModel> result = _rascunho.GetForEditRascunhoProcesso(rascunho.Id);
            SetMensagens(result.Mensagens);
            
            return PartialView("RascunhoBasico", result.Entidade);
        }

        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Edit")]
        public IActionResult Excluir(int id)
        {
            ResultViewModel<RascunhoProcessoViewModel> result = _rascunho.DeleteRascunhoProcesso(id);
            SetMensagens(result.Mensagens);

            IEnumerable<RascunhoProcessoViewModel> rascunhosPorOrganizacao = _rascunho.GetRascunhosProcessoPorOrganizacao();
            return PartialView("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpPost]
        [Authorize(Policy = "Processo.Edit")]
        public IActionResult AutuarProcessoPorIdRascunho(int id)
        {
            ResultViewModel<GetProcessoViewModel> result = _processo.AutuarPorIdRascunho(id);
            SetMensagens(result.Mensagens);

            return PartialView("RascunhoRetornoAutuacao", result);
        }
    }
}
