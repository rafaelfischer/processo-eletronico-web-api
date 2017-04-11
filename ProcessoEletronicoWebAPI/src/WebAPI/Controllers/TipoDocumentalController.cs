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
    [Route("api/tipos-documento")]
    public class TipoDocumentalController : BaseController
    {
        ITipoDocumentalWorkService service;

        public TipoDocumentalController(ITipoDocumentalWorkService service, IHttpContextAccessor httpContextAccessor, IClientAccessToken clientAccessToken) : base(httpContextAccessor, clientAccessToken)
        {
            this.service = service;
            this.service.Usuario = UsuarioAutenticado;
        }

        /// <summary>
        /// Retorna o tipo documental de acordo com identificador informado.
        /// </summary>
        /// <param name="id">Identificador do tipo documental.</param>
        /// <returns>Tipo documental de acordo com o identificador informado.</returns>
        /// <response code="200">Tipo documental de acordo com o identificador informado.</response>
        /// <response code="404">Recurso não encotrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TipoDocumentalModeloGet), 200)]
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
        /// Retorna a lista de tipos documentais que pertencem à atividade especificada.
        /// </summary>
        /// <param name="idAtividade">Identificador da atividade a qual se deseja obter seus tipos documentais.</param>
        /// <returns>Lista de tipos documentais que pertencem à atividade especificada.</returns>
        /// <response code="200">Retorna a lista de tipos documentais que pertencem à atividade especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("atividade/{idAtividade}")]
        [ProducesResponseType(typeof(List<TipoDocumentalModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Listar(int idAtividade)
        {
            try
            {
                return new ObjectResult(service.PesquisarPorAtividade(idAtividade));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Insere um tipo documental.
        /// </summary>
        /// <param name="tipoDocumentalPost">Informações do tipo documental a ser inserido.</param>
        /// <returns>Tipo documental recém inserido.</returns>
        /// <response code="201">Tipo documental recém inserido.</response>
        /// <response code="400">Motivo da requisição estar inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "TipoDocumental.Inserir")]
        [ProducesResponseType(typeof(TipoDocumentalModeloGet), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Inserir([FromBody] TipoDocumentalModeloPost tipoDocumentalPost)
        {

            if (tipoDocumentalPost == null)
            {
                throw new RequisicaoInvalidaException("Objeto inválido.");
            }

            try
            {
                HttpRequest request = HttpContext.Request;
                TipoDocumentalModeloGet tipoDocumentalGet = service.Inserir(tipoDocumentalPost);
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + tipoDocumentalGet.Id, tipoDocumentalGet);

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
        /// Exclui o tipo documental de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do tipo documental.</param>
        /// <response code="200">Tipo Documental excluído com sucesso.</response>
        /// <response code="404">Tipo Documental não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "TipoDocumental.Excluir")]
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
