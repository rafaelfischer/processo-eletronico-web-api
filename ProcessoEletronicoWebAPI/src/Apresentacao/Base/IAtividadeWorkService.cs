using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IAtividadeWorkService : IBaseWorkService
    {
        AtividadeProcessoGetModelo Pesquisar(int id);
        IEnumerable<AtividadeModelo> PesquisarPorFuncao(int idFuncao);
        IEnumerable<AtividadeProcessoGetModelo> Pesquisar();
        AtividadeProcessoGetModelo Inserir(AtividadeModeloPost atividade);
        void Excluir(int id);
        
    }
}
