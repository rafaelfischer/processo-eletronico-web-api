using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services
{
    public class RascunhoProcessoSinalizacaoService: IRascunhoProcessoSinalizacaoService
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

        public IEnumerable<SinalizacaoViewModel> GetSinalizacoes(int idRascunho)
        {
            return _mapper.Map<List<SinalizacaoViewModel>>(_sinalizacaoNegocio.Get(idRascunho));
        }


        public void PostSinalizacao(int idRascunho, IList<SinalizacaoViewModel> sinalizacoes)
        {
            List<int> sinalizacoesListaInt = new List<int>();

            foreach(var sinalizacao in sinalizacoes)
            {
                sinalizacoesListaInt.Add(sinalizacao.Id);
            }

            _sinalizacaoNegocio.Post(idRascunho, sinalizacoesListaInt);
        }
    }
}
