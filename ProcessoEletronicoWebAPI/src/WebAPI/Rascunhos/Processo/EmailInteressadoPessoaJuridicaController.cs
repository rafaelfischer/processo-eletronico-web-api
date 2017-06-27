using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo
{
    [Route("api/rascunhos-processo/{idRascunhoProcesso}/interessados-pessoa-juridica/{idInteressado}/emails")]
    public class EmailInteressadoPessoaJuridicaController : BaseController
    {
        private IMapper _mapper;
        private IEmailInteressadoPessoaJuridicaNegocio _negocio;
        public EmailInteressadoPessoaJuridicaController(IMapper mapper, IEmailInteressadoPessoaJuridicaNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        [HttpGet]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado)
        {
            return Ok(_mapper.Map<IList<GetEmailDto>>(_negocio.Get(idRascunhoProcesso, idInteressado)));
        }

        [HttpGet("{id}", Name = "GetEmailInteressadoPessoaJuridica")]
        public IActionResult Get(int idRascunhoProcesso, int idInteressado, int id)
        {
            return Ok(_mapper.Map<GetEmailDto>(_negocio.Get(idRascunhoProcesso, idInteressado, id)));
        }

        [HttpPost]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        public IActionResult Post(int idRascunhoProcesso, int idInteressado, [FromBody] PostEmailDto postEmailDto)
        {
            if (postEmailDto == null)
            {
                return BadRequest();
            }

            EmailModeloNegocio emailModeloNegocio = _negocio.Post(idRascunhoProcesso, idInteressado, _mapper.Map<EmailModeloNegocio>(postEmailDto));
            GetEmailDto getEmailDto = _mapper.Map<GetEmailDto>(emailModeloNegocio);

            return CreatedAtRoute("GetEmailInteressadoPessoaJuridica", new { Id = getEmailDto.Id }, getEmailDto);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        public IActionResult Patch(int idRascunhoProcesso, int idInteressado, int id, [FromBody] JsonPatchDocument<PatchEmailDto> patchEmailDto)
        {
            EmailModeloNegocio emailModeloNegocio = _negocio.Get(idRascunhoProcesso, idInteressado, id);
            PatchEmailDto emailToPatch = _mapper.Map<PatchEmailDto>(emailModeloNegocio);

            //Validação da existência de um "path" será feita posteriormente. Por enquanto caminhos não existentes são ignorados.
            patchEmailDto.ApplyTo(emailToPatch, ModelState);

            _mapper.Map(emailToPatch, emailModeloNegocio);
            _negocio.Patch(idRascunhoProcesso, idInteressado, id, emailModeloNegocio);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RascunhoProcesso.Rascunhar")]
        public IActionResult Delete(int idRascunhoProcesso, int idInteressado, int id)
        {
            _negocio.Delete(idRascunhoProcesso, idInteressado, id);
            return NoContent();
        }
    }
}
