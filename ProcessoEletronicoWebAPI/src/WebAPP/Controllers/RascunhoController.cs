using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<RascunhoProcessoViewModel> rascunhosPorOrganizacao = _rascunho.GetRascunhosProcessoPorOrganizacao();
            return View("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Visualizar(int id)
        {

            ResultViewModel rascunho = _rascunho.GetRascunhoProcesso(id);
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
        [Authorize]
        public IActionResult Editar(int? id)
        {
            RascunhoProcessoViewModel rascunho = _rascunho.EditRascunhoProcesso(id);
            return View("Editar", rascunho);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditarBasico(RascunhoProcessoViewModel rascunho)
        {
            _rascunho.UpdateRascunhoProcesso(rascunho.Id, rascunho);

            if (rascunho.Sinalizacoes != null && rascunho.Sinalizacoes.Count > 0)
                _sinalizacaoService.PostSinalizacao(rascunho.Id, rascunho.Sinalizacoes);

            RascunhoProcessoViewModel rascunhoAtualizado = _rascunho.EditRascunhoProcesso(rascunho.Id);
            return PartialView("RascunhoBasico", rascunhoAtualizado);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Excluir(int id)
        {
            _rascunho.DeleteRascunhoProcesso(id);
            IEnumerable<RascunhoProcessoViewModel> rascunhosPorOrganizacao = _rascunho.GetRascunhosProcessoPorOrganizacao();
            return PartialView("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AutuarProcessoPorIdRascunho(int id)
        {
            var retorno = _processo.AutuarPorIdRascunho(id);

            return Json(retorno);
        }
    }
}
