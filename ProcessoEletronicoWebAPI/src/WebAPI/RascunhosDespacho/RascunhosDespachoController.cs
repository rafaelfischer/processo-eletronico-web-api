using Apresentacao.WebAPI.Base;
using Apresentacao.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.WebAPI.Base;

using WebAPI.Config;

namespace Prodest.ProcessoEletronico.WebAPI.RascunhosDespacho
{
    [Route("api/rascunhos-despacho")]
    public class RascunhosDespachoController : BaseController
    {
        IRascunhoDespachoService _service;
        IMapper _mapper;

        public RascunhosDespachoController(IRascunhoDespachoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region GET

        /// <summary>
        /// Retorna o rascunho de despacho correspondente ao identificador informado.
        /// </summary>
        /// <param name="id">Identificador do rascunho de despacho.</param>
        /// <returns>despacho correspondente ao identificador.</returns>
        /// <response code="200">Rascunho de despacho correspondente ao identificador.</response>
        /// <response code="404">Rascunho de despacho não foi encontrado.</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetRascunhoDespacho")]
        [ProducesResponseType(typeof(GetRascunhoDespachoDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDespachoGroup)]
        public IActionResult Search(int id)
        {
            return Ok(_mapper.Map<GetRascunhoDespachoDto>(_service.Search(id)));
        }

        #endregion

        #region POST

        /// <summary>
        /// Inserção de Rascunhos de despachos
        /// </summary>
        /// <param name="rascunhoDespachoPost">Informações do rascunho de despacho</param>
        /// <returns>URL do rascunho de despacho inserido no cabeçalho da resposta e o rascunho de despacho recém inserido</returns>
        /// <response code="201">Retorna o rascunho de despacho recém inserido</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Informa o motivo do objeto estar inválido</response>
        /// <response code="500">Retorna a descrição do erro</response>
        [HttpPost]
        [Authorize(Policy = "RascunhosDespacho.Elaborar")]
        [ProducesResponseType(typeof(GetRascunhoDespachoDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDespachoGroup)]
        public IActionResult Add([FromBody]PostRascunhoDespachoDto rascunhoDespachoPost)
        {
            if (rascunhoDespachoPost == null)
            {
                return BadRequest();
            }

            GetRascunhoDespachoDto getRascunhoDespachoDto = _mapper.Map<GetRascunhoDespachoDto>(_service.Add(rascunhoDespachoPost));
            return CreatedAtRoute("GetRascunhodespacho", new { id = getRascunhoDespachoDto.Id }, getRascunhoDespachoDto);
        }
        #endregion

        #region PATCH

        /// <summary>
        /// Alteração de Rascunhos de despachos
        /// </summary>
        /// <param name="id">Identificador do rascunho de despacho<param>
        /// <param name="jsonPatchRascunhoDespacho">Informações a serem alteradas (JSON Patch Document) no rascunho de despacho<param>
        /// <response code="204">Alteração realizada com sucesso</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Informa o motivo do objeto estar inválido</response>
        /// <response code="500">Retorna a descrição do erro</response>
        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhosDespacho.Elaborar")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDespachoGroup)]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<PatchRascunhoDespachoDto> jsonPatchRascunhoDespacho)
        {
            if (jsonPatchRascunhoDespacho == null)
            {
                return BadRequest();
            }

            _service.Patch(id, jsonPatchRascunhoDespacho);
            return NoContent();
        }
        #endregion

    }
}
