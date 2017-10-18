using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.WebAPI.Base;
using Prodest.ProcessoEletronico.Integration.Common;
using Prodest.ProcessoEletronico.Integration.Common.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
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
        private string _userSistema;
        private Guid _userGuidOrganizacao;
        private Guid _userGuidOrganizacaoPatriarca;
        private IOrganizacaoService _organizacaoService;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IClientAccessTokenProvider clientAccessToken, IOrganizacaoService organizacaoService )
        {
            _organizacaoService = organizacaoService;
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
                        //TODO:Após o Acesso Cidadão implementar o retorno de guids não será mais necessário as linhas que solicitam o guid do organograma
                        string siglaOrganizacao = claimOrganizacao.Value;
                        
                        ApiCallResponse<Organizacao> organizacaoUsuario = _organizacaoService.Search(siglaOrganizacao);
                        if (organizacaoUsuario.ResponseObject == null)
                        {
                            throw new ProcessoEletronicoException($"Não foi possível obter informações da organização do usuário (Sigla: {siglaOrganizacao})");
                        }
                        else
                        {
                            _userGuidOrganizacao = Guid.Parse(organizacaoUsuario.ResponseObject.Guid);
                        }

                        ApiCallResponse<Organizacao> organizacaoPatriarca = _organizacaoService.SearchPatriarca(_userGuidOrganizacao);
                        if (organizacaoPatriarca.ResponseObject == null)
                        {
                            throw new ProcessoEletronicoException($"Não foi possível obter informações da organização patriarca do usuário (Guid: {_userGuidOrganizacao})");
                        }
                        else
                        {
                            _userGuidOrganizacaoPatriarca = Guid.Parse(organizacaoPatriarca.ResponseObject.Guid);
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
    }
}
