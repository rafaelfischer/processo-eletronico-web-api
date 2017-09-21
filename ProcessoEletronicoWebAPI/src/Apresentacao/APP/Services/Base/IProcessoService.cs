using Apresentacao.APP.ViewModels;

namespace Apresentacao.APP.WorkServices.Base
{
    public interface IProcessoService
    {
        GetProcessoViewModel GetProcessoPorNumero(string numero);
    }
}
