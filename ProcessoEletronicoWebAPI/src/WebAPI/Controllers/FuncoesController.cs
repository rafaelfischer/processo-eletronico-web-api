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
    [Route("api/funcoes")]
    public class FuncoesController : BaseController
    {
        IFuncaoWorkService service;

        public FuncoesController(IFuncaoWorkService service, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.service = service;
            this.service.Usuario = UsuarioAutenticado;
        }

        /// <summary>
        /// Retorna a lista de funções que pertencem ao plano de classificação especificado.
        /// </summary>
        /// <param name="idPlanoClassificacao">Identificador do plano de classificação do qual se deseja obter suas atividades.</param>
        /// <returns>Lista de funções que pertencem ao plano de classificação especificado.</returns>
        /// <response code="200">Retorna a lista de funções que pertencem ao plano de classificação especificado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("plano-classificacao/{idPlanoClassificacao}")]
        [ProducesResponseType(typeof(List<FuncaoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int idPlanoClassificacao)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(idPlanoClassificacao));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Insere uma função 
        /// </summary>
        /// <param name="funcao">Informações da função a ser inserida.</param>
        /// <returns>Função inserida</returns>
        /// <response code="201">Função inserida</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "Funcao.Inserir")]
        [ProducesResponseType(typeof(FuncaoProcessoGetModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Inserir([FromBody] FuncaoModeloPost funcao)
        {

            if (funcao == null)
            {
                return BadRequest("Objeto Inválido.");
            }

            try
            {
                HttpRequest request = HttpContext.Request;
                FuncaoProcessoGetModelo funcaoGet = service.Inserir(funcao);
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + funcaoGet.Id, funcaoGet);

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
        /// Exclui a função de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da função.</param>
        /// <response code="200">Função excluída com sucesso.</response>
        /// <response code="404">Função não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Funcao.Excluir")]
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
