using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base
{
    public interface IEmailNegocio
    {
        IList<EmailModeloNegocio> Get(int idRascunhoProcesso, int idInteressado);
        EmailModeloNegocio Get(int idRascunhoProcesso, int idInteressado, int id);
        EmailModeloNegocio Post(int idRascunhoProcesso, int idInteressado, EmailModeloNegocio emailModeloNegocio);
        void Patch(int idRascunhoProcesso, int idInteressado, int id, EmailModeloNegocio emailModeloNegocio);
        void Delete(int idRascunhoProcesso, int idInteressado, int id);
        void Delete(ICollection<EmailRascunho> emails);
        void Delete(EmailRascunho email);
    }
}
