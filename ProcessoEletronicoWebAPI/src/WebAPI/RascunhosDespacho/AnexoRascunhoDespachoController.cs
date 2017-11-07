using Apresentacao.WebAPI.Base;
using Apresentacao.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Config;

namespace WebAPI.RascunhosDespacho
{
    [Route("api/rascunhos-despacho/{idRascunhoDespacho}/anexos")]
    public class AnexoRascunhoDespachoController : Controller
    {
        private IMapper _mapper;
        private IAnexoRascunhoDespachoService _service;
        public AnexoRascunhoDespachoController(IMapper mapper, IAnexoRascunhoDespachoService service)
        {
            _mapper = mapper;
            _service = service;
        }

        #region GET

        /// <summary>
        /// Retorna o anexo do rascunho de despacho conforme identificadores.
        /// </summary>
        /// <param name="idRascunhoDespacho">Identificador rascunho de despacho.</param>
        /// <param name="id">Identificador do anexo do rascunho de despacho.</param>
        /// <returns>Anexo correspondente ao identificador.</returns>
        /// <response code="200">Anexo correspondente ao identificador.</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetAnexoRascunhoDespacho")]
        [ProducesResponseType(typeof(GetRascunhoAnexoDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDespachoGroup)]
        public IActionResult Search(int idRascunhoDespacho, int id)
        {
            return Ok(_mapper.Map<GetRascunhoAnexoDto>(_service.Search(idRascunhoDespacho, id)));
        }

        /// <summary>
        /// Retorna os anexos do rascunho de despacho conforme identificador.
        /// </summary>
        /// <param name="idRascunhoDespacho">Identificador rascunho de despacho.</param>
        /// <returns>Anexos do rascunho de despacho</returns>
        /// <response code="200">Anexos do rascunho de despacho</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetRascunhoAnexoDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDespachoGroup)]
        public IActionResult Search(int idRascunhoDespacho)
        {
            return Ok(_mapper.Map<IEnumerable<GetRascunhoAnexoDto>>(_service.Search(idRascunhoDespacho)));
        }
        #endregion

        #region POST

        /// <summary>
        /// Inserção de Anexos em Rascunhos de despachos
        /// </summary>
        /// <param name="idRascunhoDespacho">Identificador do Rascunho de Despacho</param>
        /// <param name="postRascunhoAnexoDto">Informações anexo</param>
        /// <returns>URL do anexo rascunho de despacho inserido no cabeçalho da resposta e o anexo do rascunho de despacho recém inserido</returns>
        /// <response code="201">Retorna o anexo de rascunho de despacho recém inserido</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Informa o motivo do objeto estar inválido</response>
        /// <response code="500">Retorna a descrição do erro</response>
        [HttpPost]
        [Authorize(Policy = "RascunhosDespacho.Elaborar")]
        [ProducesResponseType(typeof(GetRascunhoAnexoDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDespachoGroup)]
        public IActionResult Add(int idRascunhoDespacho, [FromBody]PostRascunhoAnexoDto postRascunhoAnexoDto)
        {
            if (postRascunhoAnexoDto == null)
            {
                return BadRequest();
            }

            GetRascunhoAnexoDto getRascunhoAnexoDto = _mapper.Map<GetRascunhoAnexoDto>(_service.Add(idRascunhoDespacho, postRascunhoAnexoDto));
            return CreatedAtRoute("GetAnexoRascunhoDespacho", new { id = getRascunhoAnexoDto.Id, IdRascunhoDespacho = idRascunhoDespacho }, getRascunhoAnexoDto);
        }
        #endregion

        #region PATCH

        /// <summary>
        /// Alteração de Anexos de Rascunhos de despachos
        /// </summary>
        /// <param name="id">Identificador do anexo do rascunho de despacho<param>
        /// <param name="idRascunhoDespacho">Identificador do rascunho de despacho<param>
        /// <param name="jsonPatchAnexoRascunho">Informações a serem alteradas (JSON Patch Document) no anexo do rascunho de despacho<param>
        /// <response code="204">Operação realizada com sucesso</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Informa o motivo do objeto estar inválido</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhosDespacho.Elaborar")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDespachoGroup)]
        public IActionResult Patch(int idRascunhoDespacho, int id, [FromBody] JsonPatchDocument<PatchRascunhoAnexoDto> jsonPatchAnexoRascunho)
        {
            if (jsonPatchAnexoRascunho == null)
            {
                return BadRequest();
            }

            _service.Patch(idRascunhoDespacho, id, jsonPatchAnexoRascunho);
            return NoContent();
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Exclusão de anexos de rascunho de despacho
        /// </summary>
        /// <param name="idRascunhoDespacho">Identificador rascunho de despacho.</param>
        /// <param name="id">Identificador do anexo do rascunho de despacho.</param>
        /// <response code="204">Operação realizada com sucesso</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDespachoGroup)]
        public IActionResult Delete(int idRascunhoDespacho, int id)
        {
            _service.Delete(idRascunhoDespacho, id);
            return NoContent();
        }
        #endregion
    }
}
