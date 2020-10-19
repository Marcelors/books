namespace Books.Domain.Interfaces
{
    public interface ITokenConfiguration
    {
        string Audience { get; }
        string Issuer { get; }
        int Hours { get; }
    }
}
