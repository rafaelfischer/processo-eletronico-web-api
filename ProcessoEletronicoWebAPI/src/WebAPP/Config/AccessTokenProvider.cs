using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.WebAPP.Config;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPP.Config
{
    public class AccessTokenProvider : IClientAccessTokenProvider
    {
        private readonly string _clientId;
        private readonly string _secret;
        private readonly IOptions<AutenticacaoIdentityServer> _autenticacaoIdentityServerConfig;
        private string _accessToken;        
        
        public AccessTokenProvider(IHttpContextAccessor httpContextAccessor, IOptions<AutenticacaoIdentityServer> autenticacaoIdentityServerConfig)
        {
            FillToken(httpContextAccessor.HttpContext.User);

            _clientId = Environment.GetEnvironmentVariable("ProcessoEletronicoApiClientId");
            _secret = Environment.GetEnvironmentVariable("ProcessoEletronicoApiSecret");
            _autenticacaoIdentityServerConfig = autenticacaoIdentityServerConfig;            
        }

        public string AccessToken
        {
            get
            {
                return _accessToken;
            }
        }

        private void FillToken(ClaimsPrincipal user)
        {
            Claim accessToken = user.FindFirst("access_token");
            _accessToken = accessToken.Value;
        }

        private async Task<TokenResponse> GetOrganogramaAccessTokenAsync()
        {
            AutenticacaoIdentityServer autenticacaoIdentityServer = _autenticacaoIdentityServerConfig.Value;

            TokenClient tokenClient = new TokenClient(autenticacaoIdentityServer.Authority + "/connect/token", _clientId, _secret);

            return await tokenClient.RequestClientCredentialsAsync("ApiOrganograma");
        }
    }
}
