using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;

namespace Apresentacao.APP.Services
{
    public class RascunhoService : IRascunhoService
    {
        private IMapper _mapper;        
        private ICurrentUserProvider _user;
        private IRascunhoProcessoNegocio _rascunho;

        public RascunhoService(IMapper mapper, ICurrentUserProvider user, IRascunhoProcessoNegocio rascunho)
        {
            _mapper = mapper;            
            _user = user;
            _rascunho = rascunho;
        }

        public IEnumerable<GetRascunhoProcessoViewModel> GetRascunhosOrganizacao()
        {
            try
            {
                IEnumerable<RascunhoProcessoModeloNegocio> rascunhos = _rascunho.Get(_user.UserGuidOrganizacao);
                IEnumerable<GetRascunhoProcessoViewModel> getRascunhosViewModel = _mapper.Map<List<GetRascunhoProcessoViewModel>>(rascunhos);

                return getRascunhosViewModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }    
}
