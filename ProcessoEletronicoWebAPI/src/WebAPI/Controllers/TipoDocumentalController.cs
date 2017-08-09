using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Base;
using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/tipos-documento")]
    public class TipoDocumentalController : BaseController
    {
        private ITipoDocumentalNegocio _negocio;
        private IMapper _mapper;

        public TipoDocumentalController(ITipoDocumentalNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna o tipo documental de acordo com identificador informado.
        /// </summary>
        /// <param name="id">Identificador do tipo documental.</param>
        /// <returns>Tipo documental de acordo com o identificador informado.</returns>
        /// <response code="200">Tipo documental de acordo com o identificador informado.</response>
        /// <response code="404">Recurso não encotrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}", Name = "GetTipoDocumental")]
        [ProducesResponseType(typeof(TipoDocumentalModeloGet), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id)
        {
            return Ok(_mapper.Map<TipoDocumentalModeloGet>(_negocio.Pesquisar(id)));
        }

        /// <summary>
        /// Retorna a lista de tipos documentais que pertencem à atividade especificada.
        /// </summary>
        /// <param name="idAtividade">Identificador da atividade a qual se deseja obter seus tipos documentais.</param>
        /// <returns>Lista de tipos documentais que pertencem à atividade especificada.</returns>
        /// <response code="200">Retorna a lista de tipos documentais que pertencem à atividade especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("atividade/{idAtividade}")]
        [ProducesResponseType(typeof(List<TipoDocumentalModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Listar(int idAtividade)
        {
            return Ok(_mapper.Map<List<TipoDocumentalModeloGet>>(_negocio.PesquisarPorAtividade(idAtividade)));
        }

        /// <summary>
        /// Insere um tipo documental.
        /// </summary>
        /// <param name="tipoDocumentalPost">Informações do tipo documental a ser inserido.</param>
        /// <returns>Tipo documental recém inserido.</returns>
        /// <response code="201">Tipo documental recém inserido.</response>
        /// <response code="400">Motivo da requisição estar inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "TipoDocumental.Inserir")]
        [ProducesResponseType(typeof(TipoDocumentalModeloGet), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Inserir([FromBody] TipoDocumentalModeloPost tipoDocumentalPost)
        {
            if (tipoDocumentalPost == null)
            {
                return BadRequest();
            }

            TipoDocumentalModeloGet tipoDocumentalGet = _mapper.Map<TipoDocumentalModeloGet>(_negocio.Inserir(_mapper.Map<TipoDocumentalModeloNegocio>(tipoDocumentalPost)));
            return CreatedAtRoute("GetTipoDocumental", new { id = tipoDocumentalGet.Id }, tipoDocumentalGet);
        }

        /// <summary>
        /// Exclui o tipo documental de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do tipo documental.</param>
        /// <response code="200">Tipo Documental excluído com sucesso.</response>
        /// <response code="404">Tipo Documental não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "TipoDocumental.Excluir")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Excluir(int id)
        {
            _negocio.Excluir(id);
            return NoContent();
        }
    }
}
