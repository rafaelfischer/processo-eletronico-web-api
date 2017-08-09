using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Net;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/atividades")]
    public class AtividadesController : BaseController
    {
        private IAtividadeNegocio _negocio;
        private IMapper _mapper;

        public AtividadesController(IAtividadeNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna a lista de atividades que pertencem à função especificada.
        /// </summary>
        /// <param name="idFuncao">Identificador da função a qual se deseja obter suas atividades.</param>
        /// <returns>Lista de atividades que pertencem à função especificada.</returns>
        /// <response code="200">Retorna a lista de atividades que pertencem à função especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("funcao/{idFuncao}")]
        [ProducesResponseType(typeof(List<AtividadeModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int idFuncao)
        {
            return Ok(_mapper.Map<List<AtividadeModelo>>(_negocio.PesquisarPorFuncao(idFuncao)));
        }

        /// <summary>
        /// Retorna a lista de atividades que o usuário pode utilizar.
        /// </summary>
        /// <returns>Lista de atividades que que o usuário pode utulizar.</returns>
        /// <response code="200">Retorna a lista de atividades que o usuário pode utilizar.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<AtividadeProcessoGetModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<AtividadeProcessoGetModelo>>(_negocio.Pesquisar()));
        }

        /// <summary>
        /// Retorna a atividade de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da atividade.</param>
        /// <returns>Atividade de acordo com o identificador informado.</returns>
        /// <response code="200">Atividade de acordo com o identificador informado.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}", Name = "GetAtividade")]
        [ProducesResponseType(typeof(AtividadeProcessoGetModelo), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id)
        {
            return Ok(_mapper.Map<AtividadeProcessoGetModelo>(_negocio.Pesquisar(id)));
        }

        /// <summary>
        /// Insere uma atividade
        /// </summary>
        /// <param name="atividade">Informações da atividade a ser inserida.</param>
        /// <returns>A atividade inserida.</returns>
        /// <response code="201">Atividade inserida</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "Atividade.Inserir")]
        [ProducesResponseType(typeof(AtividadeProcessoGetModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Inserir([FromBody] AtividadeModeloPost atividade)
        {
            if (atividade == null)
            {
                return BadRequest();
            }

            AtividadeProcessoGetModelo atividadeGet = _mapper.Map<AtividadeProcessoGetModelo>(_negocio.Inserir(_mapper.Map<AtividadeModeloNegocio>(atividade)));
            return CreatedAtRoute("GetAtividade", new { id = atividadeGet.Id }, atividadeGet);
        }

        /// <summary>
        /// Exclui a atividade de acordo com o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da atividade.</param>
        /// <response code="200">Atividade excluída com sucesso.</response>
        /// <response code="404">Atividade não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Atividade.Excluir")]
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
