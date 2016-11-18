using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("organizacoes-processo/{id}/sinalizacoes")]
    public class SinalizacaoController : Controller
    {
        ISinalizacaoWorkService service;

        public SinalizacaoController(ISinalizacaoWorkService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<SinalizacaoModelo> Get(int id)
        {
            return service.Pesquisar(id);
        }
    }
}
