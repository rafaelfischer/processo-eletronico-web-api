using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base
{
    public interface IRascunhoProcessoNegocio : IBaseNegocio
    {
        RascunhoProcessoModeloNegocio Pesquisar(int id);

        //Método será feito quando o escopo de permissão do usuário estiver filtrado por unidade
        //List<ProcessoModeloNegocio> PesquisarProcessoNaUnidade(string guidUnidade);
        List<RascunhoProcessoModeloNegocio> PesquisarRascunhosProcessoNaOrganizacao(Guid guidOrganizacao);
        RascunhoProcessoModeloNegocio Salvar(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio);
        void Alterar(int id, RascunhoProcessoModeloNegocio rascunhoProcessoAlterado);
        void Excluir(int id);
    }
}
