using Negocio.Modelos.Patch;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Base
{
    public interface IRascunhoProcessoNegocio
    {
        RascunhoProcessoModeloNegocio Get(int id);
        List<RascunhoProcessoModeloNegocio> Get(Guid guidOrganizacao);
        RascunhoProcessoModeloNegocio Post(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio);
        void Patch(int id, RascunhoProcessoModeloNegocio rascunhoProcessoNegocio);
        void Patch(int id, RascunhoProcessoPatchModel rascunhoProcessoPatchModel);
        void Delete(int id);
    }
}
