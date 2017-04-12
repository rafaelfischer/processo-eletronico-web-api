using IdentityModel.Client;
using Microsoft.Extensions.Options;
using ProcessoEletronicoService.WebAPI.Base;
using System;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Config
{
    public class AcessoCidadaoClientAccessToken : IClientAccessToken
    {
        private readonly string _clientId;
        private readonly string _secret;
        private readonly IOptions<AutenticacaoIdentityServer> _autenticacaoIdentityServerConfig;
        private string _accessToke;
        private DateTime? _halfExpireTime;
        private DateTime? _expireTime;

        public AcessoCidadaoClientAccessToken(IOptions<AutenticacaoIdentityServer> autenticacaoIdentityServerConfig)
        {
            _clientId = Environment.GetEnvironmentVariable("ProcessoEletronicoApiClientId");
            _secret = Environment.GetEnvironmentVariable("ProcessoEletronicoApiSecret");
            _autenticacaoIdentityServerConfig = autenticacaoIdentityServerConfig;
        }

        public string AccessToken
        {
            get
            {
                return GetAccessTokenAsync().Result;
            }
        }

        private async Task<TokenResponse> GetOrganogramaAccessTokenAsync()
        {
            AutenticacaoIdentityServer autenticacaoIdentityServer = _autenticacaoIdentityServerConfig.Value;

            TokenClient tokenClient = new TokenClient(autenticacaoIdentityServer.Authority + "/connect/token", _clientId, _secret);

            return await tokenClient.RequestClientCredentialsAsync("ApiOrganograma");
        }

        private async Task<string> GetAccessTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(_accessToke))
            {
                //Caso o access token ainda não possua valor ele será obitido de forma síncrona.
                await SetAccessTokenAsync();
            }

            DateTime now = DateTime.Now;

            if (_halfExpireTime.HasValue && _halfExpireTime.Value <= now && _expireTime.HasValue && _expireTime.Value > now)
            {
                //Caso o access token já tenha passado da metade do tempo de expiração, mas ainda não tenha chegado no limite do tempo de expiração
                //o access token será renovado de forma assincrona de forma que o usuário não fique esperando esta operação.
                SetAccessTokenAsync();
            }
            else if (_expireTime.HasValue && _expireTime.Value <= now)
            {
                //Caso o access token já tenha atingido do tempo limite de expiração
                //o access token será renovado de forma síncrona.
                await SetAccessTokenAsync();
            }

            return _accessToke;
        }

        private async Task SetAccessTokenAsync()
        {
            TokenResponse tokenResponse = await GetOrganogramaAccessTokenAsync();

            if (tokenResponse.IsError)
                throw new Exception(tokenResponse.Error, tokenResponse.Exception);

            //Tempo de expiração subtraído de 5 minutos para tolerância de relógios dessincronizados
            _expireTime = DateTime.Now.AddSeconds((tokenResponse.ExpiresIn - (5 * 60)));
            //Metade do tempo de expiração
            _halfExpireTime = DateTime.Now.AddSeconds((tokenResponse.ExpiresIn / 2));
            //AccessToken
            _accessToke = tokenResponse.AccessToken;
        }
    }
}
