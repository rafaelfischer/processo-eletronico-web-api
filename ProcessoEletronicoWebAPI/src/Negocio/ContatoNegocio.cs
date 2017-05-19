using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using System.Collections.Generic;
using ProcessoEletronicoService.Negocio.Comum;

namespace ProcessoEletronicoService.Negocio
{
    public class ContatoNegocio : BaseNegocio, IContatoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Contato> repositorioContatos;
        
        public ContatoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioContatos = repositorios.Contatos;
        }

        public void Delete (ICollection<Contato> contatos)
        {
            if (contatos != null)
            {
                foreach (var contato in contatos)
                {
                    Delete(contato);
                }
            }
        }

        public void Delete (Contato contato)
        {
            if (contato != null)
            {
                repositorioContatos.Remove(contato);
             }
        }
    }
}
