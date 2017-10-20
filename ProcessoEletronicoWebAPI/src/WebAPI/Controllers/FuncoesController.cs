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
    [Route("api/funcoes")]
    public class FuncoesController : BaseController
    {
        private IFuncaoNegocio _negocio;
        private IMapper _mapper;

        public FuncoesController(IFuncaoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna a lista de funções que pertencem ao plano de classificação especificado.
        /// </summary>
        /// <param name="idPlanoClassificacao">Identificador do plano de classificação do qual se deseja obter suas atividades.</param>
        /// <returns>Lista de funções que pertencem ao plano de classificação especificado.</returns>
        /// <response code="200">Retorna a lista de funções que pertencem ao plano de classificação especificado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("plano-classificacao/{idPlanoClassificacao}")]
        [ProducesResponseType(typeof(List<FuncaoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int idPlanoClassificacao)
        {
            return Ok(_mapper.Map<List<FuncaoModelo>>(_negocio.PesquisarPorPlanoClassificacao(idPlanoClassificacao)));
        }

        /// <summary>
        /// Retorna a função de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da função.</param>
        /// <returns>Função de acordo com o identificador informado.</returns>
        /// <response code="200">Função de acordo com o identificador informado.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}", Name = "GetFuncao")]
        [ProducesResponseType(typeof(FuncaoProcessoGetModelo), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id)
        {
            return Ok(_mapper.Map<FuncaoProcessoGetModelo>(_negocio.Pesquisar(id)));
        }

        /// <summary>
        /// Insere uma função 
        /// </summary>
        /// <param name="funcao">Informações da função a ser inserida.</param>
        /// <returns>Função inserida</returns>
        /// <response code="201">Função inserida</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "Funcao.Inserir")]
        [ProducesResponseType(typeof(FuncaoProcessoGetModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Inserir([FromBody] FuncaoModeloPost funcao)
        {
            if (funcao == null)
            {
                return BadRequest();
            }

            FuncaoProcessoGetModelo funcaoGet = _mapper.Map<FuncaoProcessoGetModelo>(_negocio.Inserir(_mapper.Map<FuncaoModeloNegocio>(funcao)));
            return CreatedAtRoute("GetFuncao", new { id = funcaoGet.Id }, funcaoGet);
        }

        /// <summary>
        /// Exclui a função de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da função.</param>
        /// <response code="200">Função excluída com sucesso.</response>
        /// <response code="404">Função não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Funcao.Excluir")]
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
