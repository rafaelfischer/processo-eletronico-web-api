using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProcessoEletronicoService.Apresentacao.Publico.Base;
using ProcessoEletronicoService.Apresentacao.Publico.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;

namespace WebAPI.Publico.Controllers
{
    [Route("publico/[controller]")]
    public class ConsultaController : Controller
    {
        IConsultaProcessoWorkService service;

        public ConsultaController(IConsultaProcessoWorkService service)
        {
            this.service = service;
        }

        // GET api/values/5
        [HttpGet("{numero}")]
        public IActionResult ConsultarProcessoPorNumero(string numero)
        {

            try
            {
                ProcessoApresentacao processo = service.ConsultarPorNumero(numero);
                return new ObjectResult(processo);
            }
            catch (ProcessoNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (NumeroProcessoInvalidoException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        // POST api/values
        [HttpPost]
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
