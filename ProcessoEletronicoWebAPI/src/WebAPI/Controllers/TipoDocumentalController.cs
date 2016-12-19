using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
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

        public TipoDocumentalController(ITipoDocumentalWorkService service, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna a lista de tipos documentais que pertencem à atividade especificada.
        /// </summary>
        /// <param name="idAtividade">Identificador da atividade a qual se deseja obter seus tipos documentais.</param>
        /// <returns>Lista de tipos documentais que pertencem à atividade especificada.</returns>
        /// <response code="200">Retorna a lista de tipos documentais que pertencem à atividade especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("atividade/{idAtividade}")]
        [ProducesResponseType(typeof(List<TipoDocumentalModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Listar(int idAtividade)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(idAtividade));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }
    }
}
