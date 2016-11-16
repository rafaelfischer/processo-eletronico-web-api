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
    [Route("organizacoes-processo/{id}/planos-classificacao")]
    public class PlanosClassificacaoController : Controller
    {
        IPlanoClassificacaoWorkService service;

        public PlanosClassificacaoController(IPlanoClassificacaoWorkService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get(int id, int idOrganizacao)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id, idOrganizacao));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
