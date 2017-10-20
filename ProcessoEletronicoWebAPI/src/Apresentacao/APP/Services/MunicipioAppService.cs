using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services
{
    public class MunicipioAppService: IMunicipioAppService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;        
        private IMunicipioService _municipioService;

        public MunicipioAppService(IMapper mapper, ICurrentUserProvider user, IMunicipioService municipioService)
        {
            _mapper = mapper;            
            _user = user;
            _municipioService = municipioService;
        }

        public IEnumerable<MunicipioViewModel> GetMunicipios(string uf)
        {
            IEnumerable<MunicipioViewModel> municipios = null;

            IEnumerable<Municipio> municipiosModel= _municipioService.SearchByEstado(uf).ResponseObject;
            municipios = _mapper.Map<List<MunicipioViewModel>>(municipiosModel);

            return municipios;
        }
    }
}
