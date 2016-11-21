using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProcessoEletronicoService.Apresentacao.Base;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/organizacoes-processo/{id}/processos")]
    public class ProcessosController : Controller
    {
        IProcessoWorkService service;

        public ProcessosController (IProcessoWorkService service)
        {
            this.service = service;
        }

        #region GET
        //GET api/v1/processos
        [HttpGet]
        public IActionResult Listar()
        {
            return new ObjectResult("Listar");
        }
        

        // GET api/v1/processos/{id}
        [HttpGet("{idProcesso}")]
        public IActionResult Pesquisar(int idProcesso)
        {
            return new ObjectResult("Pesquisar por ID");
            
        }

        // GET api/v1/processos/numero/{numeroProcesso}
        [HttpGet("/numero/{numeroProcesso}")]
        public IActionResult Pesquisar(string numeroProcesso)
        {
            return new ObjectResult("Pesquisar por número");
        }

        /// <summary>
        /// Retorna a lista de processos que estão tramintando na unidade especificada.
        /// </summary>
        /// <param name="id">Identificador da organização patriarca a qual pertencem os processos.</param>
        /// <param name="idUnidade">Identificador da unidade onde os processos estão tramintando.</param>
        /// <returns>Lista de processos que estão tramintando na unidade especificada.</returns>
        /// <response code="201">Retorna a lista de processos que estão tramintando na unidade especificada.</response>
        /// <response code="500">Se acontecer algum erro inesperado.</response>
        [HttpGet("unidade/{idUnidade}")]
        [ProducesResponseType(typeof(List<string>), 201)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id, int idUnidade)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id, idUnidade));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion

        #region POST

        // POST api/v1/processos
        [HttpPost]
        //[Authorize]
        public IActionResult Autuar([FromBody]string value)
        {
            return new ObjectResult("Autuar Processo");
        }
        [HttpPost("{idProcesso}/despacho")]
        public IActionResult Despachar(int id, [FromBody]string value)
        {
            return new ObjectResult("Despachar Processo " + id.ToString());
        }

        #endregion

        #region PATCH

        // PATCH api/v1/processos/{id}
        [HttpPatch("{idProcesso}")]
        //[Authorize]
        public IActionResult Alterar(int id, [FromBody]string value)
        {
            return new ObjectResult("Alterar Processo");
        }

        #endregion

        #region DELETE

        // DELETE api/v1/processos/{id}
        [HttpDelete("{idProcesso}")]
        //[Authorize]
        public IActionResult Excluir(int id)
        {
            return new ObjectResult("Excluir Processo");
        }

        #endregion
    }
}
