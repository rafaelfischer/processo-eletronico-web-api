using IdentityModel.Client;
using Microsoft.Extensions.Options;
using ProcessoEletronicoService.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Config
{
    public class AcessoCidadaoClientAccessToken : IClientAccessToken
    {
        private readonly IOptions<AutenticacaoIdentityServer> _autenticacaoIdentityServerConfig;

        public AcessoCidadaoClientAccessToken(IOptions<AutenticacaoIdentityServer> autenticacaoIdentityServerConfig)
        {
            _autenticacaoIdentityServerConfig = autenticacaoIdentityServerConfig;
        }

        public string AccessToken
        {
            get
            {
                TokenResponse tokenResponse = GetOrganogramaAccessTokenAsync().Result;

                if (tokenResponse.IsError)
                    throw new Exception(tokenResponse.Error, tokenResponse.Exception);

                return tokenResponse.AccessToken;
            }
        }

        private async Task<TokenResponse> GetOrganogramaAccessTokenAsync()
        {
            AutenticacaoIdentityServer autenticacaoIdentityServer = _autenticacaoIdentityServerConfig.Value;

            TokenClient tokenClient = new TokenClient(autenticacaoIdentityServer.Authority + "/connect/token", "805ac3fd-3913-4742-9f27-1fa41e7cce9d", "xg4kEtyg%I%xe5OqfsROJvcGD9tcn**C");

            return await tokenClient.RequestClientCredentialsAsync("ApiOrganograma");
        }
    }
}
