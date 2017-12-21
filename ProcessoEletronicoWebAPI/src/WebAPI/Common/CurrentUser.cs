using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Common
{

    public class CurrentUser : ICurrentUserProvider
    {
        private string _userCpf;
        private string _userNome;
        private Guid _userGuidOrganizacao;
        private Guid _userGuidOrganizacaoPatriarca;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IClientAccessTokenProvider clientAccessToken)
        {
            FillUser(httpContextAccessor.HttpContext.User, clientAccessToken);
        }

        public string UserCpf
        {
            get
            {
                return _userCpf;
            }
        }

        public string UserNome
        {
            get
            {
                return _userNome;
            }
        }

        public Guid UserGuidOrganizacao
        {
            get
            {
                return _userGuidOrganizacao;
            }
        }

        public Guid UserGuidOrganizacaoPatriarca
        {
            get
            {
                return _userGuidOrganizacaoPatriarca; 
            }
        }

        private void FillUser(ClaimsPrincipal user, IClientAccessTokenProvider clientAccessToken)
        {
            if (user != null)
            {
                Claim claimCpf = user.FindFirst("cpf");
                Claim claimNome = user.FindFirst("nome");
                if (claimCpf != null && claimNome != null)
                {
                    _userCpf = claimCpf.Value;
                    _userNome = claimNome.Value;

                    string accessToken = clientAccessToken.AccessToken;

                    List<Claim> claimsOrganizacao = user.FindAll("orgao").ToList();

                    foreach (Claim claimOrganizacao in claimsOrganizacao)
                    {
                        Guid guidOrganizacao = new Guid();
                        if (Guid.TryParse(claimOrganizacao.Value, out guidOrganizacao))
                        {
                            string urlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");
                            _userGuidOrganizacao = guidOrganizacao;

                            Organizacao organizacaoPatriarca = DownloadJsonData<Organizacao>($"{urlApiOrganograma}organizacoes/{_userGuidOrganizacao}/patriarca", accessToken);
                            if (!Guid.TryParse(organizacaoPatriarca.guid, out _userGuidOrganizacaoPatriarca))
                            {
                                throw new ProcessoEletronicoException($"Não foi possível obter informações da organização patriarca do usuário (Guid: {_userGuidOrganizacao})");
                            }
                        }
                    }
                }
            }
        }

        private T DownloadJsonData<T>(string url, string acessToken) where T : new()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(acessToken))
                    client.SetBearerToken(acessToken);

                var result = client.GetAsync(url).Result;

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return new T();
                }
            }
        }

        private class Organizacao
        {
            public string guid { get; set; }
            public string razaoSocial { get; set; }
            public string sigla { get; set; }
        }
    }
}
