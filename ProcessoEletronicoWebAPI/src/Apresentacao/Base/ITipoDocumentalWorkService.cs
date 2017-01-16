using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface ITipoDocumentalWorkService : IBaseWorkService
    {
        TipoDocumentalModeloGet Pesquisar(int id);
        List<TipoDocumentalModeloGet> PesquisarPorAtividade(int idAtividade);
        TipoDocumentalModeloGet Inserir(TipoDocumentalModeloPost tipoDocumental);
        void Excluir(int id);
        
    }
}
