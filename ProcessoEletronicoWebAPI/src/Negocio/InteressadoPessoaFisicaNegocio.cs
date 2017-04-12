using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio
{
    public class InteressadoPessoaFisicaNegocio : BaseNegocio, IInteressadoPessoaFisicaNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<InteressadoPessoaFisica> repositorioInteressadosPessoaFisica;
        private IContatoNegocio contatoNegocio;
        private IEmailNegocio emailNegocio;

        public InteressadoPessoaFisicaNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioInteressadosPessoaFisica = repositorios.InteressadosPessoaFisica;
            contatoNegocio = new ContatoNegocio(repositorios);
            emailNegocio = new EmailNegocio(repositorios);
        }

        public void Excluir (ICollection<InteressadoPessoaFisica> interessadosPessoaFisica)
        {
            if (interessadosPessoaFisica != null)
            {
                foreach (var interessadoPessoaFisica in interessadosPessoaFisica)
                {
                    Excluir(interessadoPessoaFisica);
                }
            }
        }

        public void Excluir(InteressadoPessoaFisica interessadoPessoaFisica)
        {
            if (interessadoPessoaFisica != null)
            {
                contatoNegocio.Excluir(interessadoPessoaFisica.Contatos);
                emailNegocio.Excluir(interessadoPessoaFisica.Emails);
                repositorioInteressadosPessoaFisica.Remove(interessadoPessoaFisica);
            }
        }
    }
}
