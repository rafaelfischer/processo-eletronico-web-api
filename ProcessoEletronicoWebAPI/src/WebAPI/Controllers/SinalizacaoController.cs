using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/sinalizacoes")]
    public class SinalizacaoController : BaseController
    {
        private ISinalizacaoNegocio _negocio;
        private IMapper _mapper;

        public SinalizacaoController(ISinalizacaoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna a lista de sinalizações da organização patriarca especificada.
        /// </summary>
        /// <param name="guidOrganizacaoPatriarca">Identificador da organização patriarca a qual se deseja obter suas sinalizações.</param>
        /// <returns>Lista de sinalizações da organização patriarca especificada.</returns>
        /// <response code="200">Retorna a lista de sinalizações da organização patriarca especificada.</response>
        /// <response code="400">Retorna o motivo da requisição inválida.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("organizacao-patriarca/{guidOrganizacaoPatriarca}")]
        [ProducesResponseType(typeof(List<SinalizacaoModelo>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(string guidOrganizacaoPatriarca)
        {
            return Ok(_mapper.Map<List<SinalizacaoModelo>>(_negocio.Pesquisar(guidOrganizacaoPatriarca)));
        }
    }
}
