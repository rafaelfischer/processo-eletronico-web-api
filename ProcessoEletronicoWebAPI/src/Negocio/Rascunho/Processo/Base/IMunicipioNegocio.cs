using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Base
{
    public interface IMunicipioNegocio
    {
        IList<MunicipioProcessoModeloNegocio> Get(int idRascunhoProcesso);
        MunicipioProcessoModeloNegocio Get(int idRascunhoProcesso, int id);
        MunicipioProcessoModeloNegocio Post(int idRascunhoProcesso, MunicipioProcessoModeloNegocio MunicipioRascunhoProcessoNegocio);
        void Patch(int idRascunhoProcesso, int id, MunicipioProcessoModeloNegocio MunicipioRascunhoProcessoNegocio);
        void Delete(int idRascunhoProcesso, int id);
        void DeleteAll(int idRascunhoProcesso);
        void Delete(ICollection<MunicipioRascunhoProcesso> municipiosRascunhoProcesso);
        void Delete(MunicipioRascunhoProcesso municipiosRascunhoProcesso);
    }
}
