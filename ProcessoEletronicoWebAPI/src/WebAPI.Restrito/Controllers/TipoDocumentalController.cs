using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Restrito.Base;
using ProcessoEletronicoService.Apresentacao.Restrito.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Restrito.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class TipoDocumentalController : Controller
    {
        ITipoDocumentalWorkService service;

        public TipoDocumentalController(ITipoDocumentalWorkService service)
        {
            this.service = service;
        }

        // GET tipodocumental
        [HttpGet]
        public IEnumerable<TipoDocumentalModelo> Get()
        {
            return service.ObterTiposDocumentais();
        }

        // GET tipodocumental/5
        [HttpGet("{id}")]
        public TipoDocumentalModelo Get(int id)
        {
            return service.ObterTiposDocumentais(id);
        }

        // POST tipodocumental
        [HttpPost]
        public TipoDocumentalModelo Post([FromBody]TipoDocumentalModelo tipoDocumental)
        {
            return service.Incluir(tipoDocumental);
        }

        // PUT tipodocumental/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TipoDocumentalModelo tipoDocumental)
        {
            service.Alterar(id, tipoDocumental);
        }

        // DELETE tipodocumental/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Excluir(id);
        }
    }
}
