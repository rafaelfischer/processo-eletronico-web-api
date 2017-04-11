using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/tipos-contato")]
    public class TiposContatoController : BaseController
    {
        ITipoContatoWorkService service;

        public TiposContatoController(ITipoContatoWorkService service, IHttpContextAccessor httpContextAccessor, IClientAccessToken clientAccessToken) : base(httpContextAccessor, clientAccessToken)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna a lista de tipos de contatos.
        /// </summary>
        /// <returns>Lista de tipos de contatos.</returns>
        /// <response code="200">Lista de tipos de contatos.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TipoContatoModelo>), 200)]
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
    }
}
