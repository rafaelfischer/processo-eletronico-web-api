using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPP.Config
{
    public class CurrentUser : ICurrentUserProvider
    {
        private string _userCpf;
        private string _userNome;
        private string _userSistema;
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

        public string UserSistema
        {
            get
            {
                return _userSistema;
            }
        }

        public Guid UserGuidOrganizacao
        {
            get
            {
                return _userGuidOrganizacao;
                //new Guid("3ca6ea0e-ca14-46fa-a911-22e616303722");

            }
        }

        public Guid UserGuidOrganizacaoPatriarca
        {
            get
            {
                return _userGuidOrganizacaoPatriarca;
                //new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
            }
        }

        private void FillUser(ClaimsPrincipal user, IClientAccessTokenProvider clientAccessToken)
        {
            if (user != null)
            {
                Claim claimCpf = user.FindFirst("cpf");
                Claim claimNome = user.FindFirst("nome");

                Claim claimSystem = user.FindFirst("client_id");
                _userSistema = FormatClientIdValue(claimSystem.Value);

                if (claimCpf != null && claimNome != null)
                {
                    _userCpf = claimCpf.Value;
                    _userNome = claimNome.Value;

                    string accessToken = clientAccessToken.AccessToken;

                    Claim claimOrganizacao = user.FindFirst("orgao");

                    if (claimOrganizacao != null)
                    {
                        string urlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");

                        //TODO:Após o Acesso Cidadão implemtar o retorno de guids não será mais necessário as linhas que solicitam o guid do organograma
                        string siglaOrganizacao = claimOrganizacao.Value;

                        Organizacao organizacaoUsuario = DownloadJsonData<Organizacao>($"{urlApiOrganograma}organizacoes/sigla/{siglaOrganizacao}", accessToken);
                        if (!Guid.TryParse(organizacaoUsuario.guid, out _userGuidOrganizacao))
                        {
                            throw new ProcessoEletronicoException($"Não foi possível obter informações da organização do usuário (Sigla: {siglaOrganizacao})");
                        }

                        Organizacao organizacaoPatriarca = DownloadJsonData<Organizacao>($"{urlApiOrganograma}organizacoes/{_userGuidOrganizacao}/patriarca", accessToken);
                        if (!Guid.TryParse(organizacaoPatriarca.guid, out _userGuidOrganizacaoPatriarca))
                        {
                            throw new ProcessoEletronicoException($"Não foi possível obter informações da organização patriarca do usuário (Guid: {_userGuidOrganizacao})");
                        }
                    }
                }
            }
        }

        //Essa função será utilizada enquanto o token do acesso cidadão não contiver o guid do sistema
        private string FormatClientIdValue(string clientIdValue)
        {
            string processoEletronicoCliendIdDefaultValue = "processoeletronico";

            if (clientIdValue.ToLower().Contains(processoEletronicoCliendIdDefaultValue))
            {
                return processoEletronicoCliendIdDefaultValue;
            }

            return clientIdValue;
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
