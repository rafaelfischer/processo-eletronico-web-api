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
    [Route("api/planos-classificacao")]
    public class PlanosClassificacaoController : BaseController
    {
        private IPlanoClassificacaoNegocio _negocio;
        private IMapper _mapper;

        public PlanosClassificacaoController(IPlanoClassificacaoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna a lista de planos de classificação que podem ser utilizados pela organização especificada.
        /// </summary>
        /// <param name="guidOrganizacao">Identificador da organização a qual se deseja obter seus planos de classificação.</param>
        /// <returns>Lista de planos de classificação que podem ser utilizados pela organização especificada.</returns>
        /// <response code="200">Retorna a lista de planos de classificação que podem ser utilizados pela organização especificada.</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("organizacao/{guidOrganizacao}")]
        [ProducesResponseType(typeof(List<PlanoClassificacaoModelo>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(string guidOrganizacao)
        {
            return Ok(_mapper.Map<List<PlanoClassificacaoModelo>>(_negocio.Pesquisar(guidOrganizacao)));
        }

        /// <summary>
        /// Retorna o plano de classificação de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do Plano de Classificação .</param>
        /// <returns>Plano de classificação de acordo com o identificador informado.</returns>
        /// <response code="200">Plano de classificação de acordo com o identificador informado.</response>
        /// <response code="404">Plano de classificação não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}", Name = "GetPlanoClassificacao")]
        [ProducesResponseType(typeof(PlanoClassificacaoProcessoGetModelo), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id)
        {
            return Ok(_mapper.Map<PlanoClassificacaoModelo>(_negocio.Pesquisar(id)));
        }

        /// <summary>
        /// Retorna a lista de planos de classificação que podem ser mantidos pelo usuário..
        /// </summary>
        /// <returns>Lista de planos de classificação que podem ser mantidos pelo usuário.</returns>
        /// <response code="200">Retorna a lista de planos de classificação que podem ser mantidos pelo usuário.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PlanoClassificacaoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<PlanoClassificacaoModelo>>(_negocio.Pesquisar()));
        }
        
        /// <summary>
        /// Insere um plano de classificação de acordo com a organização do usuário.
        /// </summary>
        /// <param name="planoClassificacao">Informações do Plano de Classificacão.</param>
        /// <returns>Lista de planos de classificação que podem ser utilizados pela organização especificada.</returns>
        /// <response code="201">Plano de classificação inserido</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "PlanoClassificacao.Inserir")]
        [ProducesResponseType(typeof(PlanoClassificacaoModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Inserir([FromBody]PlanoClassificacaoModeloPost planoClassificacao)
        {
            if (planoClassificacao == null)
            {
                return BadRequest();
            }

            PlanoClassificacaoModelo planoClassificacaoGet = _mapper.Map<PlanoClassificacaoModelo>(_negocio.Inserir(_mapper.Map<PlanoClassificacaoModeloNegocio>(planoClassificacao)));
            return CreatedAtRoute("GetPlanoClassificacao", new { id = planoClassificacaoGet.Id }, planoClassificacaoGet);
        }

        /// <summary>
        /// Exclui o plano de classificação de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do Plano de Classificação .</param>
        /// <response code="200">Plano de classificação excluído com sucesso.</response>
        /// <response code="404">Plano de classificação não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "PlanoClassificacao.Excluir")]
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
