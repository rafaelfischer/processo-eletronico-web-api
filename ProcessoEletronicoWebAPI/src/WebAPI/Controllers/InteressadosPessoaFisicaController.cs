using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Controllers
{

    /*
     A rota padrão utiliza o Rascunho do processo, mas há métodos GET que utilizam o identificador do Processo.
     Nos métodos que utilizarem o identificador do Processo, a rota completa será definida logo acima do método.
     Não há métodos que resultem em maninupalção de dados (POST, DELETE, PATCH) que utilizam o identificador do processo, apenas rascunho de processos.
    */
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/interessados-pessoa-fisica")]
    public class InteressadosPessoaFisicaController : BaseController
    {
        IMapper _mapper;
        IInteressadoPessoaFisicaNegocio _negocio;

        public InteressadosPessoaFisicaController(IMapper mapper, IInteressadoPessoaFisicaNegocio negocio, IHttpContextAccessor httpContextAccessor, IClientAccessToken clientAccessToken) : base(httpContextAccessor, clientAccessToken)
        {
            _mapper = mapper;
            _negocio = negocio;
            _negocio.Usuario = UsuarioAutenticado;
        }

        [HttpGet]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetInteressadoPessoaFisicaDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        [HttpGet("{id}", Name = "GetInteressado")]
        public IActionResult Get(int idRascunhoProcesso, int id)
        {
            return Ok(_mapper.Map<GetInteressadoPessoaFisicaDto>(_negocio.Get(idRascunhoProcesso, id)));
        }
        
        [HttpPost]
        public IActionResult Post (int idRascunhoProcesso, [FromBody] PostInteressadoPessoaFisicaDto interessadoPessoaFisicaDto)
        {
            if (interessadoPessoaFisicaDto == null)
            {
                return BadRequest();
            }

            InteressadoPessoaFisicaModeloNegocio intressadoPessoaFisicaNegocio = _negocio.Post(idRascunhoProcesso, _mapper.Map<InteressadoPessoaFisicaModeloNegocio>(interessadoPessoaFisicaDto));
            GetInteressadoPessoaFisicaDto getInteressadoPessoaFisicaDto = _mapper.Map<GetInteressadoPessoaFisicaDto>(intressadoPessoaFisicaNegocio);

            return CreatedAtRoute("GetInteressado", new { Id = getInteressadoPessoaFisicaDto.Id }, getInteressadoPessoaFisicaDto);
        }

        [HttpPatch("{id}")]
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
        public IActionResult Delete(int idRascunhoProcesso, int id)
        {
            _negocio.Delete(idRascunhoProcesso, id);
            return NoContent();
        }
        
    }
}
