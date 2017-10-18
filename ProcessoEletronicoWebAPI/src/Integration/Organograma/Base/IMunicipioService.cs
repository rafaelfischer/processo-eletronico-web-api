using Prodest.ProcessoEletronico.Integration.Common;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.ProcessoEletronico.Integration.Organograma.Base
{
    public interface IMunicipioService
    {
        ApiCallResponse<Municipio> Search(Guid guid);
        ApiCallResponse<IEnumerable<Municipio>> SearchByEstado(string uf);
    }
}
