using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base;
using ProcessoEletronicoService.WebAPI.Base;
using System.Collections.Generic;

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

        [HttpGet]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetInteressadoPessoaFisicaDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        [HttpGet("{id}", Name = "GetInteressadoPessoaFisica")]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetInteressadoPessoaFisicaDto>(_negocio.Get(idRascunhoProcesso, id)));
        }
        
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
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

        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        public IActionResult Patch (int idRascunhoProcesso, int id, [FromBody] JsonPatchDocument<PatchInteressadoPessoaFisicaDto> patchInteressadoPessoaFisica)
        {
            if (patchInteressadoPessoaFisica == null)
            {
                return BadRequest();
            }

            InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio = _negocio.Get(idRascunhoProcesso, id);
            PatchInteressadoPessoaFisicaDto interessadoPessoaFisica = _mapper.Map<PatchInteressadoPessoaFisicaDto>(interessadoPessoaFisicaNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchInteressadoPessoaFisica.ApplyTo(interessadoPessoaFisica, ModelState);

            _mapper.Map(interessadoPessoaFisica, interessadoPessoaFisicaNegocio);
            _negocio.Patch(idRascunhoProcesso, id, interessadoPessoaFisicaNegocio);
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
