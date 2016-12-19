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

        public PlanosClassificacaoController(IPlanoClassificacaoWorkService service, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.service = service;
            this.service.Usuario = UsuarioAutenticado;
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
    }
}
