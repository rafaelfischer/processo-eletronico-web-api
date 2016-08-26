using ProcessoEletronicoService.Apresentacao.Restrito.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Restrito.Base
{
    public interface ISinalizacaoWorkService
    {
        List<SinalizacaoModelo> Obter();

        SinalizacaoModelo Obter(int id);

        SinalizacaoModelo Incluir(SinalizacaoModelo sinalizacao);

        void Alterar(int id, SinalizacaoModelo sinalizacao);

        void Excluir(int id);
    }
}
