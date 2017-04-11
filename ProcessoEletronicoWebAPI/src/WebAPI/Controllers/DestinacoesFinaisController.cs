using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
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
        IDestinacaoFinalWorkService service;

        public DestinacoesFinaisController(IDestinacaoFinalWorkService service, IHttpContextAccessor httpContextAccessor, IClientAccessToken clientAccessToken) : base(httpContextAccessor, clientAccessToken)
        {
            this.service = service;
            this.service.Usuario = UsuarioAutenticado;
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
            try
            {
                return new ObjectResult(service.Listar());
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Retorna a destinação final de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da destinação final.</param>
        /// <returns>Destinação final de acordo com o identificador informado.</returns>
        /// <response code="200">Destinação final de acordo com o identificador informado.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DestinacaoFinalModeloGet), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
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
                return BadRequest("Objeto Inválido.");
            }

            try
            {
                HttpRequest request = HttpContext.Request;
                DestinacaoFinalModeloGet destinacaoFinalGet = service.Inserir(destinacaoFinalPost);
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + destinacaoFinalGet.Id, destinacaoFinalGet);

            }
            catch (RecursoNaoEncontradoException e)
            {
                return NotFound(MensagemErro.ObterMensagem(e));
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
            try
            {
                service.Excluir(id);
                return Ok();
            }
            catch (RecursoNaoEncontradoException e)
            {
                return NotFound(MensagemErro.ObterMensagem(e));
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

    }
}
