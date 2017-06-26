using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base
{
    public interface ISinalizacaoNegocio
    {
        IList<SinalizacaoModeloNegocio> Get(int idRascunhoProcesso);
        SinalizacaoModeloNegocio Get(int idRascunhoProcesso, int idSinalizacao);
        IList<SinalizacaoModeloNegocio> Post(int idRascunhoProcesso, IList<int> idsSinalizacao);
        void Delete(int idRascunhoProcesso, int idSinalizacao);
        void Delete(ICollection<SinalizacaoRascunhoProcesso> sinalizacoesRascunhoProcesso);
        void Delete(SinalizacaoRascunhoProcesso sinalizacao);
    }
}
