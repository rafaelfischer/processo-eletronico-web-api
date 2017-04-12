using ProcessoEletronicoService.Dominio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IEmailNegocio : IBaseNegocio
    {
        void Excluir(ICollection<Email> emails);
        void Excluir(Email email);
    }
}
