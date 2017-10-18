using Prodest.ProcessoEletronico.Integration.Common;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.ProcessoEletronico.Integration.Organograma.Base
{
    public interface IUnidadeService
    {
        ApiCallResponse<Unidade> Search(Guid guid);
        ApiCallResponse<IEnumerable<Unidade>> SearchByOrganizacao(Guid guidOrganizacao);
    }
}
