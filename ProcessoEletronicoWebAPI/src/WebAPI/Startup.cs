using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.WebAPI.Base;
using ProcessoEletronicoService.WebAPI.Common;
using ProcessoEletronicoService.WebAPI.Config;
using ProcessoEletronicoService.WebAPI.Middleware;
using Prodest.ProcessoEletronico.Integration.Common.Base;
using Swashbuckle.Swagger.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ProcessoEletronicoContext.ConnectionString = Environment.GetEnvironmentVariable("ProcessoEletronicoConnectionString");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configurar o objeto AutenticacaoIdentityServer para ser usado na autenticação
            services.Configure<AutenticacaoIdentityServer>(Configuration.GetSection("AutenticacaoIdentityServer"));

            services.AddMemoryCache();

            services.AddMvc()
                    .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IClientAccessTokenProvider, AcessoCidadaoClientAccessToken>();
            services.AddScoped<ICurrentUserProvider, CurrentUser>();
            services.AddLogging();

            services.AddAutoMapper();
            InjecaoDependencias.InjetarDependencias(services);

            #region Políticas que serão concedidas
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Processo.Autuar", policy => policy.RequireClaim("Acao$Processo", "Autuar"));
                options.AddPolicy("RascunhoProcesso.Rascunhar", policy => policy.RequireClaim("Acao$RascunhoProcesso", "Rascunhar"));
                options.AddPolicy("Despacho.Inserir", policy => policy.RequireClaim("Acao$Despacho", "Inserir"));
                options.AddPolicy("RascunhosDespacho.Elaborar", policy => policy.RequireClaim("Acao$RascunhosDespacho", "Elaborar"));
                options.AddPolicy("PlanoClassificacao.Inserir", policy => policy.RequireClaim("Acao$PlanoClassificacao", "Inserir"));
                options.AddPolicy("PlanoClassificacao.Excluir", policy => policy.RequireClaim("Acao$PlanoClassificacao", "Excluir"));
                options.AddPolicy("Funcao.Inserir", policy => policy.RequireClaim("Acao$Funcao", "Inserir"));
                options.AddPolicy("Funcao.Excluir", policy => policy.RequireClaim("Acao$Funcao", "Excluir"));
                options.AddPolicy("Atividade.Inserir", policy => policy.RequireClaim("Acao$Atividade", "Inserir"));
                options.AddPolicy("Atividade.Excluir", policy => policy.RequireClaim("Acao$Atividade", "Excluir"));
                options.AddPolicy("TipoDocumental.Inserir", policy => policy.RequireClaim("Acao$TipoDocumental", "Inserir"));
                options.AddPolicy("TipoDocumental.Excluir", policy => policy.RequireClaim("Acao$TipoDocumental", "Excluir"));
                options.AddPolicy("DestinacaoFinal.Inserir", policy => policy.RequireClaim("Acao$DestinacaoFinal", "Inserir"));
                options.AddPolicy("DestinacaoFinal.Excluir", policy => policy.RequireClaim("Acao$DestinacaoFinal", "Excluir"));
                options.AddPolicy("Sinalizacao.Inserir", policy => policy.RequireClaim("Acao$Sinalizacao", "Inserir"));
                options.AddPolicy("Sinalizacao.Excluir", policy => policy.RequireClaim("Acao$Sinalizacao", "Excluir"));
                options.AddPolicy("DestinacaoFinal.Excluir", policy => policy.RequireClaim("Acao$DestinacaoFinal", "Excluir"));
            }
            );
            #endregion

            #region Configuração do Swagger
            services.AddSwaggerGen();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Processo Eletrônico Web API",
                    Description = "Núcleo de serviço do sistema Processo Eletrônico implementado pelo Governo do Estado do Espírito Santo.",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "PRODEST",
                        Email = "atendimento@prodest.es.gov.br",
                        Url = "https://prodest.es.gov.br"
                    },
                });

                //Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                //Set the comments path for the swagger json and ui.
                var xmlPath = Path.Combine(basePath, "WebAPI.xml");
                options.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<AutenticacaoIdentityServer> autenticacaoIdentityServerConfig)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            #region Configurações de autenticação
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            AutenticacaoIdentityServer autenticacaoIdentityServer = autenticacaoIdentityServerConfig.Value;
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = autenticacaoIdentityServer.Authority,
                RequireHttpsMetadata = autenticacaoIdentityServer.RequireHttpsMetadata,

                AllowedScopes = autenticacaoIdentityServer.AllowedScopes,
                AutomaticAuthenticate = autenticacaoIdentityServer.AutomaticAuthenticate
            });
            #endregion

            #region Configuração para buscar as permissões do usuário
            app.UseRequestUserInfo(new RequestUserInfoOptions
            {
                UserInfoEndpoint = autenticacaoIdentityServer.Authority + "connect/userinfo"
            });
            #endregion

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();

            #region Configuração do Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            var requestPath = Environment.GetEnvironmentVariable("REQUEST_PATH") ?? string.Empty;
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi("api/documentation", requestPath + "/swagger/v1/swagger.json");
            #endregion
        }
    }
}
