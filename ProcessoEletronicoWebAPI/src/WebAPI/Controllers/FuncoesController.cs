using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("organizacoes-processo/{id}/funcoes")]
    public class FuncoesController : Controller
    {
        IFuncaoWorkService service;

        public FuncoesController(IFuncaoWorkService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get(int id, int idPlanoClassificacao)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id, idPlanoClassificacao));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
