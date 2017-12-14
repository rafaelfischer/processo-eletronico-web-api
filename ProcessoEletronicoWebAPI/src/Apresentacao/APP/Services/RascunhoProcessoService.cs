using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Negocio.Modelos.Patch;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
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
    public class RascunhoProcessoService : MensagemService, IRascunhoProcessoService
    {
        private IMapper _mapper;        
        private ICurrentUserProvider _user;
        private IRascunhoProcessoNegocio _rascunhoService;
        private IOrganizacaoService _organizacaoService;        
        private IUnidadeService _unidadeService;
        private IAtividadeNegocio _atividadeService;        
        private ProcessoEletronicoService.Negocio.Base.ISinalizacaoNegocio _sinalizacaoNegocio;
        private IHttpContextAccessor _current;
        private ProcessoEletronicoService.Negocio.Rascunho.Processo.Base.ISinalizacaoNegocio _sinalizacaoNegocioService;

        public RascunhoProcessoService(
            IHttpContextAccessor current,
            IMapper mapper, 
            ICurrentUserProvider user, 
            IRascunhoProcessoNegocio rascunho, 
            IOrganizacaoService organizacaoService,             
            IUnidadeService unidadeService, 
            IAtividadeNegocio atividadeNegocio,            
            ProcessoEletronicoService.Negocio.Base.ISinalizacaoNegocio sinalizacaoNegocio,
            ProcessoEletronicoService.Negocio.Rascunho.Processo.Base.ISinalizacaoNegocio sinalizacaoNegocioService
            )
        {
            _current = current;
            _mapper = mapper;            
            _user = user;
            _rascunhoService = rascunho;
            _organizacaoService = organizacaoService;            
            _unidadeService = unidadeService;
            _atividadeService = atividadeNegocio;            
            _sinalizacaoNegocio = sinalizacaoNegocio;
            _sinalizacaoNegocioService = sinalizacaoNegocioService;
        }

        public ResultViewModel<IEnumerable<RascunhoProcessoViewModel>> GetRascunhosProcessoPorOrganizacao()
        {
            ResultViewModel<IEnumerable<RascunhoProcessoViewModel>> result = new ResultViewModel<IEnumerable<RascunhoProcessoViewModel>>();

            try
            {
                IEnumerable<RascunhoProcessoModeloNegocio> rascunhos = _rascunhoService.Get(_user.UserGuidOrganizacao);
                IEnumerable<RascunhoProcessoViewModel> getRascunhosViewModel = _mapper.Map<List<RascunhoProcessoViewModel>>(rascunhos);

                result.Entidade = getRascunhosViewModel;                
            }
            catch (Exception e)
            {
                result.Mensagens = new List<MensagemViewModel>();
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<RascunhoProcessoViewModel> GetRascunhoProcesso(int id)
        {
            ResultViewModel<RascunhoProcessoViewModel> result = new ResultViewModel<RascunhoProcessoViewModel>();

            try
            {
                RascunhoProcessoModeloNegocio rascunho = _rascunhoService.Get(id);
                RascunhoProcessoViewModel rascunhoViewModel = _mapper.Map<RascunhoProcessoViewModel>(rascunho);

                result.Entidade = rascunhoViewModel;
                SetMensagemSucesso(result.Mensagens, "Rascunho recuperado com sucesso.");                
            }
            catch (RecursoNaoEncontradoException e)
            {
                result.Mensagens = new List<MensagemViewModel>();
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }        

        public ResultViewModel<RascunhoProcessoViewModel> DeleteRascunhoProcesso(int id)
        {
            ResultViewModel<RascunhoProcessoViewModel> result = new ResultViewModel<RascunhoProcessoViewModel>();
            try
            {
                _rascunhoService.Delete(id);
                SetMensagemSucesso(result.Mensagens, "Rascunho excluído com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public RascunhoProcessoViewModel PostRascunhoProcesso(RascunhoProcessoViewModel rascunhoViewModel)
        {
            RascunhoProcessoModeloNegocio rascunhoNegocio;

            if (rascunhoViewModel != null)
            {
                rascunhoNegocio = _rascunhoService.Post(_mapper.Map<RascunhoProcessoModeloNegocio>(rascunhoViewModel));

                RascunhoProcessoViewModel rascunhoProcesso = _mapper.Map<RascunhoProcessoViewModel>(rascunhoNegocio);

                /*Preenche dados adicionais do objeto viewmodel*/
                IEnumerable<AtividadeModeloNegocio> atividades = _atividadeService.Pesquisar();
                rascunhoProcesso.AtividadesLista = _mapper.Map<List<AtividadeViewModel>>(atividades);

                IEnumerable<Unidade> unidades = _unidadeService.SearchByOrganizacao(_user.UserGuidOrganizacao).ResponseObject;
                rascunhoProcesso.UnidadesLista = _mapper.Map<List<UnidadeViewModel>>(unidades);

                IEnumerable<SinalizacaoModeloNegocio> sinalizacoes = _sinalizacaoNegocio.Pesquisar(_user.UserGuidOrganizacaoPatriarca.ToString());
                rascunhoProcesso.SinalizacoesLista = _mapper.Map<List<SinalizacaoViewModel>>(sinalizacoes);

                Organizacao organizacao = _organizacaoService.Search(_user.UserGuidOrganizacao).ResponseObject;
                rascunhoProcesso.OrganizacaoProcesso = _mapper.Map<OrganizacaoViewModel>(organizacao);

                rascunhoProcesso.NomeUsuarioAutuador = _user.UserNome;
                rascunhoProcesso.UfLista = new UfViewModel().GetUFs();

                return rascunhoProcesso;
            }
            else
            {
                rascunhoNegocio = _rascunhoService.Post(new RascunhoProcessoModeloNegocio { GuidOrganizacao = _user.UserGuidOrganizacao.ToString() });
                return _mapper.Map<RascunhoProcessoViewModel>(rascunhoNegocio);
            }            
        }

        public RascunhoProcessoViewModel EditRascunhoProcesso(int? id)
        {            
            RascunhoProcessoViewModel rascunhoProcesso = new RascunhoProcessoViewModel();

            if (id.HasValue) { 
                RascunhoProcessoModeloNegocio rascunho = _rascunhoService.Get(id.Value);
                rascunhoProcesso = _mapper.Map<RascunhoProcessoViewModel>(rascunho);
            }
            else
            {
                rascunhoProcesso.Id = PostRascunhoProcesso(null).Id;
            }

            IEnumerable<AtividadeModeloNegocio> atividades = _atividadeService.Pesquisar();
            rascunhoProcesso.AtividadesLista = _mapper.Map<List<AtividadeViewModel>>(atividades);

            IEnumerable<Unidade> unidades = _unidadeService.SearchByOrganizacao(_user.UserGuidOrganizacao).ResponseObject;
            rascunhoProcesso.UnidadesLista = _mapper.Map<List<UnidadeViewModel>>(unidades);

            IEnumerable<SinalizacaoModeloNegocio> listaSinalizacoes = _sinalizacaoNegocio.Pesquisar(_user.UserGuidOrganizacaoPatriarca.ToString());
            rascunhoProcesso.SinalizacoesLista = _mapper.Map<List<SinalizacaoViewModel>>(listaSinalizacoes);

            IEnumerable<SinalizacaoModeloNegocio> sinalizacoes = _sinalizacaoNegocioService.Get(rascunhoProcesso.Id);
            rascunhoProcesso.Sinalizacoes = _mapper.Map<List<SinalizacaoViewModel>>(sinalizacoes);

            Organizacao organizacao = _organizacaoService.Search(_user.UserGuidOrganizacao).ResponseObject;
            rascunhoProcesso.OrganizacaoProcesso = _mapper.Map<OrganizacaoViewModel>(organizacao);

            rascunhoProcesso.NomeUsuarioAutuador = _user.UserNome;

            rascunhoProcesso.UfLista = new UfViewModel().GetUFs();            

            return rascunhoProcesso;
        }

        public ResultViewModel<RascunhoProcessoViewModel> GetForEditRascunhoProcesso(int? id)
        {
            RascunhoProcessoViewModel rascunhoProcesso = new RascunhoProcessoViewModel();
            ResultViewModel<RascunhoProcessoViewModel> resultRascunhoProcessoViewModel = new ResultViewModel<RascunhoProcessoViewModel>();

            try
            {
                if (id.HasValue)
                {
                    RascunhoProcessoModeloNegocio rascunho = _rascunhoService.Get(id.Value);
                    rascunhoProcesso = _mapper.Map<RascunhoProcessoViewModel>(rascunho);
                }
                else
                {
                    rascunhoProcesso.Id = PostRascunhoProcesso(null).Id;
                }

                IEnumerable<AtividadeModeloNegocio> atividades = _atividadeService.Pesquisar();
                rascunhoProcesso.AtividadesLista = _mapper.Map<List<AtividadeViewModel>>(atividades);

                IEnumerable<Unidade> unidades = _unidadeService.SearchByOrganizacao(_user.UserGuidOrganizacao).ResponseObject;
                rascunhoProcesso.UnidadesLista = _mapper.Map<List<UnidadeViewModel>>(unidades);

                Organizacao organizacao = _organizacaoService.Search(_user.UserGuidOrganizacao).ResponseObject;
                rascunhoProcesso.OrganizacaoProcesso = _mapper.Map<OrganizacaoViewModel>(organizacao);

                rascunhoProcesso.NomeUsuarioAutuador = _user.UserNome;

                resultRascunhoProcessoViewModel.Entidade = rascunhoProcesso;
                SetMensagemSucesso(resultRascunhoProcessoViewModel.Mensagens, "Dados básicos atualizados com sucesso.");
            }
            catch (RecursoNaoEncontradoException e)
            {    
                SetMensagemErro(resultRascunhoProcessoViewModel.Mensagens, e);
            }

            return resultRascunhoProcessoViewModel;
        }

        public void UpdateRascunhoProcesso(int id, RascunhoProcessoViewModel rascunhoProcessoViewModel)
        {   
            try
            {    
                RascunhoProcessoPatchModel rascunhoProcessoPatchModel = _mapper.Map<RascunhoProcessoPatchModel>(rascunhoProcessoViewModel);
                _rascunhoService.Patch(id, rascunhoProcessoPatchModel);
            }
            catch (MemberAccessException)
            {
                throw;
            }            
        }
        
    }    
}
