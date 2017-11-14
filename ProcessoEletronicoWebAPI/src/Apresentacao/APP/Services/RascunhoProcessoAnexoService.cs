using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services
{
    public class RascunhoProcessoAnexoService: IRascunhoProcessoAnexoService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IAnexoNegocio _anexoNegocio;

        public RascunhoProcessoAnexoService(IMapper mapper, ICurrentUserProvider user, IAnexoNegocio anexoNegocio)
        {
            _mapper = mapper;
            _user = user;
            _anexoNegocio = anexoNegocio;
        }

        public IEnumerable<AnexoViewModel> GetAnexos(int idRascunho)
        {
            return _mapper.Map<List<AnexoViewModel>>(_anexoNegocio.Get(idRascunho));
        }

        public AnexoViewModel GetAnexo(int idRascunho, int idAnexo)
        {
            return _mapper.Map<AnexoViewModel>(_anexoNegocio.Get(idRascunho, idAnexo));
        }

        public AnexoViewModel PostAnexo(int idRascunho, AnexoViewModel anexo)
        {
            return _mapper.Map<AnexoViewModel>(_anexoNegocio.Post(idRascunho, _mapper.Map<AnexoModeloNegocio>(anexo)));
        }

        public void EditarAnexo(int idRascunho, AnexoViewModel anexo)
        {
            _anexoNegocio.Patch(idRascunho, anexo.Id, _mapper.Map<AnexoModeloNegocio>(anexo));
        }

        public void DeleteAnexo(int idRascunho, int idAnexo)
        {
            _anexoNegocio.Delete(idRascunho, idAnexo);
        }
    }
}
