using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Authentication.ExactOnline
{
    /// <summary>
    /// Options class for Exact online authentication.
    /// </summary>
    public class ExactOnlineAuthenticationOptions : OAuthOptions
    {
        public ExactOnlineAuthenticationOptions()
        {
            AuthenticationScheme = ExactOnlineDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            CallbackPath = new PathString("/signin-exactonline");
            AuthorizationEndpoint = ExactOnlineDefaults.AuthorizationEndpoint;
            TokenEndpoint = ExactOnlineDefaults.TokenEndpoint;
            UserInformationEndpoint = ExactOnlineDefaults.UserInformationEndpoint;
        }

        /// <summary>
        /// Webhook secret api key
        /// </summary>
        public string WebhookSecret { get; set; }
    }
}