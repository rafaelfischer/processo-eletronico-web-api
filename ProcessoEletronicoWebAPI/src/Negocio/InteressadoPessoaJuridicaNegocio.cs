using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio
{
    public class InteressadoPessoaJuridicaNegocio : BaseNegocio, IInteressadoPessoaJuridicaNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<InteressadoPessoaJuridica> repositorioInteressadosPessoaJuridica;
        private IContatoNegocio contatoNegocio;
        private IEmailNegocio emailNegocio;

        public InteressadoPessoaJuridicaNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioInteressadosPessoaJuridica = repositorios.InteressadosPessoaJuridica;
            contatoNegocio = new ContatoNegocio(repositorios);
            emailNegocio = new EmailNegocio(repositorios);
        }

        public void Excluir(ICollection<InteressadoPessoaJuridica> interessadosPessoaJuridica)
        {
            if (interessadosPessoaJuridica != null)
            {
                foreach (var interessadoPessoaJuridica in interessadosPessoaJuridica)
                {
                    Excluir(interessadoPessoaJuridica);
                }
            }
        }

        public void Excluir(InteressadoPessoaJuridica interessadoPessoaJuridica)
        {
            if (interessadoPessoaJuridica != null)
            {
                contatoNegocio.Excluir(interessadoPessoaJuridica.Contatos);
                emailNegocio.Excluir(interessadoPessoaJuridica.Emails);
                repositorioInteressadosPessoaJuridica.Remove(interessadoPessoaJuridica);
            }
        }
    }
}
