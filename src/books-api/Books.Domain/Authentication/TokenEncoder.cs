using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Books.Domain.Authentication
{
    public class TokenEncoder : ITokenEncoder
    {
        readonly ITokenConfiguration _tokenConfiguration;
        readonly ISigningConfiguration _signingConfiguration;

        public TokenEncoder(ITokenConfiguration tokenConfiguration, ISigningConfiguration signingConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
            _signingConfiguration = signingConfiguration;
        }

        public string Encoder(User user)
        {
            var identity = new ClaimsIdentity(
              new[]{
                    new Claim("UserId", user.Id.ToString()),

              });

            var creationDate = DateTime.Now;
            var expirationDate = creationDate.AddHours(_tokenConfiguration.Hours);

            var handle = new JwtSecurityTokenHandler();
            var securityToken = handle.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });

            return handle.WriteToken(securityToken);
        }
    }
}
}
