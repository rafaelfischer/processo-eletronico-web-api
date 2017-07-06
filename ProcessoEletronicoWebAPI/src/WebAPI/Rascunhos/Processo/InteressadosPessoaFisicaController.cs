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
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/interessados-pessoa-fisica")]
    public class InteressadosPessoaFisicaController : BaseController
    {
        IMapper _mapper;
        IInteressadoPessoaFisicaNegocio _negocio;

        public InteressadosPessoaFisicaController(IMapper mapper, IInteressadoPessoaFisicaNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        /// <summary>
        /// Lista de interessados pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <returns>Interessados pessoa física do rascunho de processos</returns>
        /// <response code="200">Retorna a lista de interessado pessoa física</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        [ProducesResponseType(typeof(List<GetInteressadoPessoaFisicaDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetInteressadoPessoaFisicaDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        /// <summary>
        /// Interessado pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do Interessado pessoa física</param>
        /// <returns>Interessado pessoa física de acordo com o identificador informado</returns>
        /// <response code="200">Anexo de acordo com o identificador informado</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetInteressadoPessoaFisica")]
        [ProducesResponseType(typeof(GetInteressadoPessoaFisicaDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetInteressadoPessoaFisicaDto>(_negocio.Get(idRascunhoProcesso, id)));
        }

        /// <summary>
        /// Inserção de interessados pessoa física no rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="interessadoPessoaFisicaDto">Informações do interessado pessoa física a ser inserido</param>
        /// <returns>Interessado pessoa física recém inserido</returns>
        /// <response code="201">Interessado pessoa física recém inserido</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(GetInteressadoPessoaFisicaDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Post (int idRascunhoProcesso, [FromBody] PostInteressadoPessoaFisicaDto interessadoPessoaFisicaDto)
        {
            if (interessadoPessoaFisicaDto == null)
            {
                return BadRequest();
            }

            InteressadoPessoaFisicaModeloNegocio intressadoPessoaFisicaNegocio = _negocio.Post(idRascunhoProcesso, _mapper.Map<InteressadoPessoaFisicaModeloNegocio>(interessadoPessoaFisicaDto));
            GetInteressadoPessoaFisicaDto getInteressadoPessoaFisicaDto = _mapper.Map<GetInteressadoPessoaFisicaDto>(intressadoPessoaFisicaNegocio);

            return CreatedAtRoute("GetInteressadoFisica", new { Id = getInteressadoPessoaFisicaDto.Id }, getInteressadoPessoaFisicaDto);
        }

        /// <summary>
        /// Alteração de um interessado pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do Interessado pessoa física</param>
        /// <param name="patchInteressadoPessoaFisicaDto">Informações a serem alteradas (JSON Patch Document)</param>
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
        public IActionResult Patch (int idRascunhoProcesso, int id, [FromBody] JsonPatchDocument<PatchInteressadoPessoaFisicaDto> patchInteressadoPessoaFisicaDto)
        {
            if (patchInteressadoPessoaFisicaDto == null)
            {
                return BadRequest();
            }

            InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio = _negocio.Get(idRascunhoProcesso, id);
            PatchInteressadoPessoaFisicaDto interessadoPessoaFisica = _mapper.Map<PatchInteressadoPessoaFisicaDto>(interessadoPessoaFisicaNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchInteressadoPessoaFisicaDto.ApplyTo(interessadoPessoaFisica, ModelState);

            _mapper.Map(interessadoPessoaFisica, interessadoPessoaFisicaNegocio);
            _negocio.Patch(idRascunhoProcesso, id, interessadoPessoaFisicaNegocio);
            return NoContent();
        }

        /// <summary>
        /// Exclusão de um interessado pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do interessado pessoa física a ser excluído</param>
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
