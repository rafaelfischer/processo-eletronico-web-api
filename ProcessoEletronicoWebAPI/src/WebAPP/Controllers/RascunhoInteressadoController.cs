﻿using System;
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
        private IRascunhoProcessoContato _contato;

        public RascunhoInteressadoController(
            IOrganogramaAppService organogramaService,
            IRascunhoProcessoInteressadoService interessadoService,
            IRascunhoProcessoContato contato
            )
        {
            _organogramaService = organogramaService;
            _interessadoService = interessadoService;
            _contato = contato;
        }

        //[HttpPost]
        [Authorize]
        public IActionResult FormInteressado(int idRascunho, int tipoInteressado)
        {
            switch (tipoInteressado)
            {
                case 1:
                    return PartialView("RascunhoInteressadoOrgaos", _organogramaService.GetOrganizacoesPorPatriarca());
                case 2:
                    return PartialView("RascunhoInteressadoPJ", new InteressadoPessoaJuridicaViewModel
                    {
                        Ufs = new UfViewModel().GetUFs(),
                        TiposContato = _contato.GetTiposContato(),
                        IdRascunho = idRascunho
                    });
                case 3:
                    return PartialView("RascunhoInteressadoPF", new InteressadoPessoaFisicaViewModel
                    {
                        Ufs = new UfViewModel().GetUFs(),
                        TiposContato = _contato.GetTiposContato(),
                        IdRascunho = idRascunho
                    });
                default:
                    return Content("Informe um tipo válido de interessado.");
            }
        }

        public IActionResult FormContato(int index = 0)
        {
            ViewBag.Index = index;
            return PartialView("RascunhoInteressadoContato", new FormContatoViewModel { TiposContato = _contato.GetTiposContato() });
        }

        public IActionResult FormEmail(int index = 0)
        {
            ViewBag.Index = index;
            return PartialView("RascunhoInteressadoEmail");
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetUnidadesPorOrganizacao(string guidOrganizacao)
        {
            IEnumerable<UnidadeViewModel> unidades = _organogramaService.GetUniadesPorOrganizacao(guidOrganizacao);
            return PartialView("RascunhoInteressadoOrgaoUnidades", unidades);
        }

        [HttpPost]
        [Authorize]
        public IActionResult IncluirInteressadoPJOrganograma(int idRascunho, string guidOrganizacao, string guidUnidade)
        {
            OrganizacaoViewModel organizacao = _organogramaService.GetOrganizacao(guidOrganizacao);

            InteressadoPessoaJuridicaViewModel interessado = new InteressadoPessoaJuridicaViewModel
            {
                Ufs = new UfViewModel().GetUFs(),
                TiposContato = _contato.GetTiposContato(),
                Cnpj = organizacao.Cnpj,
                RazaoSocial = organizacao.RazaoSocial,
                Sigla = organizacao.Sigla,
                IdRascunho = idRascunho
            };

            if (!string.IsNullOrEmpty(guidUnidade))
            {
                UnidadeViewModel unidade = _organogramaService.GetUnidade(guidUnidade);

                interessado.NomeUnidade = unidade.Nome;
                interessado.SiglaUnidade = unidade.Sigla;
            }

            return PartialView("RascunhoInteressadoPJ", interessado);

            //ListaInteressadosPJPF interessados = new ListaInteressadosPJPF
            //{
            //    InteressadosPF = _interessadoService.GetInteressadosPF(idRascunho),
            //    InteressadosPJ = _interessadoService.GetInteressadosPJ(idRascunho)
            //};

            //return PartialView("RascunhoInteressadosLista", interessados);
        }

        [HttpPost]
        [Authorize]
        public IActionResult IncluirInteressadoPJ(InteressadoPessoaJuridicaViewModel interessado)
        {
            int idRascunho = interessado.IdRascunho;
            interessado.Cnpj = interessado.Cnpj.Replace("/", "").Replace(".", "").Replace("-", "");

            if (interessado.Id > 0)
            {
                _interessadoService.ExcluirInteressadoPJ(idRascunho, interessado.Id);
            }

            _interessadoService.PostInteressadoPJ(idRascunho, interessado);

            ListaInteressadosPJPF interessados = new ListaInteressadosPJPF
            {
                InteressadosPF = _interessadoService.GetInteressadosPF(idRascunho),
                InteressadosPJ = _interessadoService.GetInteressadosPJ(idRascunho)
            };

            return PartialView("RascunhoInteressadosLista", interessados);
        }

        [HttpPost]
        [Authorize]
        public IActionResult IncluirInteressadoPF(InteressadoPessoaFisicaViewModel interessado)
        {
            int idRascunho = interessado.IdRascunho;
            interessado.Cpf = interessado.Cpf.Replace("/", "").Replace(".", "").Replace("-", "");

            if (interessado.Id > 0)
            {
                _interessadoService.ExcluirInteressadoPF(idRascunho, interessado.Id);
            }

            _interessadoService.PostInteressadoPF(idRascunho, interessado);

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
        public IActionResult ExcluirInteressadoPF(int idRascunho, int idInteressadoPF)
        {
            _interessadoService.ExcluirInteressadoPF(idRascunho, idInteressadoPF);

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

        [HttpPost]
        [Authorize]
        public IActionResult FormInteressadoPJPreenchido(int idRascunho, int idInteressadoPJ)
        {
            InteressadoPessoaJuridicaViewModel interessado = _interessadoService.GetInteressadoPJ(idRascunho, idInteressadoPJ);
            interessado.Ufs = new UfViewModel().GetUFs();
            interessado.Municipios = _organogramaService.GetMunicipios(interessado.UfMunicipio);
            interessado.TiposContato = _contato.GetTiposContato();
            interessado.IdRascunho = idRascunho;

            return PartialView("RascunhoInteressadoPJ", interessado);            
        }

        [HttpPost]
        [Authorize]
        public IActionResult FormInteressadoPFPreenchido(int idRascunho, int idInteressadoPF)
        {
            InteressadoPessoaFisicaViewModel interessado = _interessadoService.GetInteressadoPF(idRascunho, idInteressadoPF);
            interessado.Ufs = new UfViewModel().GetUFs();
            interessado.TiposContato = _contato.GetTiposContato();
            interessado.IdRascunho = idRascunho;

            return PartialView("RascunhoInteressadoPF", interessado);
        }
    }
}