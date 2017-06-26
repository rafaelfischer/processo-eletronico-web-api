using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo
{
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/sinalizacoes")]
    public class SinalizacoesController : BaseController
    {
        IMapper _mapper;
        ISinalizacaoNegocio _negocio;

        public SinalizacoesController(IMapper mapper, ISinalizacaoNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        [HttpGet(Name = "GetSinalizacoes")]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetSinalizacaoDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        [HttpGet("{id}", Name = "GetSinalizacao")]
        public IActionResult Get(int idRascunhoProcesso, int idSinalizacao)
        {
            return Ok(_mapper.Map<GetSinalizacaoDto>(_negocio.Get(idRascunhoProcesso, idSinalizacao)));
        }
        
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        public IActionResult Post (int idRascunhoProcesso, [FromBody] IList<int> idsSinalizacoes)
        {
            if (idsSinalizacoes == null)
            {
                return BadRequest();
            }

            IList<GetSinalizacaoDto> sinalizacoes = _mapper.Map<IList<GetSinalizacaoDto>>(_negocio.Post(idRascunhoProcesso, idsSinalizacoes));
            return CreatedAtRoute("GetSinalizacoes", sinalizacoes);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        public IActionResult Delete(int idRascunhoProcesso, int idSinalizacao)
        {
            _negocio.Delete(idRascunhoProcesso, idSinalizacao);
            return NoContent();
        }
    }
}
