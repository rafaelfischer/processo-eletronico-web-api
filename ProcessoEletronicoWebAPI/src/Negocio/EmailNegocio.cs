using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Validacao;
using ProcessoEletronicoService.Negocio.Comum;

namespace ProcessoEletronicoService.Negocio
{
    public class EmailNegocio : BaseNegocio, IEmailNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Email> repositorioEmails;
        
        public EmailNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioEmails = repositorios.Emails;
        }

        public void Delete(Email email)
        {
            if (email != null)
            {
                repositorioEmails.Remove(email);
            }
        }

        public void Delete(ICollection<Email> emails)
        {
            if (emails != null)
            {
                foreach (var email in emails)
                {
                    Delete(email);
                }
            }
        }
    }
}
