using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/despachos")]
    public class DespachosController : BaseController
    {
        private IDespachoNegocio _negocio;
        private IMapper _mapper;

        public DespachosController(IDespachoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        #region GET
        /// <summary>
        /// Retorna lista de despachos realizados pelo usuario autenticado.
        /// </summary>
        /// <returns>Lista de despacho feitos pelo usuário autenticado.</returns>
        /// <response code="200">Retorna a lista de despachos realizados pelo usuário</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("usuario")]
        [ProducesResponseType(typeof(List<DespachoModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarDespachosUsuario()
        {
            return Ok(_mapper.Map<List<DespachoModeloGet>>(_negocio.PesquisarDespachosUsuario()));
        }

        /// <summary>
        /// Retorna o despacho correspondente ao identificador.
        /// </summary>
        /// <param name="id">Identificador do Despacho</param>
        /// <returns>Despacho correspondente ao identificador.</returns>
        /// <response code="200">Retorna o despacho correspondente ao identificador.</response>
        /// <response code="404">Despacho não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}", Name = "GetDespacho")]
        [ProducesResponseType(typeof(DespachoModeloGet), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarDespacho(int id)
        {
            return Ok(_mapper.Map<DespachoModeloGet>(_negocio.Pesquisar(id)));
        }

        #endregion

        #region POST

        /// <summary>
        /// Inserir despacho de processos.
        /// </summary>
        /// <param name="despachoPost">Informações do despacho do processo.</param>
        /// <returns>URL do despacho no cabeçalho da resposta e o despacho inserido.</returns>
        /// <response code="201">Retorna o despacho inserido.</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "Despacho.Inserir")]
        [ProducesResponseType(typeof(DespachoModeloGet), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Despachar([FromBody]DespachoModeloPost despachoPost)
        {
            DespachoModeloGet despachoCompleto = _mapper.Map<DespachoModeloGet>(_negocio.Despachar(_mapper.Map<DespachoModeloNegocio>(despachoPost)));
            return CreatedAtRoute("GetDespacho", new { id = despachoCompleto.Id }, despachoCompleto);
        }
        #endregion
    }
}
