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
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/interessados-pessoa-juridica")]
    public class InteressadosPessoaJuridicaController : BaseController
    {
        IMapper _mapper;
        IInteressadoPessoaJuridicaNegocio _negocio;

        public InteressadosPessoaJuridicaController(IMapper mapper, IInteressadoPessoaJuridicaNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        /// <summary>
        /// Lista de interessados pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <returns>Interessados pessoa jurídica do rascunho de processos</returns>
        /// <response code="200">Retorna a lista de interessado pessoa jurídica</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        [ProducesResponseType(typeof(List<GetInteressadoPessoaJuridicaDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetInteressadoPessoaJuridicaDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        /// <summary>
        /// Interessado pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do Interessado pessoa jurídica</param>
        /// <returns>Interessado pessoa jurídica de acordo com o identificador informado</returns>
        /// <response code="200">Interessado pessoa jurídica de acordo com o identificador informado</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetInteressadoPessoaJuridica")]
        [ProducesResponseType(typeof(GetInteressadoPessoaJuridicaDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetInteressadoPessoaJuridicaDto>(_negocio.Get(idRascunhoProcesso, id)));
        }

        /// <summary>
        /// Inserção de interessados pessoa jurídica no rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="interessadoPessoaJuridicaDto">Informações do interessado pessoa física a ser inserido</param>
        /// <returns>Interessado pessoa jurídica recém inserido</returns>
        /// <response code="201">Interessado pessoa jurídica recém inserido</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(GetInteressadoPessoaJuridicaDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Post (int idRascunhoProcesso, [FromBody] PostInteressadoPessoaJuridicaDto interessadoPessoaJuridicaDto)
        {
            if (interessadoPessoaJuridicaDto == null)
            {
                return BadRequest();
            }

            InteressadoPessoaJuridicaModeloNegocio intressadoPessoaJuridicaNegocio = _negocio.Post(idRascunhoProcesso, _mapper.Map<InteressadoPessoaJuridicaModeloNegocio>(interessadoPessoaJuridicaDto));
            GetInteressadoPessoaJuridicaDto getInteressadoPessoaJuridicaDto = _mapper.Map<GetInteressadoPessoaJuridicaDto>(intressadoPessoaJuridicaNegocio);

            return CreatedAtRoute("GetInteressadoPessoaJuridica", new { Id = getInteressadoPessoaJuridicaDto.Id }, getInteressadoPessoaJuridicaDto);
        }

        /// <summary>
        /// Alteração de um interessado pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do Interessado pessoa jurídica</param>
        /// <param name="patchInteressadoPessoaJuridicaDto">Informações a serem alteradas (JSON Patch Document)</param>
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
        public IActionResult Patch (int idRascunhoProcesso, int id, [FromBody] JsonPatchDocument<PatchInteressadoPessoaJuridicaDto> patchInteressadoPessoaJuridicaDto)
        {
            if (patchInteressadoPessoaJuridicaDto == null)
            {
                return BadRequest();
            }

            InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio = _negocio.Get(idRascunhoProcesso, id);
            PatchInteressadoPessoaJuridicaDto interessadoPessoaJuridica = _mapper.Map<PatchInteressadoPessoaJuridicaDto>(interessadoPessoaJuridicaNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchInteressadoPessoaJuridicaDto.ApplyTo(interessadoPessoaJuridica, ModelState);

            _mapper.Map(interessadoPessoaJuridica, interessadoPessoaJuridicaNegocio);
            _negocio.Patch(idRascunhoProcesso, id, interessadoPessoaJuridicaNegocio);
            return NoContent();
        }

        /// <summary>
        /// Exclusão de um interessado pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="id">Identificador do interessado pessoa jurídica a ser excluído</param>
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
