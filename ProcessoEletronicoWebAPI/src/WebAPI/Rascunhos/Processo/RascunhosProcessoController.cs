using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using System;
using System.Collections.Generic;
using WebAPI.Config;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo
{
    [Route("api/rascunhos-processo")]
    public class RascunhosProcessoController : BaseController
    {
        IRascunhoProcessoNegocio _negocio;
        IMapper _mapper;

        public RascunhosProcessoController(IRascunhoProcessoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        #region GET

        /// <summary>
        /// Retorna o rascunho de processo correspondente ao identificador informado.
        /// </summary>
        /// <param name="id">Identificador do rascunho de processo.</param>
        /// <returns>Processo correspondente ao identificador.</returns>
        /// <response code="200">Rascunho de processo correspondente ao identificador.</response>
        /// <response code="404">Rascunho de processo não foi encontrado.</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetRascunhoProcesso")]
        [ProducesResponseType(typeof(GetRascunhoProcessoDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Pesquisar(int id)
        {
            return Ok(_mapper.Map<GetRascunhoProcessoDto>(_negocio.Get(id)));
        }

        /// <summary>
        /// Retorna a lista de rascunhos de processo da organização especificada
        /// </summary>
        /// <param name="guidOrganizacao">Identificador da organização</param>
        /// <returns>Lista de rascunhos de processo da organização especificada</returns>
        /// <response code="200">Retorna a lista de rascunhos de processo da organização especificada.</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("organizacao/{guidOrganizacao}")]
        [ProducesResponseType(typeof(List<GetRascunhoProcessoPorOrganizacaoDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult PesquisarPorOganizacao(string guidOrganizacao)
        {
            Guid guid;
            if (Guid.TryParse(guidOrganizacao, out guid))
            {
                return Ok(_mapper.Map<List<GetRascunhoProcessoPorOrganizacaoDto>>(_negocio.Get(guid)));
            }

            //Guid inválido
            return BadRequest();

        }
        #endregion
        #region POST

        /// <summary>
        /// Inserção de Rascunhos de Processos
        /// </summary>
        /// <param name="rascunhoProcessoPost">Informações do rascunho de processo</param>
        /// <returns>URL do rascunho de processo inserido no cabeçalho da resposta e o rascunho de processo recém inserido</returns>
        /// <response code="201">Retorna o rascunho de processo recém inserido</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Informa o motivo do objeto estar inválido</response>
        /// <response code="500">Retorna a descrição do erro</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(GetRascunhoProcessoDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Salvar([FromBody]PostRascunhoProcessoDto rascunhoProcessoPost)
        {
            if (rascunhoProcessoPost == null)
            {
                return BadRequest();
            }

            //Mapeia para o modelo de negócio, obém o resultado e mapeia para o modelo de apresentação
            GetRascunhoProcessoDto rascunhoProcesso = _mapper.Map<GetRascunhoProcessoDto>(_negocio.Post(_mapper.Map<RascunhoProcessoModeloNegocio>(rascunhoProcessoPost)));
            return CreatedAtRoute("GetRascunhoProcesso", new { id = rascunhoProcesso.Id }, rascunhoProcesso);
        }
        #endregion
        #region PATCH

        /// <summary>
        /// Altera o rascunho de processos de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da organização</param>
        /// <param name="patchRascunhoProcesso">Informações a serem alteradas no Rascunho (JSON Patch Document)</param>
        /// <returns></returns>
        /// <response code="204">Operação feita com sucesso</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Rascunho de processo não encontrado</response>
        /// <response code="500">Retorna a descrição do erro</response>
        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Alterar(int id, [FromBody]  JsonPatchDocument<PatchRascunhoProcessoDto> patchRascunhoProcesso)
        {
            if (patchRascunhoProcesso == null)
            {
                return BadRequest();
            }

            RascunhoProcessoModeloNegocio rascunhoProcessoNegocio = _negocio.Get(id);
            PatchRascunhoProcessoDto rascunhoProcesso = _mapper.Map<PatchRascunhoProcessoDto>(rascunhoProcessoNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchRascunhoProcesso.ApplyTo(rascunhoProcesso, ModelState);

            _mapper.Map(rascunhoProcesso, rascunhoProcessoNegocio);
            _negocio.Patch(id, rascunhoProcessoNegocio);
            return NoContent();

        }
        #endregion
        #region DELETE
        /// <summary>
        /// Exclui o rascunho de processo de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do rascunho de processos.</param>
        /// <response code="204">Operação feita com sucesso</response>
        /// <response code="404">Rascunho de processo não encontrado.</response>
        /// <response code="500">Falha inesperada</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Excluir(int id)
        {
            _negocio.Delete(id);
            return NoContent();
        }
        #endregion
    }
}
