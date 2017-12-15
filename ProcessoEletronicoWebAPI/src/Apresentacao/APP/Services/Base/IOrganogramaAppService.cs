using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IOrganogramaAppService
    {
        IEnumerable<MunicipioViewModel> GetMunicipios(string uf);
        IEnumerable<OrganizacaoViewModel> GetOrganizacoesPorPatriarca();
        IEnumerable<UnidadeViewModel> GetUniadesPorOrganizacao(string guidOrganizacao);
        OrganizacaoViewModel GetOrganizacao(string guidOrganizacao);
        UnidadeViewModel GetUnidade(string guidUnidade);
    }
}
