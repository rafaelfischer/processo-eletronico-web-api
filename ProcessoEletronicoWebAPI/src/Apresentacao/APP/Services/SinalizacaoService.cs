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

        public ICollection<SinalizacaoViewModel> Search()
        {
            ICollection<SinalizacaoViewModel> sinalizacoes = new List<SinalizacaoViewModel>();
            sinalizacoes = _mapper.Map<ICollection<SinalizacaoViewModel>>(_negocio.Get());
            return sinalizacoes;
        }

        public SinalizacaoViewModel Search(int id)
        {
            throw new NotImplementedException();
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
