using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IDestinacaoFinalWorkService : IBaseWorkService
    {
        List<DestinacaoFinalModeloGet> Listar();
        DestinacaoFinalModeloGet Pesquisar(int id);
        DestinacaoFinalModeloGet Inserir(DestinacaoFinalModeloPost destinacaoFinalPost);
        void Excluir(int id);
    }
}
