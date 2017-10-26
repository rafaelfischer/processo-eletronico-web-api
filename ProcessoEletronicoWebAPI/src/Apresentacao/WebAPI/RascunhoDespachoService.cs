using Apresentacao.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Apresentacao.WebAPI.Models;
using Negocio.RascunhosDespacho.Base;
using AutoMapper;
using Negocio.RascunhosDespacho.Models;
using Microsoft.AspNetCore.JsonPatch;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;

namespace Apresentacao.WebAPI
{
    public class RascunhoDespachoService : IRascunhoDespachoService
    {
        private IRascunhoDespachoCore _core;
        private IMapper _mapper;

        public RascunhoDespachoService(IRascunhoDespachoCore core, IMapper mapper)
        {
            _core = core;
            _mapper = mapper;
        }

        public GetRascunhoDespachoDto Add(PostRascunhoDespachoDto postRascunhoDespachoDto)
        {
            RascunhoDespachoModel rascunhoDespachoModel = _core.Add(_mapper.Map<RascunhoDespachoModel>(postRascunhoDespachoDto));
            return _mapper.Map<GetRascunhoDespachoDto>(rascunhoDespachoModel);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Patch(int id, JsonPatchDocument<PatchRascunhoDespachoDto> jsonPatchRascunhoDespachoDto)
        {
            RascunhoDespachoModel rascunhoDespachoModel = _core.Search(id);
            if (rascunhoDespachoModel == null)
            {
                throw new RecursoNaoEncontradoException("Rascunho de Despacho não encontrado");
            }

            PatchRascunhoDespachoDto patchRascunhoDespachoDto = _mapper.Map<PatchRascunhoDespachoDto>(rascunhoDespachoModel);
            jsonPatchRascunhoDespachoDto.ApplyTo(patchRascunhoDespachoDto);
            _mapper.Map(patchRascunhoDespachoDto, rascunhoDespachoModel);

            _core.Update(id, rascunhoDespachoModel);
        }

        public GetRascunhoDespachoDto Search(int id)
        {
            return _mapper.Map<GetRascunhoDespachoDto>(_core.Search(id));
        }

        public IEnumerable<GetRascunhoDespachoDto> SearchByOrganizacao()
        {
            return _mapper.Map<IEnumerable<GetRascunhoDespachoDto>>(_core.SearchByOrganizacao());
        }

        public IEnumerable<GetRascunhoDespachoDto> SearchByUsuario()
        {
            return _mapper.Map<IEnumerable<GetRascunhoDespachoDto>>(_core.SearchByUsuario());
        }
    }
}
