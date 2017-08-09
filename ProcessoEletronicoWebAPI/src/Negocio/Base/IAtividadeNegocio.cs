using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IAtividadeNegocio
    {
        AtividadeModeloNegocio Pesquisar(int id);
        List<AtividadeModeloNegocio> PesquisarPorFuncao(int idFuncao);
        List<AtividadeModeloNegocio> Pesquisar();
        AtividadeModeloNegocio Inserir(AtividadeModeloNegocio atividadeModeloNegocio);
        void Excluir(int id);

    }
}
