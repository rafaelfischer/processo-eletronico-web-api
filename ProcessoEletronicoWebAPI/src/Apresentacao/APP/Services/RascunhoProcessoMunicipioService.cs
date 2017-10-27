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
    public class RascunhoProcessoMunicipioService: IRascunhoProcessoMunicipioService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;        
        private IMunicipioNegocio _municipioNegocio;
        

        public RascunhoProcessoMunicipioService(
            IMapper mapper,
            ICurrentUserProvider user,
            IRascunhoProcessoNegocio rascunho,
            IMunicipioNegocio municipioNegocio
            )
        {
            _mapper = mapper;
            _user = user;            
            _municipioNegocio = municipioNegocio;
        }

        public IEnumerable<MunicipioViewModel> GetMunicipiosPorIdRascunho(int idRascunho)
        {
            try
            {
                IEnumerable<MunicipioProcessoModeloNegocio> municipios = _municipioNegocio.Get(idRascunho);
                IEnumerable<MunicipioViewModel> municipiosViewModel = _mapper.Map<List<MunicipioViewModel>>(municipios);

                return municipiosViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MunicipioViewModel PostMunicipioPorIdRascunho(int idRascunho, MunicipioViewModel municipioViewModel)
        {
            try
            {   
                MunicipioProcessoModeloNegocio municipioNegocio = _mapper.Map<MunicipioProcessoModeloNegocio>(municipioViewModel);
                return _mapper.Map<MunicipioViewModel>(_municipioNegocio.Post(idRascunho, municipioNegocio));                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
