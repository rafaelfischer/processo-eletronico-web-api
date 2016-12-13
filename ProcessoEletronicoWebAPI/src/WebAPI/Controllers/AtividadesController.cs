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
    [Route("api/atividades")]
    public class AtividadesController : BaseController
    {
        IAtividadeWorkService service;

        public AtividadesController(IAtividadeWorkService service, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna a lista de atividades que pertencem à função especificada.
        /// </summary>
        /// <param name="idFuncao">Identificador da função a qual se deseja obter suas atividades.</param>
        /// <returns>Lista de atividades que pertencem à função especificada.</returns>
        /// <response code="200">Retorna a lista de atividades que pertencem à função especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("funcao/{idFuncao}")]
        [ProducesResponseType(typeof(List<AtividadeModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int idFuncao)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(idFuncao));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }
    }
}
