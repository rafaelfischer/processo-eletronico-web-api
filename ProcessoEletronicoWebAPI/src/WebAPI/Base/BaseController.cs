﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IOptions<OrganogramaApi> organogramaApiSettings;
        private Dictionary<string, string> usuarioAutenticado;
        public Dictionary<string, string> UsuarioAutenticado {
            get
            {
                if (usuarioAutenticado == null)
                {
                    usuarioAutenticado = new Dictionary<string, string>();

                    var user = User as ClaimsPrincipal;
                    
                    usuarioAutenticado.Add("cpf", user.FindFirst("cpf").Value);
                    usuarioAutenticado.Add("nome", user.FindFirst("nome").Value);

                    string siglaOrganizacao = user.FindFirst("orgao").Value;
                    string accessToken = user.FindFirst("accessToken").Value;

                    Organizacao organizacaoUsuario = DownloadJsonData<Organizacao>(organogramaApiSettings.Value.Url + "organizacoes/" + siglaOrganizacao, accessToken);

                    usuarioAutenticado.Add("guidOrganizacao", organizacaoUsuario.guid);

                    Organizacao organizacaoPatriarca = DownloadJsonData<Organizacao>(organogramaApiSettings.Value.Url + "organizacoes/" + organizacaoUsuario.guid + "/patriarca", accessToken);

                    usuarioAutenticado.Add("guidOrganizacaoPatriarca", organizacaoPatriarca.guid);
                }

                return usuarioAutenticado;
            }
        }

        public BaseController(IOptions<OrganogramaApi> organogramaApiSettings)
        {
            this.organogramaApiSettings = organogramaApiSettings;
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

        class Organizacao
        {
            public string guid { get; set; }
            public string razaoSocial { get; set; }
            public string sigla { get; set; }
        }
    }


}
