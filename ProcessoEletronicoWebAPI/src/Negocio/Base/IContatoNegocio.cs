using ProcessoEletronicoService.Dominio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IContatoNegocio : IBaseNegocio
    {
        void Excluir(ICollection<Contato> contatos);
        void Excluir(Contato contato);
    }
}
