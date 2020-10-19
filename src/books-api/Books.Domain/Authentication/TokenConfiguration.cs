using Books.Domain.Interfaces;

namespace Books.Domain.Authentication
{
    public class TokenConfiguration : ITokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Hours { get; set; }
    }
}
