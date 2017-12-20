using Apresentacao.APP.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;

namespace Apresentacao.APP.Services
{
    public class DespachoService : MensagemService, IDespachoService
    {
        private IDespachoNegocio _negocio;
        private IMapper _mapper;

        public DespachoService(IDespachoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        public ResultViewModel<GetDespachoViewModel> Search(int id)
        {
            ResultViewModel<GetDespachoViewModel> despachoResultViewModel = new ResultViewModel<GetDespachoViewModel>();

            try
            {
                despachoResultViewModel.Entidade = _mapper.Map<GetDespachoViewModel>(_negocio.PesquisarComProcesso(id));
                SetMensagemSucesso(despachoResultViewModel.Mensagens, "Consulta realizada com sucesso.");
                despachoResultViewModel.Success = true;
            }
            catch(Exception e)
            {
                SetMensagemErro(despachoResultViewModel.Mensagens, e);
                despachoResultViewModel.Success = false;
            }

            return despachoResultViewModel;
        }

        public ResultViewModel<GetDespachoViewModel> Despachar(int idProcesso, int idRascunhoDespacho)
        {
            ResultViewModel<GetDespachoViewModel> result = new ResultViewModel<GetDespachoViewModel>();

            try
            {
                result.Entidade = _mapper.Map<GetDespachoViewModel>(_negocio.DespacharPorRascunho(idProcesso, idRascunhoDespacho));
                SetMensagemSucesso(result.Mensagens, "Despacho realizado com sucesso.");                
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                SetMensagemErro(result.Mensagens, e);
            }           

            return result;
        }
    }
}
