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

        [HttpGet]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetInteressadoPessoaJuridicaDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        [HttpGet("{id}", Name = "GetInteressadoPessoaJuridica")]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetInteressadoPessoaJuridicaDto>(_negocio.Get(idRascunhoProcesso, id)));
        }
        
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
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

        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        public IActionResult Patch (int idRascunhoProcesso, int id, [FromBody] JsonPatchDocument<PatchInteressadoPessoaJuridicaDto> patchInteressadoPessoaJuridica)
        {
            if (patchInteressadoPessoaJuridica == null)
            {
                return BadRequest();
            }

            InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio = _negocio.Get(idRascunhoProcesso, id);
            PatchInteressadoPessoaJuridicaDto interessadoPessoaJuridica = _mapper.Map<PatchInteressadoPessoaJuridicaDto>(interessadoPessoaJuridicaNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchInteressadoPessoaJuridica.ApplyTo(interessadoPessoaJuridica, ModelState);

            _mapper.Map(interessadoPessoaJuridica, interessadoPessoaJuridicaNegocio);
            _negocio.Patch(idRascunhoProcesso, id, interessadoPessoaJuridicaNegocio);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        public IActionResult Delete(int idRascunhoProcesso, int id)
        {
            _negocio.Delete(idRascunhoProcesso, id);
            return NoContent();
        }
        
    }
}
