
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using StoneCastle.Domain.Authentication.Entities;
using StoneCastle.WebSecurity;
using StoneCastle.WebSecurity.Providers;

namespace StoneCastle.Identity
{
    public class SecurityStartup
    {
        public void ConfigureCookieAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(StoneCastle.Security.Commons.Constants.COOKIE_PATH),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }

        public void ConfigureTokenAuthGeneration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString(StoneCastle.Security.Commons.Constants.CONFIGURATION_TOKEN_ENDPOINT),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(StoneCastle.Security.Commons.Constants.ACCESSTOKEN_EXPIRE_TIMESPAN_MINUTES),
                Provider = new CustomOAuthProviderToken(),
                AccessTokenFormat = new CustomJwtFormat(StoneCastle.Security.Commons.Constants.CONFIGURATION_ISSUER)
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        public void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = StoneCastle.Security.Commons.Constants.CONFIGURATION_ISSUER;//Uri.UriSchemeHttp;
            string audienceId = StoneCastle.Security.Commons.Constants.CONFIGURATION_AUDIENCE_ID;
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(StoneCastle.Security.Commons.Constants.CONFIGURATION_AUDIENCE_SECRET);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }
    }
}
