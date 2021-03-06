﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Base;
using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    [Route("api/processos")]
    public class ProcessosController : BaseController
    {
        private IProcessoNegocio _negocio;
        private IMapper _mapper;

        public ProcessosController(IProcessoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        #region GET

        /// <summary>
        /// Retorna o processo correspondente ao identificador informado.
        /// </summary>
        /// <param name="id">Identificador do processo.</param>
        /// <returns>Processo correspondente ao identificador.</returns>
        /// <response code="200">Processo correspondente ao identificador.</response>
        /// <response code="404">Processo não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}", Name = "GetProcesso")]
        [ProducesResponseType(typeof(ProcessoCompletoModelo), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<ProcessoCompletoModelo>(_negocio.Pesquisar(id)));
        }

        /// <summary>
        /// Retorna o processo correspondente ao número informado.
        /// </summary>
        /// <param name="numero">Número do processo. Formato: SEQUENCIAL-DD.AAAA.P.E.OOOO</param>
        /// <returns>Processo correspondente ao número.</returns>
        /// <response code="200">Retorna o processo correspondente ao número.</response>
        /// <response code="400">Número do processo inválido.</response>
        /// <response code="404">Proceso não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("numero/{numero}")]
        [ProducesResponseType(typeof(ProcessoCompletoModelo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPorNumero(string numero)
        {
            return Ok(_mapper.Map<ProcessoCompletoModelo>(_negocio.Pesquisar(numero)));
        }

        /// <summary>
        /// Retorna os dados básicos do  processo correspondente ao número informado.
        /// </summary>
        /// <param name="numero">Número do processo. Formato: SEQUENCIAL-DD.AAAA.P.E.OOOO</param>
        /// <returns>Processo correspondente ao número.</returns>
        /// <response code="200">Retorna o processo correspondente ao número.</response>
        /// <response code="400">Número do processo inválido.</response>
        /// <response code="404">Proceso não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("dados-basicos/{numero}")]
        [ProducesResponseType(typeof(ProcessoSimplificadoModelo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSimplificadoPorNumero(string numero)
        {
            return Ok(_mapper.Map<ProcessoSimplificadoModelo>(_negocio.PesquisarSimplificado(numero)));
        }

        /// <summary>
        /// Retorna lista de processos que posuem pelo menos um despacho feito pelo usuario autenticado.
        /// </summary>
        /// <returns>Processo correspondente ao número.</returns>
        /// <response code="200">Processos que posuem pelo menos um despacho feito pelo usuario autenticado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("usuario")]
        [ProducesResponseType(typeof(List<ProcessoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetProcessosDespachadosPorUsuario()
        {
            return Ok(_mapper.Map<List<ProcessoModelo>>(_negocio.PesquisarProcessosDespachadosUsuario()));
        }

        /// <summary>
        /// Retorna a lista de processos que estão tramintando na unidade especificada.
        /// </summary>
        /// <param name="guidUnidade">Identificador da unidade onde os processos estão tramitando.</param>
        /// <returns>Lista de processos que estão tramintando na unidade especificada.</returns>
        /// <response code="200">Retorna a lista de processos que estão tramintando na unidade especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("unidade/{guidUnidade}")]
        [ProducesResponseType(typeof(List<ProcessoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPorUnidade(string guidUnidade)
        {
            return Ok(_mapper.Map<List<ProcessoModelo>>(_negocio.PesquisarProcessosNaUnidade(guidUnidade)));
        }

        /// <summary>
        /// Retorna a lista de processos que estão tramitando na organização especificada.
        /// </summary>
        /// <param name="guidOrganizacao">Identificador da organização onde os processos estão tramintando.</param>
        /// <returns>Lista de processos que estão tramintando na organização especificada.</returns>
        /// <response code="200">Retorna a lista de processos que estão tramintando na organização especificada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("organizacao/{guidOrganizacao}")]
        [ProducesResponseType(typeof(List<ProcessoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarPorOganizacao(string guidOrganizacao)
        {
            return Ok(_mapper.Map<List<ProcessoModelo>>(_negocio.PesquisarProcessosNaOrganizacao(guidOrganizacao)));
        }
        #endregion

        #region POST

        /// <summary>
        /// Autuação de Processos (inserção de processos).
        /// </summary>
        /// <remarks>
        /// Apesar das listas de interessados estarem sinalizadas como opcionais, o Processo deve possuir ao menos um interessado (seja ele pessoa física ou jurídica).
        /// O campo "conteudo" dos anexos do processo é uma string. O arquivo deve ser codificado para uma string base64 antes de ser enviado para a API.
        /// Quanto aos parâmetros, o processo a ser autuado pode ser passado no corpo da requisição ou então pode ser informado o identificador de um rascunho de processos.
        /// O identificador do rascunho terá prioridade na escolha caso ambos os parâmetros sejam informados.
        /// </remarks>
        /// <param name="processoPost">Informações do processo.</param>
        /// <param name="idRascunhoProcesso">Identificador do rascunho de processo.</param>
        /// <returns>URL do processo inserido no cabeçalho da resposta e o processo recém inserido</returns>
        /// <response code="201">Retorna o processo recém inserido.</response>
        /// <response code="400">Retorna o motivo da requisição estar inválida.</response>
        /// <response code="400">Recurso não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "Processo.Autuar")]
        [ProducesResponseType(typeof(ProcessoCompletoModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody]ProcessoModeloPost processoPost, [FromQuery] int idRascunhoProcesso)
        {

            if (idRascunhoProcesso > 0)
            {
                ProcessoCompletoModelo GetProcesso = _mapper.Map<ProcessoCompletoModelo>(_negocio.Post(idRascunhoProcesso));
                return CreatedAtRoute("GetProcesso", new { id = GetProcesso.Id }, GetProcesso);
            }


            if (processoPost == null)
            {
                return BadRequest();
            }

            ProcessoCompletoModelo processoCompleto = _mapper.Map<ProcessoCompletoModelo>(_negocio.Autuar(_mapper.Map<ProcessoModeloNegocio>(processoPost)));
            return CreatedAtRoute("GetProcesso", new { id = processoCompleto.Id }, processoCompleto);
            
        }
        #endregion
    }
}
