using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IEmailNegocio : IBaseNegocio
    {
        void Delete(ICollection<Email> emails);
        void Delete(Email email);
    }
}
