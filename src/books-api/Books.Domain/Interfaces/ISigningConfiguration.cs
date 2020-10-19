using Microsoft.IdentityModel.Tokens;

namespace Books.Domain.Interfaces
{
    public interface ISigningConfiguration
    {
        SecurityKey Key { get; }
        SigningCredentials SigningCredentials { get; }

        void GenerateKey();
    }
}
