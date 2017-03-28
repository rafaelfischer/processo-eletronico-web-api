using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IRascunhoProcessoWorkService : IBaseWorkService
    {
        RascunhoProcessoCompletoModelo Pesquisar(int id);
        List<RascunhoProcessoModelo> PesquisarRascunhosProcessoNaOrganizacao(string guidOrganizacao);
        RascunhoProcessoCompletoModelo Salvar(RascunhoProcessoModeloPost rascunhoProcessoPost);
        RascunhoProcessoCompletoModelo Alterar(RascunhoProcessoModeloPatch rascunhoProcessoPatch);
        void Excluir(int id);
        
        //List<RascunhoProcessoModelo> PesquisarProcessosNaUnidade(string guidUnidade);

    }
}
