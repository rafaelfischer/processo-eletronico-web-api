using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using WebAPP.Config;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.HttpOverrides;

namespace WebAPP
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _hostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add authentication services
            services.AddAuthentication(
                options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme
            );

            services.AddMvc();
            services.AddAutoMapper();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add the Auth0 Settings object so it can be injected
            services.Configure<Settings>(Configuration.GetSection("oidc"));

            var physicalProvider = _hostingEnvironment.ContentRootFileProvider;

            services.AddSingleton<IFileProvider>(physicalProvider);

            #region Políticas que serão concedidas
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Processo.Edit", policy => policy.RequireClaim("Acao$Processo", "Autuar"));
                options.AddPolicy("RascunhoProcesso.Edit", policy => policy.RequireClaim("Acao$RascunhoProcesso", "Rascunhar"));
                options.AddPolicy("Despacho.Edit", policy => policy.RequireClaim("Acao$Despacho", "Inserir"));
                options.AddPolicy("RascunhoDespacho.Edit", policy => policy.RequireClaim("Acao$RascunhosDespacho", "Elaborar"));
                options.AddPolicy("Sinalizacao.Edit", policy => policy.RequireClaim("Acao$Sinalizacao", "Inserir"));
            }
            );
            #endregion

            DependencyInjection.InjectDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<Settings> settings)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Inicio/Error");                
            }
            
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.Use(async (context, next) =>
                {
                    context.Request.Scheme = "https";
                    await next.Invoke();
                });
            }

            #region CONFIGURACAO AUTENTICAÇÃO ACESSO CIDADAO

            // Add the cookie middleware
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = "/Home/Login",
                AccessDeniedPath = "/Home/AcessoNegado",
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
                SlidingExpiration = true,
                LogoutPath = "/Home/Logout",
                CookieName = "processoeletronico"
            });

            // Add the OIDC middleware
            var options = new OpenIdConnectOptions("processoeletronico")
            {
                // Set the authority to your Auth0 domain
                Authority = $"https://{settings.Value.Domain}",

                AuthenticationScheme = "oidc",

                // Configure the Auth0 Client ID and Client Secret
                ClientId = Environment.GetEnvironmentVariable("ProcessoEletronicoAppClientId"),
                ClientSecret = Environment.GetEnvironmentVariable("ProcessoEletronicoAppSecret"),

                // Do not automatically authenticate and challenge
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,

                // Set response type to code
                ResponseType = "code id_token token",

                // Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0 
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
                CallbackPath = new PathString("/signin-oidc"),

                // Configure the Claims Issuer to be Auth0
                ClaimsIssuer = "processoeletronico",

                // Set the correct name claim type
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                },

                GetClaimsFromUserInfoEndpoint = false,

                SaveTokens = true,

                // Adiciona token a Claims
                Events = new OpenIdConnectEvents()
                {
                    OnTokenValidated = async c =>
                    {
                        // use the access token to retrieve claims from userinfo
                        var userInfoClient = new UserInfoClient("https://acessocidadao.es.gov.br/is/connect/userinfo");
                        var access_token = c.ProtocolMessage.AccessToken;

                        var userInfoResponse = await userInfoClient.GetAsync(access_token);


                        // create new identity
                        var id = new ClaimsIdentity(c.Ticket.Principal.Identity.AuthenticationType);

                        var userInfoList = userInfoResponse.Claims.ToList();
                        foreach (var ui in userInfoList)
                        {
                            if (ui.Type != "permissao")
                            {
                                id.AddClaim(new Claim(ui.Type, ui.Value));
                            }
                        }

                        var permissaoClaims = userInfoResponse.Claims.Where(x => x.Type == "permissao").ToList();
                        foreach (var permissaoClaim in permissaoClaims)
                        {
                            dynamic objetoPermissao = JsonConvert.DeserializeObject(permissaoClaim.Value.ToString());
                            string recurso = objetoPermissao.Recurso;
                            id.AddClaim(new Claim("Recurso", recurso));
                            var listaAcoes = ((JArray)objetoPermissao.Acoes).Select(x => x.ToString()).ToList();
                            foreach (var acao in listaAcoes)
                            {
                                id.AddClaim(new Claim("Acao$" + recurso, acao));
                            }
                        }

                        //id.AddClaims(userInfoResponse.Claims);

                        id.AddClaim(new Claim("access_token", access_token));
                        id.AddClaim(new Claim("expires_at", DateTime.Now.AddSeconds(int.Parse(c.ProtocolMessage.ExpiresIn)).ToLocalTime().ToString()));
                        id.AddClaim(new Claim("id_token", c.ProtocolMessage.IdToken));
                        id.AddClaim(new Claim("client_id", c.Ticket.Principal.FindFirst("aud").Value));


                        c.Ticket = new AuthenticationTicket(
                            new ClaimsPrincipal(id),
                            c.Ticket.Properties,
                            c.Ticket.AuthenticationScheme
                            );

                        await Task.FromResult(0);
                    }
                }
            };
            options.Scope.Clear();
            options.Scope.Add(settings.Value.Scope);

            app.UseOpenIdConnectAuthentication(options);

            #endregion            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Inicio}/{action=Index}/{id?}");
            });
        }

        private void FillOrgaoEPatriarca(ClaimsIdentity id, string token)
        {

            if (id.HasClaim(a => a.Type == "orgao"))
            {
                string siglaOrganizacao = string.Empty;
                siglaOrganizacao = id.FindFirst("orgao").Value;

                string guidOrganizacao = "";
                string guidPatriarca = "";
                string nomeOrganizacao = "";

                DownloadJson downloadJson = new DownloadJson();
                string urlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");

                Organizacao organizacao = downloadJson.DownloadJsonData<Organizacao>($"{urlApiOrganograma}organizacoes/sigla/{siglaOrganizacao}?guidPatriarca=true", token);

                guidOrganizacao = organizacao.Guid;
                nomeOrganizacao = organizacao.RazaoSocial;
                guidPatriarca = organizacao.GuidPatriarca;

                id.AddClaim(new Claim("guidorganizacao", guidOrganizacao));
                id.AddClaim(new Claim("nomeorganizacao", nomeOrganizacao));
                id.AddClaim(new Claim("guidpatriarca", guidPatriarca));
            }
        }
    }
}
