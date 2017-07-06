using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using System.Collections.Generic;
using WebAPI.Config;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo
{
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/anexos")]
    public class AnexosController : BaseController
    {
        IMapper _mapper;
        IAnexoNegocio _negocio;

        public AnexosController(IMapper mapper, IAnexoNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }
        /// <summary>
        /// Lista de anexos do rascunho de processos
        /// </summary>
        /// <remarks>
        /// O contéudo dos anexos não é enviado. O conteúdo é disponibilizado apenas na consulta por identificador.
        /// </remarks>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <returns>Anexos do rascunho de processos</returns>
        /// <response code="200">Retorna a lista de anexos</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        [ProducesResponseType(typeof(List<GetAnexoDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetAnexoDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        /// <summary>
        /// Anexo do rascunho de processos
        /// </summary>
        /// <remarks>
        /// Essa consulta retorna o conteúdo do anexo
        /// </remarks>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do Anexo</param>
        /// <returns>Anexo de acordo com o identificador informado</returns>
        /// <response code="200">Anexo de acordo com o identificador informado</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetAnexo")]
        [ProducesResponseType(typeof(List<GetAnexoDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetAnexoDto>(_negocio.Get(idRascunhoProcesso, id)));
        }

        /// <summary>
        /// Inserção de anexos no rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="postAnexoDto">Informações do anexo a ser inserido</param>
        /// <returns>Anexo recém inserido</returns>
        /// <response code="201">Anexo recém inserido</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(GetAnexoDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Post (int idRascunhoProcesso, [FromBody] PostAnexoDto postAnexoDto)
        {
            if (postAnexoDto == null)
            {
                return BadRequest();
            }

            AnexoModeloNegocio anexoNegocio = _negocio.Post(idRascunhoProcesso, _mapper.Map<AnexoModeloNegocio>(postAnexoDto));
            GetAnexoDto getAnexoDto = _mapper.Map<GetAnexoDto>(anexoNegocio);

            return CreatedAtRoute("GetAnexo", new { Id = getAnexoDto.Id }, getAnexoDto);
        }

        /// <summary>
        /// Alteração de um anexo do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do Anexo</param>
        /// <param name="patchAnexoDto">Informações a serem alteradas (JSON Patch Document)</param>
        /// <response code="204">Operação feita com sucesso</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Patch (int idRascunhoProcesso, int id, [FromBody] JsonPatchDocument<PatchAnexoDto> patchAnexoDto)
        {
            if (patchAnexoDto == null)
            {
                return BadRequest();
            }

            AnexoModeloNegocio anexoNegocio = _negocio.Get(idRascunhoProcesso, id);
            PatchAnexoDto AnexoToPatch = _mapper.Map<PatchAnexoDto>(anexoNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchAnexoDto.ApplyTo(AnexoToPatch, ModelState);

            _mapper.Map(patchAnexoDto, anexoNegocio);
            _negocio.Patch(idRascunhoProcesso, id, anexoNegocio);
            return NoContent();
        }

        /// <summary>
        /// Exclusão de um anexo do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do anexo a ser excluído</param>
        /// <response code="204">Operação feita com sucesso</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Delete(int idRascunhoProcesso, int id)
        {
            _negocio.Delete(idRascunhoProcesso, id);
            return NoContent();
        }
    }
}
