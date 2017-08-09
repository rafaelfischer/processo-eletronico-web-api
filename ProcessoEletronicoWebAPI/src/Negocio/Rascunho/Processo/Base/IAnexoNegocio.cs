using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Base
{
    public interface IAnexoNegocio
    {
        IList<AnexoModeloNegocio> Get(int idRascunhoProcesso);
        AnexoModeloNegocio Get(int idRascunhoProcesso, int id);
        AnexoModeloNegocio Post(int idRascunhoProcesso, AnexoModeloNegocio anexoNegocio);
        void Patch(int idRascunhoProcesso, int id, AnexoModeloNegocio anexoNegocio);
        void Delete(int idRascunhoProcesso, int id);
        void Delete(ICollection<AnexoRascunho> anexoRascunho);
        void Delete(AnexoRascunho anexoRascunho);
    }
}
