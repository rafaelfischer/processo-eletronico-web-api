using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class SinalizacaoController : Controller
    {
        ISinalizacaoWorkService service;

        public SinalizacaoController(ISinalizacaoWorkService service)
        {
            this.service = service;
        }

        // GET sinalizacao
        [HttpGet]
        public IEnumerable<SinalizacaoModelo> Get()
        {
            return service.Obter();
        }

        // GET sinalizacao/5
        [HttpGet("{id}")]
        public SinalizacaoModelo Get(int id)
        {
            return service.Obter(id);
        }

        // POST sinalizacao
        [HttpPost]
        public SinalizacaoModelo Post([FromBody]SinalizacaoModelo sinalizacao)
        {
            return service.Incluir(sinalizacao);
        }

        // PUT sinalizacao/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]SinalizacaoModelo sinalizacao)
        {
            service.Alterar(id, sinalizacao);
        }

        // DELETE sinalizacao/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Excluir(id);
        }
    }
}
