using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Modelos;
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


        [HttpGet]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetAnexoDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        [HttpGet("{id}", Name = "GetAnexo")]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetAnexoDto>(_negocio.Get(idRascunhoProcesso, id)));
        }
        
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
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

        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Patch (int idRascunhoProcesso, int id, [FromBody] JsonPatchDocument<PatchAnexoDto> patchAnexo)
        {
            if (patchAnexo == null)
            {
                return BadRequest();
            }

            AnexoModeloNegocio anexoNegocio = _negocio.Get(idRascunhoProcesso, id);
            PatchAnexoDto patchAnexoDto = _mapper.Map<PatchAnexoDto>(anexoNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchAnexo.ApplyTo(patchAnexoDto, ModelState);

            _mapper.Map(patchAnexoDto, anexoNegocio);
            _negocio.Patch(idRascunhoProcesso, id, anexoNegocio);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Delete(int idRascunhoProcesso, int id)
        {
            _negocio.Delete(idRascunhoProcesso, id);
            return NoContent();
        }
    }
}
