﻿using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IPlanoClassificacaoNegocio
    {
        PlanoClassificacaoModeloNegocio Pesquisar(int id);
        List<PlanoClassificacaoModeloNegocio> Pesquisar(string guidOrganizacao);
        List<PlanoClassificacaoModeloNegocio> Pesquisar();
        PlanoClassificacaoModeloNegocio Inserir(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio);
        void Excluir(int id);
    }
}
