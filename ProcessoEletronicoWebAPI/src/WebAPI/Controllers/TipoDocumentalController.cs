using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/organizacoes-processo/{id}/tipos-documento")]
    public class TipoDocumentalController : Controller
    {
        ITipoDocumentalWorkService service;

        public TipoDocumentalController(ITipoDocumentalWorkService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Listar(int id, [FromQuery] int idAtividade)
        {
            try
            {
                return new ObjectResult(service.Listar(id, idAtividade));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
                
    }
}
