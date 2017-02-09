using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace AspNetCore.Authentication.ExactOnline
{
    public static class ExactOnlineAppBuilderExtensions
    {
        public static IApplicationBuilder UseExactOnlineAuthentication(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ExactOnlineMiddleware>();
        }

        public static IApplicationBuilder UseExactOnlineAuthentication(this IApplicationBuilder app, ExactOnlineOptions options)
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