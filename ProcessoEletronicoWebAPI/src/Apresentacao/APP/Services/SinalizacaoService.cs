using Apresentacao.APP.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Apresentacao.APP.ViewModels;
using ProcessoEletronicoService.Negocio.Sinalizacoes.Base;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;

namespace Apresentacao.APP.Services
{
    public class SinalizacaoService : ISinalizacaoService
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
        public SinalizacaoViewModel Add(SinalizacaoViewModel sinalizacaoViewModel)
        {
            SinalizacaoModeloNegocio sinalizacaoModeloNegocio = _mapper.Map<SinalizacaoModeloNegocio>(sinalizacaoViewModel);
            SinalizacaoViewModel createdSinalizacaoViewModel = _mapper.Map<SinalizacaoViewModel>(_negocio.Add(sinalizacaoModeloNegocio));
            return createdSinalizacaoViewModel;
        }

    }
}
