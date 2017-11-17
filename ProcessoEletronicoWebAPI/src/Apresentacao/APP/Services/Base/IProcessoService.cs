using Apresentacao.APP.ViewModels;
using System.Collections.Generic;

namespace Apresentacao.APP.WorkServices.Base
{
    public interface IProcessoService
    {
        GetProcessoViewModel GetProcessoPorNumero(string numero);
        ICollection<GetProcessoViewModel> GetProcessosOrganizacao();
        ICollection<TipoDocumentalViewModel> GetTiposDocumentais(int idAtividade);
    }
}
