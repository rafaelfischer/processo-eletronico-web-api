using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services
{
    public class RascunhoProcessoSinalizacaoService : MensagemService, IRascunhoProcessoSinalizacaoService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private ISinalizacaoNegocio _sinalizacaoNegocio;

        public RascunhoProcessoSinalizacaoService(IMapper mapper, ICurrentUserProvider user, ISinalizacaoNegocio sinalizacaoNegocio)
        {
            _mapper = mapper;
            _user = user;
            _sinalizacaoNegocio = sinalizacaoNegocio;
        }

        public List<SinalizacaoViewModel> GetSinalizacoes(int idRascunho)
        {
            return _mapper.Map<List<SinalizacaoViewModel>>(_sinalizacaoNegocio.Get(idRascunho));
        }

        public ResultViewModel<List<SinalizacaoViewModel>> UpdateSinalizacao(int idRascunho, IList<SinalizacaoViewModel> sinalizacoes)
        {
            List<int> sinalizacoesListaInt = new List<int>();
            ResultViewModel<List<SinalizacaoViewModel>> resultSinalizacoesViewModel = new ResultViewModel<List<SinalizacaoViewModel>>();

            try
            {
                if (sinalizacoes != null && sinalizacoes.Count > 0)
                {
                    foreach (var sinalizacao in sinalizacoes)
                    {
                        sinalizacoesListaInt.Add(sinalizacao.Id);
                    }

                    List<SinalizacaoViewModel> listaSinalizacoes = _mapper.Map<List<SinalizacaoViewModel>>(_sinalizacaoNegocio.Put(idRascunho, sinalizacoesListaInt));
                    resultSinalizacoesViewModel.Entidade = listaSinalizacoes;                    
                }
                else
                {
                    DeleteAllSinalizacao(idRascunho);
                }

                SetMensagemSucesso(resultSinalizacoesViewModel.Mensagens, "Sinalizações atualizadas com sucesso.");

            }
            catch (RequisicaoInvalidaException e)
            {
                SetMensagemErro(resultSinalizacoesViewModel.Mensagens, e);
            }
            catch (Exception e)
            {
                SetMensagemErro(resultSinalizacoesViewModel.Mensagens, e);
            }

            return resultSinalizacoesViewModel;
        }

        public void DeleteAllSinalizacao(int idRascunho)
        {
            _sinalizacaoNegocio.DeleteAll(idRascunho);
        }


        public void PostSinalizacao(int idRascunho, IList<SinalizacaoViewModel> sinalizacoes)
        {
            List<int> sinalizacoesListaInt = new List<int>();

            foreach (var sinalizacao in sinalizacoes)
            {
                sinalizacoesListaInt.Add(sinalizacao.Id);
            }

            _sinalizacaoNegocio.Post(idRascunho, sinalizacoesListaInt);
        }
    }
}
