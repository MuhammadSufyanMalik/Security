namespace Security
{
    public class TokenOptions
    {
        public string Audience { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public int AccessTokenExpiration { get; set; } = 60;
        public string SecurityKey { get; set; } = null!;
    }
}
