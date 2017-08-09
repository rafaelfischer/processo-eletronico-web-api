using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/tipos-contato")]
    public class TiposContatoController : BaseController
    {
        private ITipoContatoNegocio _negocio;
        private IMapper _mapper;

        public TiposContatoController(ITipoContatoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna a lista de tipos de contatos.
        /// </summary>
        /// <returns>Lista de tipos de contatos.</returns>
        /// <response code="200">Lista de tipos de contatos.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TipoContatoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Listar()
        {
            return Ok(_mapper.Map<List<TipoContatoModelo>>(_negocio.Listar()));
        }
    }
}
