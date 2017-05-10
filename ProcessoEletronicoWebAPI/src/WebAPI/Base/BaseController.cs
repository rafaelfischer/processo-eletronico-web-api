using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProcessoEletronicoService.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Base
{
    [Authorize]
    public class BaseController : Controller
    {
        private Dictionary<string, string> usuarioAutenticado;
        public Dictionary<string, string> UsuarioAutenticado
        {
            get
            {
                return usuarioAutenticado;
            }
        }

        public BaseController(IHttpContextAccessor httpContextAccessor, IClientAccessToken clientAccessToken)
        {
            PreencherUsuario(httpContextAccessor.HttpContext.User, clientAccessToken);
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

        private void PreencherUsuario(ClaimsPrincipal user, IClientAccessToken clientAccessToken)
        {
            if (user != null)
            {
                Claim claimCpf = user.FindFirst("cpf");
                Claim claimNome = user.FindFirst("nome");
                if (claimCpf != null && claimNome != null)
                {
                    usuarioAutenticado = new Dictionary<string, string>();

                    usuarioAutenticado.Add("cpf", claimCpf.Value);
                    usuarioAutenticado.Add("nome", claimNome.Value);

                    string accessToken = clientAccessToken.AccessToken;
                    usuarioAutenticado.Add("clientAccessToken", accessToken);

                    Claim claimOrganizacao = user.FindFirst("orgao");

                    if (claimOrganizacao != null)
                    {
                        string urlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");

                        //TODO:Após o Acesso Cidadão implemtar o retorno de guids não será mais necessário as linhas que solicitam o guid do organograma
                        string siglaOrganizacao = claimOrganizacao.Value;

                        Organizacao organizacaoUsuario = DownloadJsonData<Organizacao>(urlApiOrganograma + "organizacoes/sigla/" + siglaOrganizacao, accessToken);

                        usuarioAutenticado.Add("guidOrganizacao", organizacaoUsuario.guid);

                        Organizacao organizacaoPatriarca = DownloadJsonData<Organizacao>(urlApiOrganograma + "organizacoes/" + organizacaoUsuario.guid + "/patriarca", accessToken);

                        usuarioAutenticado.Add("guidOrganizacaoPatriarca", organizacaoPatriarca.guid);
                    }
                }
            }
        }

        class Organizacao
        {
            public string guid { get; set; }
            public string razaoSocial { get; set; }
            public string sigla { get; set; }
        }
    }
    
}
