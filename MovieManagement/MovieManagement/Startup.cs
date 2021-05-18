using IdentityModel;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieManagement.Api.Service;
using MovieManagement.Application;
using MovieManagement.Application.Common.Interfaces;
using MovieManagement.Infrastructure;
using MovieManagement.Infrastructure.Identity;
using MovieManagement.Persistance;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {

            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddPersistance(Configuration);
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin());
            });
            if (Environment.IsEnvironment("Test"))
            {

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MovieDatabase")));
                services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();
                services.AddIdentityServer()
                        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                        {
                            options.ApiResources.Add(new IdentityServer4.Models.ApiResource("api1"));
                            options.ApiScopes.Add(new IdentityServer4.Models.ApiScope("api1"));
                            options.Clients.Add(new IdentityServer4.Models.Client
                            {
                                ClientId = "client",
                                AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                                ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },
                                AllowedScopes = { "openid", "profile", "MovieManagement.ApiAPI", "api1" }
                            });
                        }).AddTestUsers(new List<TestUser>
                        {
                        new TestUser
                        {
                            SubjectId = "4B434A88-212D-4A4D-A17C-F35102D73CBB",
                            Username = "alice",
                            Password = "Pass123$",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "alice@user.com"),
                                new Claim(ClaimTypes.Name, "alice")
                            }
                        }
                        });
                services.AddAuthentication("Bearer").AddIdentityServerJwt();
            }
            else
            {
                services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://localhost:5001";
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateAudience = false
            };
        });

                services.AddAuthorization(options =>
                {
                    options.AddPolicy("ApiScope", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", "api1");
                    });
                });
            }
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows()
                    {

                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"api1", "Demo - full access" }
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "MovieManagement.Api.xml");
                c.IncludeXmlComments(filePath);
            });


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieManagement v1");
                    c.OAuthClientId("swagger");
                    c.OAuth2RedirectUrl("https://localhost:44384/swagger/oauth2-redirect.html");
                    c.OAuthUsePkce();
                });
            }
            app.UseHealthChecks("/hc");
            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseAuthentication();
            if (Environment.IsEnvironment("Test"))
            {
                app.UseIdentityServer();
            }
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
