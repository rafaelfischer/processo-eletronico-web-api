using Apresentacao.APP.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Apresentacao.APP.ViewModels;
using ProcessoEletronicoService.Negocio.Sinalizacoes.Base;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;

namespace Apresentacao.APP.Services
{
    public class SinalizacaoService : MensagemService, ISinalizacaoService
    {
        private ISinalizacaoNegocio _negocio;
        private IMapper _mapper;

        public SinalizacaoService(ISinalizacaoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        public ResultViewModel<ICollection<SinalizacaoViewModel>> Search()
        {
            ResultViewModel<ICollection<SinalizacaoViewModel>> resultSinalizacoes = new ResultViewModel<ICollection<SinalizacaoViewModel>>();
            resultSinalizacoes.Entidade = _mapper.Map<ICollection<SinalizacaoViewModel>>(_negocio.Get());
            return resultSinalizacoes;
        }

        public ResultViewModel<SinalizacaoViewModel> Search(int id)
        {
            ResultViewModel<SinalizacaoViewModel> resultSinalizacaoViewModel = new ResultViewModel<SinalizacaoViewModel>();

            try
            {
                resultSinalizacaoViewModel.Entidade = _mapper.Map<SinalizacaoViewModel>(_negocio.Get(id));
            }
            catch (RecursoNaoEncontradoException e)
            {
                SetMensagemErro(resultSinalizacaoViewModel.Mensagens, e);
            }

            return resultSinalizacaoViewModel;
        }

        public ResultViewModel<SinalizacaoViewModel> Add(SinalizacaoViewModel sinalizacaoViewModel)
        {
            SinalizacaoModeloNegocio sinalizacaoModeloNegocio = _mapper.Map<SinalizacaoModeloNegocio>(sinalizacaoViewModel);
            ResultViewModel<SinalizacaoViewModel> resultSinalizacaoViewModel = new ResultViewModel<SinalizacaoViewModel>();
            
            try
            {
                resultSinalizacaoViewModel.Entidade = _mapper.Map<SinalizacaoViewModel>(_negocio.Add(sinalizacaoModeloNegocio));
                SetMensagemSucesso(resultSinalizacaoViewModel.Mensagens, "Operação realizada com sucesso");

            }
            catch (RequisicaoInvalidaException e)
            {
                SetMensagemErro(resultSinalizacaoViewModel.Mensagens, e);
                
            }

            return resultSinalizacaoViewModel;
        }

        public ResultViewModel<SinalizacaoViewModel> Update(SinalizacaoViewModel sinalizacaoViewModel)
        {
            ResultViewModel<SinalizacaoViewModel> resultSinalizacaoViewModel = new ResultViewModel<SinalizacaoViewModel>();
            
            try
            {
                _negocio.Update(sinalizacaoViewModel.Id, _mapper.Map<SinalizacaoModeloNegocio>(sinalizacaoViewModel));
                resultSinalizacaoViewModel.Entidade = _mapper.Map<SinalizacaoViewModel>(_negocio.Get(sinalizacaoViewModel.Id));
                SetMensagemSucesso(resultSinalizacaoViewModel.Mensagens, "Operação realizada com sucesso");

            }
            catch (Exception e)
            {
                SetMensagemErro(resultSinalizacaoViewModel.Mensagens, e);
            }

            return resultSinalizacaoViewModel;
        }

        public ICollection<MensagemViewModel> Delete(int id)
        {
            ICollection<MensagemViewModel> mensagens = new List<MensagemViewModel>();
           
            try
            {
                _negocio.Delete(id);
                SetMensagemSucesso(mensagens, "Exclusão realizada com sucesso");
            }
            catch (RequisicaoInvalidaException e)
            {
                SetMensagemErro(mensagens, e);
            }

            return mensagens;
        }

 
    }
}
