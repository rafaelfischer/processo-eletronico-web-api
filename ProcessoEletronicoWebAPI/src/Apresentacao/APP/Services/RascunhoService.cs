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
        public IEnumerable<RascunhoProcessoViewModel> GetRascunhosProcessoPorOrganizacao()
        {
            try
            {
                IEnumerable<RascunhoProcessoModeloNegocio> rascunhos = _rascunhoService.Get(_user.UserGuidOrganizacao);
                IEnumerable<RascunhoProcessoViewModel> getRascunhosViewModel = _mapper.Map<List<RascunhoProcessoViewModel>>(rascunhos);

                return getRascunhosViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetRascunhoProcessoViewModel GetRascunhoProcesso(int id)
        {
            try
            {
                RascunhoProcessoModeloNegocio rascunhos = _rascunhoService.Get(id);
                GetRascunhoProcessoViewModel getRascunhosViewModel = _mapper.Map<GetRascunhoProcessoViewModel>(rascunhos);

                return getRascunhosViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }        

        public void DeleteRascunhoProcesso(int id)
        {
            try
            {
                _rascunhoService.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public RascunhoProcessoViewModel EditRascunhoProcesso(int? id)
        {            
            RascunhoProcessoViewModel RascunhoProcesso = new RascunhoProcessoViewModel();

            if (id.HasValue) { 
                RascunhoProcessoModeloNegocio rascunho = _rascunhoService.Get(id.Value);
                RascunhoProcesso = _mapper.Map<RascunhoProcessoViewModel>(rascunho);
            }
            else
            {
                RascunhoProcesso.Id = _rascunhoService.Post(new RascunhoProcessoModeloNegocio { GuidOrganizacao = _user.UserGuidOrganizacao.ToString() }).Id;
            }

            IEnumerable<AtividadeModeloNegocio> atividades = _atividadeService.Pesquisar();            
            RascunhoProcesso.AtividadesLista = _mapper.Map<List<AtividadeViewModel>>(atividades);

            IEnumerable<Unidade> unidades = _unidadeService.SearchByOrganizacao(_user.UserGuidOrganizacao).ResponseObject;
            RascunhoProcesso.UnidadesLista = _mapper.Map<List<UnidadeViewModel>>(unidades);

            IEnumerable<SinalizacaoModeloNegocio> sinalizacoes = _sinalizacaoNegocio.Pesquisar(_user.UserGuidOrganizacaoPatriarca.ToString());
            RascunhoProcesso.SinalizacoesLista = _mapper.Map<List<SinalizacaoViewModel>>(sinalizacoes);

            Organizacao organizacao = _organizacaoService.Search(_user.UserGuidOrganizacao).ResponseObject;            
            RascunhoProcesso.OrganizacaoProcesso = _mapper.Map<OrganizacaoViewModel>(organizacao);

            RascunhoProcesso.NomeUsuarioAutuador = _user.UserNome;            

            RascunhoProcesso.UfLista = new UfViewModel().GetUFs();            

            return RascunhoProcesso;
        }        

        public void UpdateRascunhoProcesso(int id, RascunhoProcessoViewModel rascunhoProcessoViewModel)
        {   
            try
            {    
                RascunhoProcessoModeloNegocio rascunhoProcessoNegocio = _mapper.Map<RascunhoProcessoModeloNegocio>(rascunhoProcessoViewModel);
                _rascunhoService.Patch(id, rascunhoProcessoNegocio);
            }
            catch (MemberAccessException)
            {
                throw;
            }            
        }
        
    }    
}
