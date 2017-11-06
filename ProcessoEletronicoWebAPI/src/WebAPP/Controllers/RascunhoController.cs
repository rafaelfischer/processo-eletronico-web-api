using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IRascunhoProcessoService _service;
        private IRascunhoProcessoMunicipioService _municipioService;
        private IRascunhoProcessoSinalizacaoService _sinalizacaoService;
        private IRascunhoProcessoAnexoService _anexoService;
        private IOrganogramaAppService _organogramaService;
        private IRascunhoProcessoInteressadoService _interessadoService;

        public RascunhoController(
            IRascunhoProcessoService service,
            IRascunhoProcessoMunicipioService municipio,
            IRascunhoProcessoSinalizacaoService sinalizacao,
            IRascunhoProcessoAnexoService anexoService,
            IOrganogramaAppService organogramaService,
            IRascunhoProcessoInteressadoService interessadoService)
        {
            _service = service;
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
        public IActionResult EditarBasico(RascunhoProcessoViewModel rascunho)
        {
            _service.UpdateRascunhoProcesso(rascunho.Id, rascunho);

            if (rascunho.Sinalizacoes != null && rascunho.Sinalizacoes.Count > 0)
                _sinalizacaoService.PostSinalizacao(rascunho.Id, rascunho.Sinalizacoes);

            RascunhoProcessoViewModel rascunhoAtualizado = _service.EditRascunhoProcesso(rascunho.Id);
            return PartialView("RascunhoBasico", rascunhoAtualizado);
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
