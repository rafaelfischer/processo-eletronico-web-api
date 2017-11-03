using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Base
{
    public interface ISinalizacaoNegocio
    {
        IList<SinalizacaoModeloNegocio> Get(int idRascunhoProcesso);
        SinalizacaoModeloNegocio Get(int idRascunhoProcesso, int idSinalizacao);
        IList<SinalizacaoModeloNegocio> Post(int idRascunhoProcesso, IList<int> idsSinalizacao);
        IList<SinalizacaoModeloNegocio> Put(int idRascunhoProcesso, IList<int> idsSinalizacoes);
        void Delete(int idRascunhoProcesso, int idSinalizacao);
        void DeleteAll(int idRascunhoProcesso);
        void Delete(ICollection<SinalizacaoRascunhoProcesso> sinalizacoesRascunhoProcesso);
        void Delete(SinalizacaoRascunhoProcesso sinalizacao);
    }
}
