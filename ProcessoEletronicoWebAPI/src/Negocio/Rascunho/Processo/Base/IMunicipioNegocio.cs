using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base
{
    public interface IMunicipioNegocio
    {
        IList<MunicipioRascunhoProcessoModeloNegocio> Get(int idRascunhoProcesso);
        MunicipioRascunhoProcessoModeloNegocio Get(int idRascunhoProcesso, int id);
        MunicipioRascunhoProcessoModeloNegocio Post(int idRascunhoProcesso, MunicipioRascunhoProcessoModeloNegocio MunicipioRascunhoProcessoNegocio);
        void Patch(int idRascunhoProcesso, int id, MunicipioRascunhoProcessoModeloNegocio MunicipioRascunhoProcessoNegocio);
        void Delete(int idRascunhoProcesso, int id);
        void Delete(ICollection<MunicipioRascunhoProcesso> municipiosRascunhoProcesso);
        void Delete(MunicipioRascunhoProcesso municipiosRascunhoProcesso);
    }
}
