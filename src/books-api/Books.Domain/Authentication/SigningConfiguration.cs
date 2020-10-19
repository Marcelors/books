using System;
using System.Security.Cryptography;
using Books.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Books.Domain.Authentication
{
    public class SigningConfiguration : ISigningConfiguration
    {
        public SecurityKey Key { get; private set; }

        public SigningCredentials SigningCredentials { get; private set; }

        public void GenerateKey()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
