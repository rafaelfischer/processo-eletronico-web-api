using Apresentacao.APP.ViewModels;
using System.Collections.Generic;

namespace Apresentacao.APP.Services.Base
{
    public interface ISinalizacaoService
    {
        ICollection<SinalizacaoViewModel> Search();
        SinalizacaoViewModel Search(int id);
        ResultViewModel<SinalizacaoViewModel> Add(SinalizacaoViewModel sinalizacaoViewModel);
        ICollection<MensagemViewModel> Delete(int id);

    }
}
