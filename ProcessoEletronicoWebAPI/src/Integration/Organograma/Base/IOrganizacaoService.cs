using Prodest.ProcessoEletronico.Integration.Common;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.ProcessoEletronico.Integration.Organograma.Base
{
    public interface IOrganizacaoService
    {
        ApiCallResponse<Organizacao> SearchPatriarca(Guid guidOrganizacao);
        ApiCallResponse<Organizacao> Search(Guid guidOrganizacao);
        ApiCallResponse<Organizacao> Search(string siglaOrganizacao);
        ApiCallResponse<IEnumerable<Organizacao>> SearchFilhas(Guid guidOrganizacao);
    }
}
