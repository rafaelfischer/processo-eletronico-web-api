﻿using AutoMapper;
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
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/interessados-pessoa-juridica/{idInteressado}/contatos")]
    public class ContatoInteressadoPessoaJuridicaController : BaseController
    {
        private IMapper _mapper;
        private IContatoInteressadoPessoaJuridicaNegocio _negocio;
        public ContatoInteressadoPessoaJuridicaController(IMapper mapper, IContatoInteressadoPessoaJuridicaNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        /// <summary>
        /// Lista de contatos do interessado pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa jurídica</param>
        /// <returns>Lista de contatos de interessados pessoa jurídica do rascunho de processos</returns>
        /// <response code="200">Lista de contatos do interessado pessoa jurídica do rascunho de processos</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetContatoDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado)
        {
            return Ok(_mapper.Map<IList<GetContatoDto>>(_negocio.Get(idRascunhoProcesso, idInteressado)));
        }

        /// <summary>
        /// Contato do interessado pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa jurídica</param>
        /// <param name="id">Identificador do contato</param>
        /// <returns>Contato do interessado pessoa jurídica do rascunho de processos</returns>
        /// <response code="200">Contato do interessado pessoa jurídica do rascunho de processos</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetContatoInteressadoPessoaJuridica")]
        [ProducesResponseType(typeof(GetContatoDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado, int id)
        {
            return Ok(_mapper.Map<GetContatoDto>(_negocio.Get(idRascunhoProcesso, idInteressado, id)));
        }

        /// <summary>
        /// Inserção de contato ao interessado pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa jurídica</param>
        /// <param name="postContatoDto">Informações do contato a ser inserido</param>
        /// <returns>Contato recém inserido</returns>
        /// <response code="201">Contato recém inserido do interessado pessoa jurídica do rascunho de processos</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(GetContatoDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Post(int idRascunhoProcesso, int idInteressado, [FromBody] PostContatoDto postContatoDto)
        {
            if (postContatoDto == null)
            {
                return BadRequest();
            }

            ContatoModeloNegocio contatoModeloNegocio = _negocio.Post(idRascunhoProcesso, idInteressado, _mapper.Map<ContatoModeloNegocio>(postContatoDto));
            GetContatoDto getContatoDto = _mapper.Map<GetContatoDto>(contatoModeloNegocio);

            return CreatedAtRoute("GetContatoInteressadoPessoaJuridica", new { Id = getContatoDto.Id }, getContatoDto);
        }

        /// <summary>
        /// Alteração de contato do interessado pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa física</param>
        /// <param name="id">Identificador do contato</param>
        /// <param name="patchContatoDto">Informações do contato a ser inserido</param>
        /// <returns></returns>
        /// <response code="204">Operação feita com sucesso</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Patch(int idRascunhoProcesso, int idInteressado, int id, [FromBody] JsonPatchDocument<PatchContatoDto> patchContatoDto)
        {
            ContatoModeloNegocio contatoModeloNegocio = _negocio.Get(idRascunhoProcesso, idInteressado, id);
            PatchContatoDto contatoToPatch = _mapper.Map<PatchContatoDto>(contatoModeloNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchContatoDto.ApplyTo(contatoToPatch, ModelState);

            _mapper.Map(contatoToPatch, contatoModeloNegocio);
            _negocio.Patch(idRascunhoProcesso, idInteressado, id, contatoModeloNegocio);
            return NoContent();
        }

        /// <summary>
        /// Exclusão de contato do interessado pessoa jurídica do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa jurídica</param>
        /// <param name="id">Identificador do contato</param>
        /// <returns></returns>
        /// <response code="204">Operação feita com sucesso</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Delete(int idRascunhoProcesso, int idInteressado, int id)
        {
            _negocio.Delete(idRascunhoProcesso, idInteressado, id);
            return NoContent();
        }
    }
}
