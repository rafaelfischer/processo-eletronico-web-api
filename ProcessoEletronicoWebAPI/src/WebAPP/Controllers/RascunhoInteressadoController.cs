using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Apresentacao.APP.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Apresentacao.APP.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class RascunhoInteressadoController : BaseController
    {   
        private IOrganogramaAppService _organogramaService;
        private IRascunhoProcessoInteressadoService _interessadoService;

        public RascunhoInteressadoController(     
            IOrganogramaAppService organogramaService,
            IRascunhoProcessoInteressadoService interessadoService)
        {   
            _organogramaService = organogramaService;
            _interessadoService = interessadoService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult FormInteressado(int tipoInteressado)
        {
            switch (tipoInteressado)
            {
                case 1:
                    return PartialView("RascunhoInteressadoOrgaos");
                case 2:
                    return PartialView("RascunhoInteressadoPJ");
                case 3:
                    return PartialView("RascunhoInteressadoPF");
                default:
                    return Content("Informe um tipo válido de interessado.");
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult IncluirInteressadoPJ(int idRascunho, string guidOrganizacao, string guidUnidade)
        {
            if (string.IsNullOrEmpty(guidUnidade))
            {
                OrganizacaoViewModel organizacao = _organogramaService.GetOrganizacao(guidOrganizacao);
                _interessadoService.PostInteressadoPJ(idRascunho, organizacao);
            }
            else
            {
                OrganizacaoViewModel organizacao = _organogramaService.GetOrganizacao(guidOrganizacao);
                UnidadeViewModel unidade = _organogramaService.GetUnidade(guidUnidade);

                _interessadoService.PostInteressadoPJ(idRascunho, organizacao, unidade);
            }

            ListaInteressadosPJPF interessados = new ListaInteressadosPJPF
            {
                InteressadosPF = _interessadoService.GetInteressadosPF(idRascunho),
                InteressadosPJ = _interessadoService.GetInteressadosPJ(idRascunho)
            };

            return PartialView("RascunhoInteressadosLista", interessados);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ExcluirInteressadoPJ(int idRascunho, int idInteressadoPJ)
        {
            _interessadoService.ExcluirInteressadoPJ(idRascunho, idInteressadoPJ);

            ListaInteressadosPJPF interessados = new ListaInteressadosPJPF
            {
                InteressadosPF = _interessadoService.GetInteressadosPF(idRascunho),
                InteressadosPJ = _interessadoService.GetInteressadosPJ(idRascunho)
            };

            return PartialView("RascunhoInteressadosLista", interessados);
        }

        [HttpPost]
        [Authorize]
        public IActionResult FormInteressadoPF()
        {
            return PartialView("RascunhoInteressadoPF");
        }
    }
}
