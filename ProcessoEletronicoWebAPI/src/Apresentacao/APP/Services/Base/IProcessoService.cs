using Apresentacao.APP.ViewModels;
using System.Collections.Generic;

namespace Apresentacao.APP.Services.Base
{
    public interface IProcessoService
    {
        ResultViewModel<GetProcessoViewModel> Search(int id);
        ResultViewModel<GetProcessoViewModel> GetProcessoPorNumero(string numero);
        ICollection<GetProcessoBasicoViewModel> GetProcessosOrganizacao();
        ICollection<TipoDocumentalViewModel> GetTiposDocumentais(int idAtividade);
        ResultViewModel<GetProcessoViewModel> AutuarPorIdRascunho(int idRascunho);
    }
}
