using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Prodest.ProcessoEletronico.Integration.Common;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using Prodest.ProcessoEletronico.Integration.Common.Base;

namespace Infraestrutura.Integrations.Organograma
{
    public class OrganizacaoService : IOrganizacaoService
    {
        private readonly string UrlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");
        private IApiHandler _apiHandler;

        public OrganizacaoService(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public ApiCallResponse<Organizacao> Search(Guid guidOrganizacao)
        {
            ApiCallResponse<Organizacao> organizacao = _apiHandler.DownloadJsonDataFromApi<Organizacao>($"{UrlApiOrganograma}/organizacoes/{guidOrganizacao}");
            return organizacao;
        }

        public ApiCallResponse<Organizacao> Search(string siglaOrganizacao)
        {
            ApiCallResponse<Organizacao> organizacao = _apiHandler.DownloadJsonDataFromApi<Organizacao>($"{UrlApiOrganograma}/organizacoes/sigla/{siglaOrganizacao}");
            return organizacao;
        }

        public ApiCallResponse<Organizacao> SearchPatriarca(Guid guidOrganizacao)
        {
            ApiCallResponse<Organizacao> organizacao = _apiHandler.DownloadJsonDataFromApi<Organizacao>($"{UrlApiOrganograma}/organizacoes/{guidOrganizacao}/patriarca");
            return organizacao;
        }

        public ApiCallResponse<IEnumerable<Organizacao>> SearchFilhas(Guid guidOrganizacao)
        {
            ApiCallResponse<IEnumerable<Organizacao>> organizacoes = _apiHandler.DownloadJsonDataFromApi<IEnumerable<Organizacao>>($"{UrlApiOrganograma}/organizacoes/{guidOrganizacao}/filhas");
            return organizacoes;
        }
        
    }
}
