using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;

namespace WebAPI.Restrito.Controllers
{
    [Route("[controller]")]
    public class RestritoController : Controller
    {
        IAutuacaoWorkService service;

        public RestritoController(IAutuacaoWorkService service)
        {
            this.service = service;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "já era" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("autuar/{numero}")]
        public string Autuar (int numero)
        {
            return service.Autuar(numero);
        }
        
        // POST api/values
        [HttpPost ("autuar")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
