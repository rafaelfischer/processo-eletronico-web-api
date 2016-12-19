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
    [Route("api/anexos")]
    public class AnexosController : BaseController
    {
        IAnexoWorkService service;

        public AnexosController(IAnexoWorkService service, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna o anexo correspondente ao identificador informado.
        /// </summary>
        /// <param name="id">Identificador do anexo.</param>
        /// <returns>Anexo correspondente ao identificador.</returns>
        /// <response code="200">Anexo correspondente ao identificador.</response>
        /// <response code="404">Anexo não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
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
    }
}
