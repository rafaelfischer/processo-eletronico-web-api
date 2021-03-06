﻿using Newtonsoft.Json;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProcessoEletronicoService.Negocio.Comum
{
    public class BaseNegocio : IBaseNegocio
    {
        private readonly string UrlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");
        private Dictionary<string, string> usuario;
        private string usuarioCpf;
        private string usuarioNome;
        private string clientAccessToken;
        private Guid usuarioGuidOrganizacao;
        private Guid usuarioGuidOrganizacaoPatriarca;

        public Dictionary<string, string> Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public string UsuarioCpf
        {
            get
            {
                if (usuarioCpf == null)
                    usuarioCpf = Usuario["cpf"];

                return usuarioCpf;
            }
        }
        public string UsuarioNome
        {
            get
            {
                if (usuarioNome == null)
                    usuarioNome = Usuario["nome"];

                return usuarioNome;
            }
        }
        public string ClientAccessToken
        {
            get
            {
                if (clientAccessToken == null)
                    clientAccessToken = Usuario["clientAccessToken"];

                return clientAccessToken;
            }
        }
        public Guid UsuarioGuidOrganizacao
        {
            get
            {
                if (usuarioGuidOrganizacao.Equals(Guid.Empty))
                {
                    string stringGuidOrganizacao = Usuario["guidOrganizacao"];

                    if (stringGuidOrganizacao != null)
                        usuarioGuidOrganizacao = new Guid(stringGuidOrganizacao);
                }

                return usuarioGuidOrganizacao;
            }
        }
        public Guid UsuarioGuidOrganizacaoPatriarca
        {
            get
            {
                if (usuarioGuidOrganizacaoPatriarca.Equals(Guid.Empty))
                {
                    string stringGuidOrganizacaoPatriarca = Usuario["guidOrganizacaoPatriarca"];

                    if (stringGuidOrganizacaoPatriarca != null)
                        usuarioGuidOrganizacaoPatriarca = new Guid(stringGuidOrganizacaoPatriarca);
                }

                return usuarioGuidOrganizacaoPatriarca;
            }
        }



        private T DownloadJsonData<T>(string url) where T : new()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(ClientAccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ClientAccessToken);
                }
                var result = client.GetAsync(url).Result;



                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return default(T) ;
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
