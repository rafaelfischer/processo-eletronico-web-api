using AutoMapper;
using Apresentacao.APP.WorkServices.Base;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using Apresentacao.APP.ViewModels;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;

namespace Apresentacao.APP.WorkServices
{
    public class ProcessoService : IProcessoService
    {
        private IMapper _mapper;
        private IProcessoNegocio _negocio;

        public ProcessoService(IMapper mapper, IProcessoNegocio negocio)
        {
            _mapper = mapper;
            _negocio = negocio;
        }

        public GetProcessoViewModel GetProcessoPorNumero(string numero)
        {
            try
            {
                ProcessoModeloNegocio processoModeloNegocio = _negocio.Pesquisar(numero);
                GetProcessoViewModel getProcessoViewModel = _mapper.Map<GetProcessoViewModel>(processoModeloNegocio);
                return getProcessoViewModel;
            }
            catch (Exception e)
            {
                return null;
            }
        
        }
    }
}
