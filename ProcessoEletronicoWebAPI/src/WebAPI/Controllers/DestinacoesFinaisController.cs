using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/destinacoes-finais")]
    public class DestinacoesFinaisController : BaseController
    {
        private IDestinacaoFinalNegocio _negocio;
        private IMapper _mapper;

        public DestinacoesFinaisController(IDestinacaoFinalNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna a lista de destinações finais para tipos documentais.
        /// </summary>
        /// <returns>Retorna a lista de destinações finais para tipos documentais.</returns>
        /// <response code="200">Lista de destinações finais para tipos documentais.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<DestinacaoFinalModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Listar()
        {
            return Ok(_mapper.Map<List<DestinacaoFinalModeloGet>>(_negocio.Listar()));
        }

        /// <summary>
        /// Retorna a destinação final de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da destinação final.</param>
        /// <returns>Destinação final de acordo com o identificador informado.</returns>
        /// <response code="200">Destinação final de acordo com o identificador informado.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}", Name = "GetDestinacaoFinal")]
        [ProducesResponseType(typeof(DestinacaoFinalModeloGet), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id)
        {
            return Ok(_mapper.Map<DestinacaoFinalModeloGet>(_negocio.Pesquisar(id)));
        }

        /// <summary>
        /// Insere uma destinação final 
        /// </summary>
        /// <param name="destinacaoFinalPost">Informações da destinação final a ser inserida.</param>
        /// <returns>Função inserida</returns>
        /// <response code="201">Destinação final inserida</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "DestinacaoFinal.Inserir")]
        [ProducesResponseType(typeof(DestinacaoFinalModeloGet), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Inserir([FromBody] DestinacaoFinalModeloPost destinacaoFinalPost)
        {
            if (destinacaoFinalPost == null)
            {
                return BadRequest();
            }

            DestinacaoFinalModeloGet destinacaoFinalGet = _mapper.Map<DestinacaoFinalModeloGet>(_negocio.Inserir(_mapper.Map<DestinacaoFinalModeloNegocio>(destinacaoFinalPost)));
            return CreatedAtRoute("GetDestinacaoFinal", new { id = destinacaoFinalGet.Id }, destinacaoFinalGet);
        }

        /// <summary>
        /// Exclui a destinação final de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da destinação final.</param>
        /// <response code="200">Destinação final excluída com sucesso.</response>
        /// <response code="404">Destinação final não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "DestinacaoFinal.Excluir")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Excluir(int id)
        {
            _negocio.Excluir(id);
            return NoContent();
        }
    }
}
