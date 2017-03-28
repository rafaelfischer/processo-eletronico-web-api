using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using System.Collections.Generic;

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

        public void Excluir (ICollection<Contato> contatos)
        {
            if (contatos != null)
            {
                foreach (var contato in contatos)
                {
                    Excluir(contato);
                }
            }
        }

        public void Excluir(Contato contato)
        {
            if (contato != null)
            {
                repositorioContatos.Remove(contato);
             }
        }
    }
}
