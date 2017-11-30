using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;

namespace Apresentacao.APP.WorkServices
{
    public class ProcessoAnexoService : MensagemService, IProcessoAnexoService
    {
        private IAnexoNegocio _negocio;
        private IMapper _mapper;

        public ProcessoAnexoService(IAnexoNegocio negocio, IMapper mapper)
        {
            _negocio = negocio;
            _mapper = mapper;
        }

        public AnexoViewModel Search(int id)
        {
            AnexoViewModel anexoViewModel = _mapper.Map<AnexoViewModel>(_negocio.Pesquisar(id));
            return anexoViewModel;
        }
    }
}
