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
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/interessados-pessoa-fisica/{idInteressado}/emails")]
    public class EmailInteressadoPessoaFisicaController : BaseController
    {
        private IMapper _mapper;
        private IEmailInteressadoPessoaFisicaNegocio _negocio;
        public EmailInteressadoPessoaFisicaController(IMapper mapper, IEmailInteressadoPessoaFisicaNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        /// <summary>
        /// Lista de emails do interessado pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa física</param>
        /// <returns>Lista de emails de interessados pessoa física do rascunho de processos</returns>
        /// <response code="200">Lista de emails do interessado pessoa física do rascunho de processos</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetEmailDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado)
        {
            return Ok(_mapper.Map<IList<GetEmailDto>>(_negocio.Get(idRascunhoProcesso, idInteressado)));
        }
        
        /// <summary>
        /// Email do interessado pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa física</param>
        /// <param name="id">Identificador do email</param>
        /// <returns>Email do interessado pessoa física do rascunho de processos</returns>
        /// <response code="200">Email do interessado pessoa física do rascunho de processos</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetEmailInteressadoPessoaFisica")]
        [ProducesResponseType(typeof(GetEmailDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado, int id)
        {
            return Ok(_mapper.Map<GetEmailDto>(_negocio.Get(idRascunhoProcesso, idInteressado, id)));
        }

        /// <summary>
        /// Inserção de email ao interessado pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa física</param>
        /// <param name="postEmailDto">Informações do email a ser inserido</param>
        /// <returns>Email recém inserido</returns>
        /// <response code="201">Email recém inserido do interessado pessoa física do rascunho de processos</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(GetEmailDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Post(int idRascunhoProcesso, int idInteressado, [FromBody] PostEmailDto postEmailDto)
        {
            if (postEmailDto == null)
            {
                return BadRequest();
            }

            EmailModeloNegocio emailModeloNegocio = _negocio.Post(idRascunhoProcesso, idInteressado, _mapper.Map<EmailModeloNegocio>(postEmailDto));
            GetEmailDto getEmailDto = _mapper.Map<GetEmailDto>(emailModeloNegocio);

            return CreatedAtRoute("GetEmailInteressadoPessoaFisica", new { Id = getEmailDto.Id }, getEmailDto);
        }

        /// <summary>
        /// Alteração de email do interessado pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa física</param>
        /// <param name="id">Identificador do email</param>
        /// <param name="patchEmailDto">Informações do email a ser inserido</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="400"></response>
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
        public IActionResult Patch(int idRascunhoProcesso, int idInteressado, int id, [FromBody] JsonPatchDocument<PatchEmailDto> patchEmailDto)
        {
            EmailModeloNegocio emailModeloNegocio = _negocio.Get(idRascunhoProcesso, idInteressado, id);
            PatchEmailDto emailToPatch = _mapper.Map<PatchEmailDto>(emailModeloNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchEmailDto.ApplyTo(emailToPatch, ModelState);

            _mapper.Map(emailToPatch, emailModeloNegocio);
            _negocio.Patch(idRascunhoProcesso, idInteressado, id, emailModeloNegocio);
            return NoContent();
        }

        /// <summary>
        /// Exclusão de email do interessado pessoa física do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processos</param>
        /// <param name="idInteressado">Identificador do interessado pessoa física</param>
        /// <param name="id">Identificador do email</param>
        /// <returns></returns>
        /// <response code="204"></response>
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
