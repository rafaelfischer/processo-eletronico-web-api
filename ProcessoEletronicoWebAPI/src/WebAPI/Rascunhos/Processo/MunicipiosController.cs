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

        [HttpGet]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetMunicipioDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        [HttpGet("{id}", Name = "GetMunicipio")]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetMunicipioDto>(_negocio.Get(idRascunhoProcesso, id)));
        }
        
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
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

        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Patch (int idRascunhoProcesso, int id, [FromBody] JsonPatchDocument<PatchMunicipioDto> patchMunicipio)
        {
            if (patchMunicipio == null)
            {
                return BadRequest();
            }

            MunicipioProcessoModeloNegocio municipioNegocio = _negocio.Get(idRascunhoProcesso, id);
            PatchMunicipioDto patchMunicipioDto = _mapper.Map<PatchMunicipioDto>(municipioNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchMunicipio.ApplyTo(patchMunicipioDto, ModelState);

            _mapper.Map(patchMunicipioDto, municipioNegocio);
            _negocio.Patch(idRascunhoProcesso, id, municipioNegocio);
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
