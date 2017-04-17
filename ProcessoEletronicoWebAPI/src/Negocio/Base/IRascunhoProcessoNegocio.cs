using Microsoft.AspNetCore.JsonPatch;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IRascunhoProcessoNegocio : IBaseNegocio
    {
        RascunhoProcessoModeloNegocio Pesquisar(int id);

        //Método será feito quando o escopo de permissão do usuário estiver filtrado por unidade
        //List<ProcessoModeloNegocio> PesquisarProcessoNaUnidade(string guidUnidade);
        List<RascunhoProcessoModeloNegocio> PesquisarRascunhosProcessoNaOrganizacao(Guid guidOrganizacao);
        RascunhoProcessoModeloNegocio Salvar(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio);
        RascunhoProcessoModeloNegocio Alterar(int id, RascunhoProcessoModeloNegocio rascunhoProcessoAlterado);
        void Excluir(int id);
    }
}
