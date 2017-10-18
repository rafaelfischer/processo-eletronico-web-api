using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Prodest.ProcessoEletronico.Integration.Common;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using Prodest.ProcessoEletronico.Integration.Common.Base;

namespace Infraestrutura.Integrations.Organograma
{
    public class UnidadeService : IUnidadeService
    {
        private readonly string UrlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");
        private IApiHandler _apiHandler;

        public UnidadeService(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }
        
        public ApiCallResponse<Unidade> Search(Guid guid)
        {
            ApiCallResponse<Unidade> unidade = _apiHandler.DownloadJsonDataFromApi<Unidade>($"{UrlApiOrganograma}/unidades/{guid}");
            return unidade;
        }

        public ApiCallResponse<IEnumerable<Unidade>> SearchByOrganizacao(Guid guidOrganizacao)
        {
            ApiCallResponse<IEnumerable<Unidade>> unidades = _apiHandler.DownloadJsonDataFromApi<IEnumerable<Unidade>>($"{UrlApiOrganograma}/unidades/organizacao/{guidOrganizacao}");
            return unidades;
        }
    }
}
