using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/rascunhos-processo")]
    public class RascunhosProcessoController : BaseController
    {
        IRascunhoProcessoWorkService service;
        
        public RascunhosProcessoController (IRascunhoProcessoWorkService service, IHttpContextAccessor httpContextAccessor, IClientAccessToken clientAccessToken) : base (httpContextAccessor, clientAccessToken)
        {
            this.service = service;
            this.service.Usuario = UsuarioAutenticado;
        }

        #region GET

        /// <summary>
        /// Retorna o rascunho de processo correspondente ao identificador informado.
        /// </summary>
        /// <param name="id">Identificador do rascunho de processo.</param>
        /// <returns>Processo correspondente ao identificador.</returns>
        /// <response code="200">Rascunho de processo correspondente ao identificador.</response>
        /// <response code="404">Rascunho de processo não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RascunhoProcessoCompletoModelo), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id));
            }
            catch (RecursoNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }


        /// <summary>
        /// Retorna a lista de rascunhos de processo da organização especificada.
        /// </summary>
        /// <param name="guidOrganizacao">Identificador da organização.</param>
        /// <returns>Lista de rascunhos de processo da organização especificada.</returns>
        /// <response code="200">Retorna a lista de rascunhos de processo da organização especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("organizacao/{guidOrganizacao}")]
        [ProducesResponseType(typeof(List<RascunhoProcessoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarPorOganizacao(string guidOrganizacao)
        {
            try
            {
                return new ObjectResult(service.PesquisarRascunhosProcessoNaOrganizacao(guidOrganizacao));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }
        #endregion

        #region POST
        
        /// <summary>
        /// Salvamento de Rascunhos Processos (inserção de processos).
        /// </summary>
        /// <param name="rascunhoProcessoPost">Informações do rascunho de processo.</param>
        /// <returns>URL do rascunho de processo inserido no cabeçalho da resposta e o rascunho de processo recém inserido</returns>
        /// <response code="201">Retorna o rascunho de processo recém inserido.</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(RascunhoProcessoCompletoModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Salvar([FromBody]RascunhoProcessoModeloPost rascunhoProcessoPost)
        {
            try
            {
                HttpRequest request = HttpContext.Request;
                RascunhoProcessoCompletoModelo rascunhoProcessoCompleto = service.Salvar(rascunhoProcessoPost);
                return Created(request.Scheme + "://"+ request.Host.Value + request.Path.Value + "/" + rascunhoProcessoCompleto.Id , rascunhoProcessoCompleto);
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
        #region PUT

        /// <summary>
        /// Altera o rascunhos de processo de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da organização.</param>
        /// <returns></returns>
        /// <response code="200">Rascunho de processo alterado com sucesso.</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="404">Rascunho de processo não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Alterar(int id, [FromBody] AlteraRascunhoProcesso alteraRascunhoProcesso)
        {
            try
            {
                RascunhoProcessoCompletoModelo rascunhoProcessoCompleto =  service.Alterar(id, alteraRascunhoProcesso);
                return Ok(rascunhoProcessoCompleto);
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

        #region DELETE
        /// <summary>
        /// Exclui o rascunho de processo de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do rascunho de processos.</param>
        /// <response code="200">Rascunhos de processo excluído com sucesso.</response>
        /// <response code="404">Rascunho de processo não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
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
        #endregion


    }
}
