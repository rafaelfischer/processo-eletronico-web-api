using Apresentacao.APP.ViewModels;
using System.Collections.Generic;

namespace Apresentacao.APP.Services.Base
{
    public interface ISinalizacaoService
    {
        ResultViewModel<ICollection<SinalizacaoViewModel>> Search();
        ResultViewModel<SinalizacaoViewModel> Search(int id);
        ResultViewModel<SinalizacaoViewModel> Add(SinalizacaoViewModel sinalizacaoViewModel);
        ResultViewModel<SinalizacaoViewModel> Update(SinalizacaoViewModel sinalizacaoViewModel);
        ICollection<MensagemViewModel> Delete(int id);

    }
}
