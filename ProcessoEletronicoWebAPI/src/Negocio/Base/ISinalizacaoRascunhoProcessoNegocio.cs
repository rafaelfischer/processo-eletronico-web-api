using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface ISinalizacaoRascunhoProcessoNegocio
    {
        void Excluir(ICollection<SinalizacaoRascunhoProcesso> sinalizacoesRascunhoProcesso);
        void Excluir(SinalizacaoRascunhoProcesso sinalizacaoRascunhoProcesso);

    }
}
