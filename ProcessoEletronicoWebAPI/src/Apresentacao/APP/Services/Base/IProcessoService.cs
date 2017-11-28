using Apresentacao.APP.ViewModels;
using System.Collections.Generic;

namespace Apresentacao.APP.WorkServices.Base
{
    public interface IProcessoService
    {
        ResultViewModel<GetProcessoViewModel> Search(int id);
        GetProcessoBasicoViewModel GetProcessoPorNumero(string numero);
        ICollection<GetProcessoBasicoViewModel> GetProcessosOrganizacao();
        ICollection<TipoDocumentalViewModel> GetTiposDocumentais(int idAtividade);
        ResultViewModel<GetProcessoViewModel> AutuarPorIdRascunho(int idRascunho);
    }
}
