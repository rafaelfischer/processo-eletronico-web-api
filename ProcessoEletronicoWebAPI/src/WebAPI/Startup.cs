using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProcessoEletronicoService.WebAPI.Config;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Swashbuckle.Swagger.Model;
using Microsoft.Extensions.PlatformAbstractions;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.WebAPI.Middleware;

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

            services.AddMvc()
                    .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    });

            InjecaoDependencias.InjetarDependencias(services);
            ConfiguracaoAutoMapper.CriarMapeamento();

            #region Políticas que serão concedidas
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Processo.Autuar", policy => policy.RequireClaim("Acao$Processo", "Autuar"));
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
                        Url = "http://prodest.es.gov.br"
                    }
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

                ScopeName = autenticacaoIdentityServer.ScopeName,
                AutomaticAuthenticate = autenticacaoIdentityServer.AutomaticAuthenticate
            });
            #endregion

            #region Configuração para buscar as permissões do usuário
            app.UseRequestUserInfo(new RequestUserInfoOptions
            {
                UserInfoEndpoint = "https://sistemas.es.gov.br/prodest/acessocidadao/is/connect/userinfo"
            });
            #endregion

            app.UseMvc();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Documentacao")),
                RequestPath = new PathString("/Documentacao")
            });

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
