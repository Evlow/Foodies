namespace Foodies.Api.Common
{
    public class JwtSetting
    {
        public string SecretKey { get ; set;}

        public string Issuer { get ; set;}

        public string Audience { get ; set;}

        public int AccessTokenExpirationMinutes { get ; set;}

        public int RefreshTokenExpirationDays { get; set; }
    }
}