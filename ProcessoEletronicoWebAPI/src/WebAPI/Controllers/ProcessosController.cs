using Microsoft.AspNetCore.Authorization;
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
    [Route("api/organizacoes-processo/{id}/processos")]
    public class ProcessosController : BaseController
    {
        IProcessoWorkService service;
        
        public ProcessosController (IProcessoWorkService service, IHttpContextAccessor httpContextAccessor, IOptions<OrganogramaApi> organogramaApiSettings) : base (httpContextAccessor, organogramaApiSettings)
        {
            this.service = service;
            this.service.Usuario = UsuarioAutenticado;
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
        /// Retorna lista de processos que posuem pelo menos um despacho feito pelo usuario
        /// </summary>
        /// <param name="id">Identificador da organização patriarca</param>
        /// <param name="cpfUsuario">CPF do usuário</param>
        /// <returns></returns>
        [HttpGet("usuario/{cpfUsuario}")]
        [ProducesResponseType(typeof(List<ProcessoModelo>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarProcessosDespachadosUsuario(int id, string cpfUsuario)
        {
            try
            {
                return new ObjectResult(service.PesquisarProcessosDespachadosUsuario(id, cpfUsuario));
            }
            catch (RequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Retorna lista de despachos feitos pelo usuario
        /// </summary>
        /// <param name="id">Identificador da organização patriarca</param>
        /// <param name="cpfUsuario">CPF do usuário</param>
        /// <returns></returns>
        [HttpGet("despachos/usuario/{cpfUsuario}")]
        [ProducesResponseType(typeof(List<ProcessoModelo>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarDespachosUsuario(int id, string cpfUsuario)
        {
            try
            {
                return new ObjectResult(service.PesquisarDespachosUsuario(id, cpfUsuario));
            }
            catch (RequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
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
        /// <param name="idProcesso">Identificador do Processo</param>
        /// <param name="id">Identificador da Organização do Processo</param>
        /// <returns>Despacho correspondente ao identificador.</returns>
        /// <response code="201">Retorna o despacho correspondente ao identificador.</response>
        /// <response code="404">Despacho não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{idProcesso}/despachos/{idDespacho}")]
        [ProducesResponseType(typeof(DespachoProcessoModeloGet), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarDespacho(int idDespacho, int idProcesso, int id)
        {
            try
            {
                return new ObjectResult(service.PesquisarDespacho(idDespacho, idProcesso, id));
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

        /// </summary>
        /// <param name="id">Identificador da organização patriarca</param>
        /// <param name="idProcesso">Identificador do Processo</param>
        /// <param name="idDespacho">Identificador do Despacho</param>
        /// <param name="idAnexo">Identificador do Anexo</param>
        /// <returns></returns>
        [HttpGet("{idProcesso}/anexos/{idAnexo}")]
        public IActionResult PesquisarAnexo(int id, int idProcesso, [FromQuery] int idDespacho,  int idAnexo)
        {
            try
            {
                return new ObjectResult(service.PesquisarAnexo(id, idProcesso, idDespacho , idAnexo));
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


        /// <summary>
        /// Retorna a lista de processos que estão tramintando na organização especificada.
        /// </summary>
        /// <param name="guidOrganizacao">Identificador da organização onde os processos estão tramintando.</param>
        /// <returns>Lista de processos que estão tramintando na organização especificada.</returns>
        /// <response code="201">Retorna a lista de processos que estão tramintando na organização especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("/api/processos/organizacao/{guidOrganizacao}")]
        [ProducesResponseType(typeof(List<ProcessoModelo>), 201)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarPorOganizacao(string guidOrganizacao)
        {
            try
            {
                return new ObjectResult(service.PesquisarProcessosNaOrganizacao(guidOrganizacao));
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
        /// <remarks>
        /// Apesar das listas de interessados estarem sinalizadas como opcionais, o Processo deve possuir ao menos um interessado (seja ele pessoa física ou jurídica).
        /// O campo "conteudo" dos anexos do processo é uma string. O arquivo deve ser codificado para uma string base64 antes de ser enviado para a API.
        /// </remarks>
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
            catch (RequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
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
        /// Despacho de processos
        /// </summary>
        /// <param name="id">Identificador da organização patriarca</param>
        /// <param name="idProcesso">Identificador do processo</param>
        /// <param name="despachoPost">Informações do despacho do processo</param>
        /// <returns></returns>
        [HttpPost("{idProcesso}/despachos")]
        [ProducesResponseType(typeof(DespachoProcessoModeloGet), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        //[Authorize]
        public IActionResult Despachar(int id, int idProcesso, [FromBody]DespachoProcessoModeloPost despachoPost)
        {
            try
            {
                HttpRequest request = HttpContext.Request;
                DespachoProcessoModeloGet despachoCompleto = service.Despachar(id, idProcesso, despachoPost);
                return Created("http://" + request.Host.Value + request.Path.Value + "/" + despachoCompleto.Id, despachoCompleto);

            }
            catch (RequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
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

        #endregion
                
    }
}
