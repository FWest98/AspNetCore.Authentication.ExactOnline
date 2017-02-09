using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore.Authentication.ExactOnline
{
    public class ExactOnlineMiddleware : OAuthMiddleware<ExactOnlineAuthenticationOptions>
    {
        public ExactOnlineMiddleware(RequestDelegate next, IDataProtectionProvider dataProtectionProvider, ILoggerFactory loggerFactory, UrlEncoder encoder, IOptions<SharedAuthenticationOptions> sharedOptions, IOptions<ExactOnlineAuthenticationOptions> options) : base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options)
        {
        }

        protected override AuthenticationHandler<ExactOnlineAuthenticationOptions> CreateHandler()
        {
            return new ExactOnlineHandler(Backchannel);
        }
    }
}