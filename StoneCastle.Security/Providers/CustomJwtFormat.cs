using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.IdentityModel.Tokens;

namespace StoneCastle.WebSecurity.Providers
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer = string.Empty; 

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string SignatureAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"; }
        }

        public string DigestAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmlenc#sha256"; }
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = StoneCastle.Security.Commons.Constants.CONFIGURATION_AUDIENCE_ID;

            string symmetricKeyAsBase64 = StoneCastle.Security.Commons.Constants.CONFIGURATION_AUDIENCE_SECRET;

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            //var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyByteArray);
            //var securityKey = new System.IdentityModel.Tokens.InMemorySymmetricSecurityKey(keyByteArray);
            //var signingCredentials = new System.IdentityModel.Tokens.SigningCredentials(securityKey,
            //                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.RsaSha256Signature,
            //                System.IdentityModel.Tokens.SecurityAlgorithms.Sha256Digest);

            //var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey,
            //                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            var signingCredentials = new System.IdentityModel.Tokens.SigningCredentials(
                            new InMemorySymmetricSecurityKey(keyByteArray),
                            SignatureAlgorithm,
                            DigestAlgorithm);

            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;

            //var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingCredentials);

            //var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }


        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
