using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IMunicipioRascunhoProcesso : IBaseNegocio
    {
        void Excluir(ICollection<MunicipioRascunhoProcesso> municipiosRascunhoProcesso);
        void Excluir(MunicipioRascunhoProcesso municipioRascunhoProcesso);
    }
}
