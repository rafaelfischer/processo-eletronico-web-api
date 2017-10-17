using Newtonsoft.Json;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using Prodest.ProcessoEletronico.Integration.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Comum.Validacao
{
    public class OrganogramaValidacao
    {
        private readonly string UrlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");
        private IClientAccessTokenProvider _accessTokenProvider;

        public OrganogramaValidacao(IClientAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
        }

        private T DownloadJsonData<T>(string url) where T : new()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(_accessTokenProvider.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessTokenProvider.AccessToken);
                }
                var result = client.GetAsync(url).Result;



                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new ProcessoEletronicoException("Não foi possível obter informações do Organograma. Tente novamente mais tarde");
                }
            }

        }

        public OrganizacaoOrganogramaModelo PesquisarOrganizacaoPatriarca(Guid guidOrganizacao)
        {
            OrganizacaoOrganogramaModelo organizacao = DownloadJsonData<OrganizacaoOrganogramaModelo>(UrlApiOrganograma + "organizacoes/" + guidOrganizacao.ToString("D") + "/patriarca");
            return organizacao;
        }

        public OrganizacaoOrganogramaModelo PesquisarOrganizacao(Guid guidOrganizacao)
        {
            OrganizacaoOrganogramaModelo organizacao = DownloadJsonData<OrganizacaoOrganogramaModelo>(UrlApiOrganograma + "organizacoes/" + guidOrganizacao.ToString("D"));
            return organizacao;
        }

        public UnidadeOrganogramaModelo PesquisarUnidade(Guid guidUnidade)
        {
            UnidadeOrganogramaModelo unidade = DownloadJsonData<UnidadeOrganogramaModelo>(UrlApiOrganograma + "unidades/" + guidUnidade.ToString("D"));
            return unidade;
        }

        public MunicipioOrganogramaModelo PesquisarMunicipio(Guid guidMunicipio)
        {
            MunicipioOrganogramaModelo municipio = DownloadJsonData<MunicipioOrganogramaModelo>(UrlApiOrganograma + "municipios/" + guidMunicipio.ToString("D"));
            return municipio;
        }

        public class OrganizacaoOrganogramaModelo
        {
            public string guid { get; set; }
            public string razaoSocial { get; set; }
            public string sigla { get; set; }
        }

        public class UnidadeOrganogramaModelo
        {
            public string guid { get; set; }
            public string nome { get; set; }
            public string sigla { get; set; }
            public OrganizacaoOrganogramaModelo organizacao { get; set; }
        }

        public class MunicipioOrganogramaModelo
        {
            public string guid { get; set; }
            public string nome { get; set; }
            public string uf { get; set; }

        }
    }
}
