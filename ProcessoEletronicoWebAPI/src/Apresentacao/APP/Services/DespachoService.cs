using Apresentacao.APP.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;

namespace Apresentacao.APP.Services
{
    public class DespachoService : IDespachoService
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
            despachoResultViewModel.Entidade = _mapper.Map<GetDespachoViewModel>(_negocio.PesquisarComProcesso(id));
            return despachoResultViewModel;
        }
    }
}
