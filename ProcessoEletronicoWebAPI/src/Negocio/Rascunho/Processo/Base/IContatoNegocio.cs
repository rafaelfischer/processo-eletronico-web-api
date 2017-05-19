using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base
{
    public interface IContatoNegocio : IBaseNegocio
    {
        void Delete(ICollection<Contato> contatos);
        void Delete(Contato contato);
    }
}
