﻿using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Base
{
    public interface IRascunhoProcessoNegocio
    {
        RascunhoProcessoModeloNegocio Get(int id);

        //Método será feito quando o escopo de permissão do usuário estiver filtrado por unidade
        //List<ProcessoModeloNegocio> PesquisarProcessoNaUnidade(string guidUnidade);
        List<RascunhoProcessoModeloNegocio> Get(Guid guidOrganizacao);
        RascunhoProcessoModeloNegocio Post(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio);
        void Patch(int id, RascunhoProcessoModeloNegocio rascunhoProcessoAlterado);
        void Delete(int id);
    }
}
