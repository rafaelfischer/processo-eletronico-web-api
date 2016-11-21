using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/processos")]
    public class ProcessosController : Controller
    {
        IProcessoWorkService service;
        
        public ProcessosController (IProcessoWorkService service)
        {
            this.service = service;
        }

        #region GET
        //GET api/processos
        [HttpGet]
        public IActionResult Listar()
        {
            return new ObjectResult("Listar");
        }
        

        // GET api/v1/processos/{id}
        [HttpGet("{id}")]
        public IActionResult Pesquisar(int id)
        {
            return new ObjectResult("Pesquisar por ID");
            
        }

        // GET api/v1/processos/numero/{numeroProcesso}
        [HttpGet("/numero/{numeroProcesso}")]
        public IActionResult Pesquisar(string numeroProcesso)
        {
            return new ObjectResult("Pesquisar por número");
        }

        #endregion

        #region POST

        // POST api/processos

        /// <summary>
        /// Autuação de Processos
        /// </summary>
        /// <remarks>O Processo deve possuir ao menos um interessado (seja ele pessoa física ou jurídica)</remarks>
        /// <param name="processoPost"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        public IActionResult Autuar([FromBody]ProcessoModeloPost processoPost)
        {
            return new ObjectResult("Autuar Processo");
        }
        [HttpPost("{id}/despacho")]
        public IActionResult Despachar(int id, [FromBody]string value)
        {
            return new ObjectResult("Despachar Processo " + id.ToString());
        }

        #endregion

        #region PATCH

        // PATCH api/v1/processos/{id}
        [HttpPatch("{id}")]
        //[Authorize]
        public IActionResult Alterar(int id, [FromBody]string value)
        {
            return new ObjectResult("Alterar Processo");
        }

        #endregion

        #region DELETE

        // DELETE api/v1/processos/{id}
        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult Excluir(int id)
        {
            return new ObjectResult("Excluir Processo");
        }

        #endregion
    }
}
