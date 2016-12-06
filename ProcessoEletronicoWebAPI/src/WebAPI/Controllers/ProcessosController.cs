using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/organizacoes-processo/{id}/processos")]
    public class ProcessosController : Controller
    {
        IProcessoWorkService service;
        
        public ProcessosController (IProcessoWorkService service)
        {
            this.service = service;
        }

        #region GET
        //GET api/processos
        [HttpGet]
        public IActionResult Listar()
        {
            return new ObjectResult("Listar");
        }

        /// <summary>
        /// Retorna o processo correspondente ao idProcesso informado.
        /// </summary>
        /// <param name="id">Identificador da organização patriarca a qual pertencem os processos.</param>
        /// <param name="idProcesso">Identificador do processo.</param>
        /// <returns>Processo correspondente ao idProcesso.</returns>
        /// <response code="201">Retorna o processo correspondente ao idProcesso.</response>
        /// <response code="404">Proceso não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{idProcesso}")]
        [ProducesResponseType(typeof(ProcessoCompletoModelo), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id, int idProcesso)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id, idProcesso));
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
        /// Retorna o processo correspondente ao número informado.
        /// </summary>
        /// <param name="numero">Número do processo.Formato: SEQUENCIAL-DD.AAAA.P.E.OOOO</param>
        /// <returns>Processo correspondente ao número.</returns>
        /// <response code="201">Retorna o processo correspondente ao número.</response>
        /// <response code="404">Proceso não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("/api/processos/numero/{numero}")]
        [ProducesResponseType(typeof(ProcessoCompletoModelo), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(string numero)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(numero));
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

        /// <summary>
        /// Retorna a lista de processos que estão tramintando na unidade especificada.
        /// </summary>
        /// <param name="id">Identificador da organização patriarca a qual pertencem os processos.</param>
        /// <param name="idUnidade">Identificador da unidade onde os processos estão tramintando.</param>
        /// <returns>Lista de processos que estão tramintando na unidade especificada.</returns>
        /// <response code="201">Retorna a lista de processos que estão tramintando na unidade especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("unidade/{idUnidade}")]
        [ProducesResponseType(typeof(List<ProcessoModelo>), 201)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarPorUnidade(int id, int idUnidade)
        {
            try
            {
                return new ObjectResult(service.PesquisarProcessosNaUnidade(id, idUnidade));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion

        #region POST

        // POST api/processos

        /// <summary>
        /// Autuação de Processos
        /// </summary>
        /// <remarks>Apesar das listas de interessados estarem sinalizadas como opcionais, o Processo deve possuir ao menos um interessado (seja ele pessoa física ou jurídica)</remarks>
        /// <param name="processoPost"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "Processo.Autuar")]
        public IActionResult Inserir([FromBody]ProcessoModeloPost processoPost, int id)
        {
            try
            {
                HttpRequest request = HttpContext.Request;
                ProcessoCompletoModelo processoCompleto = service.Autuar(processoPost, id);
                return Created("http://"+ request.Host.Value + request.Path.Value + "/" + processoCompleto.Id , processoCompleto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }

            //return Created("URL Processo","Objeto JSON");
            
        }
        [HttpPost("{idProcesso}/despacho")]
        public IActionResult Despachar(int id, [FromBody]string value)
        {
            return new ObjectResult("Despachar Processo " + id.ToString());
        }

        #endregion

        #region PATCH

        // PATCH api/v1/processos/{id}
        [HttpPatch("{idProcesso}")]
        //[Authorize]
        public IActionResult Alterar(int id, [FromBody]string value)
        {
            return new ObjectResult("Alterar Processo");
        }

        #endregion

        #region DELETE

        // DELETE api/v1/processos/{id}
        [HttpDelete("{idProcesso}")]
        //[Authorize]
        public IActionResult Excluir(int id)
        {
            return new ObjectResult("Excluir Processo");
        }

        #endregion
    }
}
