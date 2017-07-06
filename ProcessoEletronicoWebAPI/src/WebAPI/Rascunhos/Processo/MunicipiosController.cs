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
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/municipios")]
    public class MunicipiosController : BaseController
    {
        IMapper _mapper;
        IMunicipioNegocio _negocio;

        public MunicipiosController(IMapper mapper, IMunicipioNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        /// <summary>
        /// Lista de municípios do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <returns>Municípios do rascunho de processos</returns>
        /// <response code="200">Retorna a lista de municípios</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        [ProducesResponseType(typeof(List<GetMunicipioDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetMunicipioDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        /// <summary>
        /// Município do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do Município</param>
        /// <returns>Município de acordo com o identificador informado</returns>
        /// <response code="200">Município de acordo com o identificador informado</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetMunicipio")]
        [ProducesResponseType(typeof(List<GetMunicipioDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetMunicipioDto>(_negocio.Get(idRascunhoProcesso, id)));
        }

        /// <summary>
        /// Inserção de municípios no rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="postMunicipioDto">Informações do município a ser inserido</param>
        /// <returns>Município recém inserido</returns>
        /// <response code="201">Município recém inserido</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(GetMunicipioDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Post (int idRascunhoProcesso, [FromBody] PostMunicipioDto postMunicipioDto)
        {
            if (postMunicipioDto == null)
            {
                return BadRequest();
            }

            MunicipioProcessoModeloNegocio municipioNegocio = _negocio.Post(idRascunhoProcesso, _mapper.Map<MunicipioProcessoModeloNegocio>(postMunicipioDto));
            GetMunicipioDto getMunicipioDto = _mapper.Map<GetMunicipioDto>(municipioNegocio);

            return CreatedAtRoute("GetMunicipio", new { Id = getMunicipioDto.Id }, getMunicipioDto);
        }

        /// <summary>
        /// Alteração de um município do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do Anexo</param>
        /// <param name="patchMunicipioDto">Informações a serem alteradas (JSON Patch Document)</param>
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
        public IActionResult Patch (int idRascunhoProcesso, int id, [FromBody] JsonPatchDocument<PatchMunicipioDto> patchMunicipioDto)
        {
            if (patchMunicipioDto == null)
            {
                return BadRequest();
            }

            MunicipioProcessoModeloNegocio municipioNegocio = _negocio.Get(idRascunhoProcesso, id);
            PatchMunicipioDto patchMunicipio = _mapper.Map<PatchMunicipioDto>(municipioNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchMunicipioDto.ApplyTo(patchMunicipio, ModelState);

            _mapper.Map(patchMunicipioDto, municipioNegocio);
            _negocio.Patch(idRascunhoProcesso, id, municipioNegocio);
            return NoContent();
        }

        /// <summary>
        /// Exclusão de um município do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do município a ser excluído</param>
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
