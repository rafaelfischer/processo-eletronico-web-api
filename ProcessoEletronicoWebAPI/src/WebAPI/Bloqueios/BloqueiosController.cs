using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Negocio.Bloqueios;
using Negocio.Bloqueios.Base;
using ProcessoEletronicoService.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Bloqueios
{
    [Route("api/processos/{idProcesso}/bloqueios")]
    public class BloqueiosController : BaseController
    {
        private IMapper _mapper;
        private IBloqueioCore _core;

        public BloqueiosController(IBloqueioCore core, IMapper mapper)
        {
            _core = core;
            _mapper = mapper;
        }

        
        /// <summary>
        /// Retorna obloqueio do processo de acordo com o identificador informado
        /// </summary>
        /// <response code="200">Lista de bloqueios do processo</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Erro inesperado</response>
        [HttpGet("{id}", Name = "GetSingleBloqueioOfProcesso")]
        public IActionResult GetSingleBloqueioOfProcesso(int idProcesso, int id)
        {
            return Ok(_mapper.Map<GetBloqueioDto>(_core.GetSingleBloqueio(idProcesso, id)));
        }

        /// <summary>
        /// Retorna a lista de bloqueios do processo ordernados descrescentemente pela data do ínicio do bloqueio
        /// </summary>
        /// <response code="200">Lista de bloqueios do processo</response>
        /// <response code="500">Erro inesperado</response>
        [HttpGet]
        public IActionResult GetBloqueiosOfProcesso(int idProcesso)
        {
            return Ok(_mapper.Map<IList<GetBloqueioDto>>(_core.GetBloqueiosOfProcesso(idProcesso)).OrderByDescending(a => a.DataInicio));
        }

        /// <summary>
        /// Insere um bloqueio ao processo
        /// </summary>
        /// <response code="201">Bloqueio recém inserido</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPost]
        public IActionResult InsertBloqueiosIntoProcesso(int idProcesso, [FromBody] PostBloqueioDto postBloqueioDto)
        {
            if (postBloqueioDto == null)
            {
                return BadRequest();
            }

            GetBloqueioDto getBloqueioDto = _mapper.Map<GetBloqueioDto>(_core.InsertBloqueioIntoProcesso(idProcesso, _mapper.Map<BloqueioModel>(postBloqueioDto)));
            return CreatedAtRoute("GetSingleBloqueioOfProcesso", new { idProcesso = idProcesso, id = getBloqueioDto.Id }, getBloqueioDto);

        }

        /// <summary>
        /// Remome bloqueio do processo
        /// </summary>
        /// <response code="204">Bloqueio removido com sucesso</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Erro inesperado</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteBloqueioOfProcesso (int idProcesso, int id)
        {
            _core.DeleteBloqueioOfProcesso(idProcesso, id);
            return NoContent();
        }
    }
}
