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
    public class AnexoRascunhoDespachoService : IAnexoRascunhoDespachoService
    {
        private IAnexoRascunhoDespachoCore _core;
        private IMapper _mapper;

        public AnexoRascunhoDespachoService(IAnexoRascunhoDespachoCore core, IMapper mapper)
        {
            _core = core;
            _mapper = mapper;
        }

        public GetRascunhoAnexoDto Add(int idRascunhoDespacho, PostRascunhoAnexoDto postRascunhoAnexoDto)
        {
            AnexoRascunhoDespachoModel anexoRascunhoDespachoModel = _core.Add(idRascunhoDespacho, _mapper.Map<AnexoRascunhoDespachoModel>(postRascunhoAnexoDto));
            return _mapper.Map<GetRascunhoAnexoDto>(anexoRascunhoDespachoModel);
        }

        public void Delete(int idRascunhoDespacho, int id)
        {
            _core.Delete(idRascunhoDespacho, id);
        }

        public GetRascunhoAnexoDto Search(int idRascunhoDespacho, int id)
        {
            return _mapper.Map<GetRascunhoAnexoDto>(_core.Search(idRascunhoDespacho, id));
        }

        public IEnumerable<GetRascunhoAnexoDto> Search(int idRascunhoDespacho)
        {
            return _mapper.Map<IEnumerable<GetRascunhoAnexoDto>>(_core.Search(idRascunhoDespacho));
        }

        public void Update(int idRascunhoDespacho, int id, PatchRascunhoAnexoDto patchRascunhoAnexoDto)
        {
            throw new NotImplementedException();
        }
    }
}
