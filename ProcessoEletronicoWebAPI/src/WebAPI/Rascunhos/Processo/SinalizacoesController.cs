using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using ProcessoEletronicoService.WebAPI.Sinalizacoes.Modelos;
using System.Collections.Generic;
using WebAPI.Config;

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

        /// <summary>
        /// Lista de sinalizações do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <returns>Sinalizações do rascunho de processos</returns>
        /// <response code="200">Retorna a lista de sinalizações</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet(Name = "GetSinalizacoes")]
        [ProducesResponseType(typeof(List<GetSinalizacaoNoImagemDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso)
        {
            return Ok(_mapper.Map<List<GetSinalizacaoNoImagemDto>>(_negocio.Get(idRascunhoProcesso)));
        }

        /// <summary>
        /// Sinalização do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="idSinalizacao">Identificador da Sinalizaçã</param>
        /// <returns>Sinalização de acordo com o identificador informado</returns>
        /// <response code="200">Sinalização de acordo com o identificador informado</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpGet("{id}", Name = "GetSinalizacaoRascunho")]
        [ProducesResponseType(typeof(GetSinalizacaoNoImagemDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Get(int idRascunhoProcesso, int idSinalizacao)
        {
            return Ok(_mapper.Map<GetSinalizacaoDto>(_negocio.Get(idRascunhoProcesso, idSinalizacao)));
        }

        /// <summary>
        /// Inserção de sinalizações no rascunho de processos
        /// </summary>
        /// <remarks>
        /// A lista de todas as sinalizações disponíveis está na consulta do recurso sinalizações (GET /api/sinalizacoes/organizacao-patriarca/{guidOrganizacaoPatriarca})
        /// </remarks>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="idsSinalizacoes">Lista de IDs de sinalizações</param>
        /// <returns>Todas as sinalizações inseridas no rascunho de processos</returns>
        /// <response code="201">Todas as sinalizações inseridas no rascunho de processos</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(List<GetSinalizacaoNoImagemDto>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Post (int idRascunhoProcesso, [FromBody] IList<int> idsSinalizacoes)
        {
            if (idsSinalizacoes == null)
            {
                return BadRequest();
            }

            IList<GetSinalizacaoDto> sinalizacoes = _mapper.Map<IList<GetSinalizacaoDto>>(_negocio.Post(idRascunhoProcesso, idsSinalizacoes));
            return CreatedAtRoute("GetSinalizacoes", sinalizacoes);
        }

        /// <summary>
        /// Atualização (substituição total) de sinalizações no rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="idsSinalizacoes">Lista de IDs de sinalizações</param>
        /// <returns>Todas as sinalizações recém inseridas no rascunho de processos</returns>
        /// <response code="201">Todas as sinalizações recém inseridas no rascunho de processos</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="422">Objeto não processável</response>
        /// <response code="500">Falha inesperada</response>
        [HttpPut]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(List<GetSinalizacaoNoImagemDto>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Put(int idRascunhoProcesso, [FromBody] IList<int> idsSinalizacoes)
        {
            if (idsSinalizacoes == null)
            {
                return BadRequest();
            }

            IList<GetSinalizacaoNoImagemDto> sinalizacoes = _mapper.Map<IList<GetSinalizacaoNoImagemDto>>(_negocio.Put(idRascunhoProcesso, idsSinalizacoes));
            return CreatedAtRoute("GetSinalizacoes", sinalizacoes);
        }

        /// <summary>
        /// Exclusão de todas sinalizações do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <response code="204">Operação feita com sucesso</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpDelete]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Delete(int idRascunhoProcesso)
        {
            _negocio.DeleteAll(idRascunhoProcesso);
            return NoContent();
        }

        /// <summary>
        /// Exclusão de uma sinalização do rascunho de processos
        /// </summary>
        /// <param name="idRascunhoProcesso">Identificador do Rascunho de processos</param>
        /// <param name="idSinalizacao">Identificador da sinalização a ser excluída</param>
        /// <response code="204">Operação feita com sucesso</response>
        /// <response code="404">Recurso não encontrado</response>
        /// <response code="500">Falha inesperada</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ApiExplorerSettings(GroupName = Constants.RascunhosDocumentationGroup)]
        public IActionResult Delete(int idRascunhoProcesso, int idSinalizacao)
        {
            _negocio.Delete(idRascunhoProcesso, idSinalizacao);
            return NoContent();
        }
    }
}
