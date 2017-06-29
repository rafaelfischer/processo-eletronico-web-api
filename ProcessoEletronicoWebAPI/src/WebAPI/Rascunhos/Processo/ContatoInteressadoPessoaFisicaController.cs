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
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/interessados-pessoa-fisica/{idInteressado}/contatos")]
    public class ContatoInteressadoPessoaFisicaController : BaseController
    {
        private IMapper _mapper;
        private IContatoInteressadoPessoaFisicaNegocio _negocio;
        public ContatoInteressadoPessoaFisicaController(IMapper mapper, IContatoInteressadoPessoaFisicaNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        [HttpGet]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado)
        {
            return Ok(_mapper.Map<IList<GetContatoDto>>(_negocio.Get(idRascunhoProcesso, idInteressado)));
        }

        [HttpGet("{id}", Name = "GetContatoInteressadoPessoaFisica")]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado, int id)
        {
            return Ok(_mapper.Map<GetContatoDto>(_negocio.Get(idRascunhoProcesso, idInteressado, id)));
        }

        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Post(int idRascunhoProcesso, int idInteressado, [FromBody] PostContatoDto postContatoDto)
        {
            if (postContatoDto == null)
            {
                return BadRequest();
            }

            ContatoModeloNegocio contatoModeloNegocio = _negocio.Post(idRascunhoProcesso, idInteressado, _mapper.Map<ContatoModeloNegocio>(postContatoDto));
            GetContatoDto getContatoDto = _mapper.Map<GetContatoDto>(contatoModeloNegocio);

            return CreatedAtRoute("GetContatoInteressadoPessoaFisica", new { Id = getContatoDto.Id }, getContatoDto);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
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

        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Delete(int idRascunhoProcesso, int idInteressado, int id)
        {
            _negocio.Delete(idRascunhoProcesso, idInteressado, id);
            return NoContent();
        }
    }
}
