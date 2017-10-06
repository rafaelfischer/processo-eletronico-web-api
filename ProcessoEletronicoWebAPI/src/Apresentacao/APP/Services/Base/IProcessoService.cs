using Apresentacao.APP.ViewModels;
using System.Collections.Generic;

namespace Apresentacao.APP.WorkServices.Base
{
    public interface IProcessoService
    {
        GetProcessoViewModel GetProcessoPorNumero(string numero);
        IEnumerable<GetProcessoViewModel> GetProcessosOrganizacao();
        IEnumerable<GetRascunhoProcessoViewModel> GetRascunhosOrganizacao();
    }
}
