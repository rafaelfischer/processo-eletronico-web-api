using Apresentacao.APP.Services.Base;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;

namespace Apresentacao.APP.Services
{
    public class RascunhoProcessoInteressadoService: IRascunhoProcessoInteressadoService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IInteressadoPessoaJuridicaNegocio _interessadoPessoaJuridica;
        private IInteressadoPessoaFisicaNegocio _interessadoPessoaFisica;
        private IEmailNegocio _emailNegocio;
        private IContatoNegocio _contatoNegocio;

        public RascunhoProcessoInteressadoService(
            IMapper mapper, 
            ICurrentUserProvider user,
            IInteressadoPessoaJuridicaNegocio interessadoPessoaJuridica,
            IInteressadoPessoaFisicaNegocio interessadoPessoaFisica,
            IEmailNegocio emailNegocio,
            IContatoNegocio contatoNegocio
            )
        {
            _mapper = mapper;
            _user = user;
            _interessadoPessoaJuridica = interessadoPessoaJuridica;
            _interessadoPessoaFisica = interessadoPessoaFisica;
            _emailNegocio = emailNegocio;
            _contatoNegocio = contatoNegocio;
        }
    }
}
