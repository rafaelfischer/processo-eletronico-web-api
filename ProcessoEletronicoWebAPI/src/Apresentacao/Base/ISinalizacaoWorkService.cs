using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
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
