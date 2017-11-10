using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;

namespace Apresentacao.APP.Services
{
    public class RascunhoProcessoContato : IRascunhoProcessoContato
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private ITipoContatoNegocio _tipoContato;        

        public RascunhoProcessoContato(
            IMapper mapper,
            ICurrentUserProvider user,
            ITipoContatoNegocio tipoContato            
            )
        {
            _mapper = mapper;
            _user = user;
            _tipoContato = tipoContato;            
        }

        public List<TipoContatoViewModel> GetTiposContato()
        {
            try
            {
                return _mapper.Map<List<TipoContatoViewModel>>(_tipoContato.Listar());
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
