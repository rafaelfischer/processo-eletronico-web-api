using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Sinalizacoes.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Sinalizacoes.Modelos;
using System.Collections.Generic;

namespace WebAPI.Sinalizacoes
{
    [Route("api/sinalizacoes")]
    public class SinalizacoesController : BaseController
    {
        private ISinalizacaoNegocio _negocio;
        private IMapper _mapper;

        public SinalizacoesController(ISinalizacaoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna a lista de sinalizações da organização patriarca do usuário (caso ele possua).
        /// </summary>
        /// <returns>Lista de sinalizações da organização patriarca do usuário</returns>
        /// <response code="200">Lista de sinalizações da organização patriarca especificada.</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Erro inesperado</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSinalizacaoNoImagemDto>), 200)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<IList<GetSinalizacaoNoImagemDto>>(_negocio.Get()));
        }

        /// <summary>
        /// Retorna a lista de sinalizações da organização patriarca especificada.
        /// </summary>
        /// <param name="guidOrganizacaoPatriarca">Identificador da organização patriarca a qual se deseja obter suas sinalizações.</param>
        /// <returns>Lista de sinalizações da organização patriarca especificada.</returns>
        /// <response code="200">Lista de sinalizações da organização patriarca especificada.</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Erro inesperado</response>
        [HttpGet("por-organizacao/{guidOrganizacaoPatriarca}")]
        [ProducesResponseType(typeof(List<GetSinalizacaoNoImagemDto>), 200)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(string guidOrganizacaoPatriarca)
        {
            return Ok(_mapper.Map<IList<GetSinalizacaoNoImagemDto>>(_negocio.Get(guidOrganizacaoPatriarca)));
        }

        /// <summary>
        /// Retorna a sinalização de acordo com o identificador informado
        /// </summary>
        /// <param name="id">Identificador da sinalização</param>
        /// <returns>Lista de sinalizações da organização patriarca especificada</returns>
        /// <response code="200">Sinalização de acordo com o identificador informado</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Erro inesperado</response>
        [HttpGet("{id}", Name = "GetSinalizacao")]
        [ProducesResponseType(typeof(GetSinalizacaoDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<GetSinalizacaoDto>(_negocio.Get(id)));
        }

        /// <summary>
        /// Inserção de sinalizações
        /// </summary>
        /// <param name="postSinalizacaoDto">Informações da sinalização a ser inserida</param>
        /// <returns>Sinalização recém inserida</returns>
        /// <response code="201">Inserção realizca com sucesso</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPost]
        [ProducesResponseType(typeof(GetSinalizacaoDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Insert([FromBody] PostSinalizacaoDto postSinalizacaoDto)
        {
            if (postSinalizacaoDto == null)
            {
                return BadRequest();
            }

            GetSinalizacaoDto getSinalizacaoDto = _mapper.Map<GetSinalizacaoDto>(_negocio.Insert(_mapper.Map<SinalizacaoModeloNegocio>(postSinalizacaoDto)));
            return CreatedAtRoute("GetSinalizacao", new { Id = getSinalizacaoDto.Id }, getSinalizacaoDto);
        }

        /// <summary>
        /// Inserção de sinalizações
        /// </summary>
        /// <param name="id">Identificador da sinalização</param>
        /// <param name="patchSinalizacao">Informações da sinalização a ser a ser alterada (JSON Patch Document)</param>
        /// <response code="204">Alteração realizada com sucesso</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Update(int id, [FromBody] JsonPatchDocument<PatchSinalizacaoDto> patchSinalizacao)
        {
            if (patchSinalizacao == null)
            {
                return BadRequest();
            }

            SinalizacaoModeloNegocio sinalizacaoModeloNegocio = _negocio.Get(id);
            PatchSinalizacaoDto patchSinalizacaoDto = _mapper.Map<PatchSinalizacaoDto>(sinalizacaoModeloNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchSinalizacao.ApplyTo(patchSinalizacaoDto, ModelState);

            _mapper.Map(patchSinalizacaoDto, sinalizacaoModeloNegocio);
            _negocio.Update(id, sinalizacaoModeloNegocio);
            return NoContent();
        }

        /// <summary>
        /// Exclusão de sinalizações
        /// </summary>
        /// <param name="id">Identificador da sinalização</param>
        /// <response code="204">Exclusão realizada com sucesso</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Erro inesperado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Delete(int id)
        {
            _negocio.Delete(id);
            return NoContent();
        }
    }
}
