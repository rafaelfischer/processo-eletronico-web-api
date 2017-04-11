﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
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
        IDespachoWorkService service;

        public DespachosController(IDespachoWorkService service, IHttpContextAccessor httpContextAccessor, IClientAccessToken clientAccessToken) : base(httpContextAccessor, clientAccessToken)
        {
            this.service = service;
            this.service.Usuario = UsuarioAutenticado;
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
            try
            {
                return new ObjectResult(service.PesquisarDespachosUsuario());
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Retorna o despacho correspondente ao identificador.
        /// </summary>
        /// <param name="idDespacho">Identificador do Despacho</param>
        /// <returns>Despacho correspondente ao identificador.</returns>
        /// <response code="200">Retorna o despacho correspondente ao identificador.</response>
        /// <response code="404">Despacho não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{idDespacho}")]
        [ProducesResponseType(typeof(DespachoModeloGet), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarDespacho(int idDespacho)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(idDespacho));
            }
            catch (RecursoNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (RequisicaoInvalidaException e)
            {
                return BadRequest(MensagemErro.ObterMensagem(e));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
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
            try
            {
                HttpRequest request = HttpContext.Request;
                DespachoModeloGet despachoCompleto = service.Despachar(despachoPost);
                return Created("https://" + request.Host.Value + request.Path.Value + "/" + despachoCompleto.Id, despachoCompleto);

            }
            catch (RequisicaoInvalidaException e)
            {
                return BadRequest(MensagemErro.ObterMensagem(e));
            }
            catch (RecursoNaoEncontradoException e)
            {
                return NotFound(MensagemErro.ObterMensagem(e));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }
        #endregion
    }
}
