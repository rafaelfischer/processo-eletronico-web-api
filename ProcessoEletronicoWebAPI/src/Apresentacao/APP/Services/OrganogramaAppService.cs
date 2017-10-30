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
    public class OrganogramaAppService: IOrganogramaAppService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;        
        private IMunicipioService _municipioService;
        private IOrganizacaoService _organizacaoService;
        private IUnidadeService _unidadeService;

        public OrganogramaAppService(
            IMapper mapper, 
            ICurrentUserProvider user, 
            IMunicipioService municipioService, 
            IOrganizacaoService organizacaoService,
            IUnidadeService unidadeService)
        {
            _mapper = mapper;            
            _user = user;
            _municipioService = municipioService;
            _organizacaoService = organizacaoService;
            _unidadeService = unidadeService;
        }

        public IEnumerable<MunicipioViewModel> GetMunicipios(string uf)
        {
            IEnumerable<MunicipioViewModel> municipios = null;

            IEnumerable<Municipio> municipiosModel= _municipioService.SearchByEstado(uf).ResponseObject;
            municipios = _mapper.Map<List<MunicipioViewModel>>(municipiosModel);

            return municipios;
        }

        public IEnumerable<OrganizacaoViewModel> GetOrganizacoesPorPatriarca()
        {
            IEnumerable<OrganizacaoViewModel> organizacoes = null;

            IEnumerable<Organizacao> organizacoesModel = _organizacaoService.SearchFilhas(_user.UserGuidOrganizacaoPatriarca).ResponseObject;
            organizacoes = _mapper.Map<List<OrganizacaoViewModel>>(organizacoesModel);

            return organizacoes;
        }

        public IEnumerable<UnidadeViewModel> GetUniadesPorOrganizacao(string guidOrganizacao)
        {
            IEnumerable<UnidadeViewModel> unidades = null;

            IEnumerable<Unidade> unidadeViewModel = _unidadeService.SearchByOrganizacao(new Guid(guidOrganizacao)).ResponseObject;
            unidades = _mapper.Map<List<UnidadeViewModel>>(unidadeViewModel);

            return unidades;
        }
    }
}
