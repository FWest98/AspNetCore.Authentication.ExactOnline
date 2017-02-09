using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace AspNetCore.Authentication.ExactOnline
{
    public static class ExactOnlineAppBuilderExtensions
    {
        /// <summary>
        /// Add exact authentication to the OAuth authentication providers.
        /// Don't forget to Configure <see cref="ExactOnlineAuthenticationOptions"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when app is null</exception>
        public static IApplicationBuilder UseExactOnlineAuthentication(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ExactOnlineMiddleware>();
        }

        /// <summary>
        /// Add exact online as OAuth authentication provider
        /// </summary>
        /// <exception cref="ArgumentNullException">if either argument is null</exception>
        public static IApplicationBuilder UseExactOnlineAuthentication(this IApplicationBuilder app, ExactOnlineAuthenticationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<ExactOnlineMiddleware>(Options.Create(options));
        }
    }
}