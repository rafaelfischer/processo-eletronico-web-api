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

namespace WebAPP
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();



            #region CONFIGURACAO AUTENTICAÇÃO ACESSO CIDADAO

            // Add the cookie middleware
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = "/home/login",
                AccessDeniedPath = "/home/AcessoNegado",                
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
                SlidingExpiration = true,
                LogoutPath = "/home/logout",
                CookieName = "processoeletronico"
            });

            // Add the OIDC middleware
            var options = new OpenIdConnectOptions("processoeletronico")
            {
                // Set the authority to your Auth0 domain
                Authority = $"https://{settings.Value.Domain}",

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

                GetClaimsFromUserInfoEndpoint = true,

                SaveTokens = true,
                
                // Adiciona token a Claims
                Events = new OpenIdConnectEvents()
                {
                    OnTokenValidated = async c => 
                    {   
                        c.Ticket.Principal.Identities.First().AddClaim(new Claim("id_token", c.ProtocolMessage.IdToken));
                        c.Ticket.Principal.Identities.First().AddClaim(new Claim("access_token", c.ProtocolMessage.AccessToken));                        
                        c.Ticket.Principal.Identities.First().AddClaim(new Claim("expires_at", DateTime.Now.AddSeconds(int.Parse(c.ProtocolMessage.ExpiresIn)).ToLocalTime().ToString()));
                        c.Ticket.Principal.Identities.First().AddClaim(new Claim("client_id", c.Ticket.Principal.FindFirst("aud").Value));


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
    }
}
