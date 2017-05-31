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
    [Route("api/planos-classificacao")]
    public class PlanosClassificacaoController : BaseController
    {
        IPlanoClassificacaoWorkService service;

        public PlanosClassificacaoController(IPlanoClassificacaoWorkService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna a lista de planos de classificação que podem ser utilizados pela organização especificada.
        /// </summary>
        /// <param name="guidOrganizacao">Identificador da organização a qual se deseja obter seus planos de classificação.</param>
        /// <returns>Lista de planos de classificação que podem ser utilizados pela organização especificada.</returns>
        /// <response code="200">Retorna a lista de planos de classificação que podem ser utilizados pela organização especificada.</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("organizacao/{guidOrganizacao}")]
        [ProducesResponseType(typeof(List<PlanoClassificacaoModelo>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(string guidOrganizacao)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(guidOrganizacao));
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
        /// Retorna o plano de classificação de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do Plano de Classificação .</param>
        /// <returns>Plano de classificação de acordo com o identificador informado.</returns>
        /// <response code="200">Plano de classificação de acordo com o identificador informado.</response>
        /// <response code="404">Plano de classificação não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlanoClassificacaoProcessoGetModelo), 200)]
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
                return NotFound(MensagemErro.ObterMensagem(e));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Retorna a lista de planos de classificação que podem ser mantidos pelo usuário..
        /// </summary>
        /// <returns>Lista de planos de classificação que podem ser mantidos pelo usuário.</returns>
        /// <response code="200">Retorna a lista de planos de classificação que podem ser mantidos pelo usuário.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PlanoClassificacaoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get()
        {
            try
            {
                return new ObjectResult(service.Pesquisar());
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Insere um plano de classificação de acordo com a organização do usuário.
        /// </summary>
        /// <param name="planoClassificacao">Informações do Plano de Classificacão.</param>
        /// <returns>Lista de planos de classificação que podem ser utilizados pela organização especificada.</returns>
        /// <response code="201">Plano de classificação inserido</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "PlanoClassificacao.Inserir")]
        [ProducesResponseType(typeof(PlanoClassificacaoModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Inserir([FromBody]PlanoClassificacaoModeloPost planoClassificacao)
        {

            if (planoClassificacao == null)
            {
                return BadRequest("Objeto Inválido");
            }

            try
            {
                HttpRequest request = HttpContext.Request;
                PlanoClassificacaoProcessoGetModelo planoClassificacaoGet = service.Inserir(planoClassificacao);
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + planoClassificacaoGet.Id, planoClassificacaoGet);

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
        /// Exclui o plano de classificação de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do Plano de Classificação .</param>
        /// <response code="200">Plano de classificação excluído com sucesso.</response>
        /// <response code="404">Plano de classificação não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "PlanoClassificacao.Excluir")]
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
