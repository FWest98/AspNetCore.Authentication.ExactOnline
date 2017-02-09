using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Authentication.ExactOnline
{
    public class ExactOnlineOptions : OAuthOptions
    {
        public ExactOnlineOptions()
        {
            AuthenticationScheme = ExactOnlineDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            CallbackPath = new PathString("/signin-exactonline");
            AuthorizationEndpoint = ExactOnlineDefaults.AuthorizationEndpoint;
            TokenEndpoint = ExactOnlineDefaults.TokenEndpoint;
            UserInformationEndpoint = ExactOnlineDefaults.UserInformationEndpoint;
        }

        public string WebhookSecret { get; set; }
    }
}