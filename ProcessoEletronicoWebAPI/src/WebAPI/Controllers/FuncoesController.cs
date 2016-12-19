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
    [Route("api/funcoes")]
    public class FuncoesController : BaseController
    {
        IFuncaoWorkService service;

        public FuncoesController(IFuncaoWorkService service, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.service = service;
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
    }
}
