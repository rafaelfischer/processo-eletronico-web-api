using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Prodest.ProcessoEletronico.Integration.Common;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using Prodest.ProcessoEletronico.Integration.Common.Base;

namespace Infraestrutura.Integrations.Organograma
{
    public class MunicipioService : IMunicipioService
    {
        private readonly string UrlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");
        private IApiHandler _apiHandler;

        public MunicipioService(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public ApiCallResponse<Municipio> Search(Guid guid)
        {
            ApiCallResponse<Municipio> municipio = _apiHandler.DownloadJsonDataFromApi<Municipio>($"{UrlApiOrganograma}/municipios/{guid}");
            return municipio;
        }

        public ApiCallResponse<IEnumerable<Municipio>> SearchByEstado(string uf)
        {
            ApiCallResponse<IEnumerable<Municipio>> municipios = _apiHandler.DownloadJsonDataFromApi<IEnumerable<Municipio>>($"{UrlApiOrganograma}/municipios?uf={uf}");
            return municipios;
        }
    }
}
