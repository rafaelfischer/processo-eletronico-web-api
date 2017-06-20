using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using System.Collections.Generic;

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

        [HttpGet]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado)
        {
            return Ok(_mapper.Map<IList<GetContatoDto>>(_negocio.Get(idRascunhoProcesso, idInteressado)));
        }

        [HttpGet("{id}", Name = "GetContatoInteressadoPessoaJuridica")]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado, int id)
        {
            return Ok(_mapper.Map<GetContatoDto>(_negocio.Get(idRascunhoProcesso, idInteressado, id)));
        }

        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
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

        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
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
        public IActionResult Delete(int idRascunhoProcesso, int idInteressado, int id)
        {
            _negocio.Delete(idRascunhoProcesso, idInteressado, id);
            return NoContent();
        }
    }
}
