using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface ITipoDocumentalNegocio : IBaseNegocio
    {
        TipoDocumentalModeloNegocio Pesquisar(int id);
        List<TipoDocumentalModeloNegocio> PesquisarPorAtividade(int idAtividade);
        TipoDocumentalModeloNegocio Inserir(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio);
        void Excluir(int id);


    }
}
