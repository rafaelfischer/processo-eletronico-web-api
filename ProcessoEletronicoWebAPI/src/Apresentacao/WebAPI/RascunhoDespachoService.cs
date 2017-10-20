using Apresentacao.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Apresentacao.WebAPI.Models;
using Negocio.RascunhosDespacho.Base;
using AutoMapper;
using Negocio.RascunhosDespacho.Models;

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

        public void Patch(int id, PatchRascunhoDespachoDto patchRascunhoDespachoDto)
        {
            throw new NotImplementedException();
        }

        public GetRascunhoDespachoDto Search(int id)
        {
            return _mapper.Map<GetRascunhoDespachoDto>(_core.Search(id));
        }

        public IEnumerable<GetRascunhoDespachoDto> SearchByOrganizacao(string guidOrganizacao)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetRascunhoDespachoDto> SearchByUsuario(string idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
