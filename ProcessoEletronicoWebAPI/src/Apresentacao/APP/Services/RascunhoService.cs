using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;

namespace Apresentacao.APP.Services
{
    public class RascunhoService : IRascunhoService
    {
        private IMapper _mapper;        
        private ICurrentUserProvider _user;
        private IRascunhoProcessoNegocio _rascunhoService;
        private IOrganizacaoService _organizacaoService;        
        private IUnidadeService _unidadeService;
        private IAtividadeNegocio _atividadeService;
        private ProcessoEletronicoService.Negocio.Base.ISinalizacaoNegocio _sinalizacaoNegocio;

        public RascunhoService(
            IMapper mapper, 
            ICurrentUserProvider user, 
            IRascunhoProcessoNegocio rascunho, 
            IOrganizacaoService organizacaoService,             
            IUnidadeService unidadeService, 
            IAtividadeNegocio atividadeNegocio,
            ProcessoEletronicoService.Negocio.Base.ISinalizacaoNegocio sinalizacaoNegocio
            )
        {
            _mapper = mapper;            
            _user = user;
            _rascunhoService = rascunho;
            _organizacaoService = organizacaoService;            
            _unidadeService = unidadeService;
            _atividadeService = atividadeNegocio;
            _sinalizacaoNegocio = sinalizacaoNegocio;
        }

        public AutuacaoInicioViewModel GetFormularioInicioAutuacao()
        {
            AutuacaoInicioViewModel formularioInicio = new AutuacaoInicioViewModel();

            IEnumerable<AtividadeModeloNegocio> atividades = _atividadeService.Pesquisar();            
            formularioInicio.Atividades = _mapper.Map<List<AtividadeViewModel>>(atividades);

            IEnumerable<Unidade> unidades = _unidadeService.SearchByOrganizacao(_user.UserGuidOrganizacao).ResponseObject;
            formularioInicio.Unidades = _mapper.Map<List<UnidadeViewModel>>(unidades);

            IEnumerable<SinalizacaoModeloNegocio> sinalizacoes = _sinalizacaoNegocio.Pesquisar(_user.UserGuidOrganizacaoPatriarca.ToString());
            formularioInicio.Sinalizacoes = _mapper.Map<List<SinalizacaoViewModel>>(sinalizacoes);

            Organizacao organizacao = _organizacaoService.Search(_user.UserGuidOrganizacao).ResponseObject;            
            formularioInicio.OrganizacaoUsuario = _mapper.Map<OrganizacaoViewModel>(organizacao);

            formularioInicio.NomeUsuario = _user.UserNome;
            formularioInicio.Cpf = _user.UserCpf;

            formularioInicio.ListaUfs = new UfViewModel().GetUFs();

            //var organizacao = _organizacaoService.Search(_user.UserGuidOrganizacao);
            //var organizacoes = _organizacaoService.SearchFilhas(_user.UserGuidOrganizacaoPatriarca);
            //var patriarca = _organizacaoService.SearchPatriarca(_user.UserGuidOrganizacaoPatriarca);
            //var organizacoSigla = _organizacaoService.Search("PRODEST");            
            //var municipio = _municipioService.Search(new Guid("7017bf17-07e0-4c40-b453-8d7c123dfc53"));
            //var unidades = _unidadeService.SearchByOrganizacao(_user.UserGuidOrganizacao);

            return formularioInicio;
        }

        public IEnumerable<GetRascunhoProcessoViewModel> GetRascunhosOrganizacao()
        {
            try
            {
                IEnumerable<RascunhoProcessoModeloNegocio> rascunhos = _rascunhoService.Get(_user.UserGuidOrganizacao);
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
