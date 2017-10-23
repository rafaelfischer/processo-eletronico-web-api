using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.WebAPI.Base;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/anexos")]
    public class AnexosController : BaseController
    {
        private IAnexoNegocio _negocio;
        private IMapper _mapper;

        public AnexosController(IAnexoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna o anexo correspondente ao identificador informado.
        /// </summary>
        /// <param name="id">Identificador do anexo.</param>
        /// <returns>Anexo correspondente ao identificador.</returns>
        /// <response code="200">Anexo correspondente ao identificador.</response>
        /// <response code="404">Anexo não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
        public IActionResult Pesquisar(int id)
        {
            return Ok(_mapper.Map<AnexoModeloGet>(_negocio.Pesquisar(id)));
        }
    }
}
