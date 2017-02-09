namespace AspNetCore.Authentication.ExactOnline
{
    public static class ExactOnlineDefaults
    {
        public const string AuthenticationScheme = "ExactOnline";

        public static readonly string AuthorizationEndpoint = "https://start.exactonline.nl/api/oauth2/auth";

        public static readonly string TokenEndpoint = "https://start.exactonline.nl/api/oauth2/token";

        public static readonly string UserInformationEndpoint = "https://start.exactonline.nl/api/v1/current/Me";
    }
}
