using Enviewer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class EnviewerBuilderExtensions
    {
        /// <summary>
        /// Register the Enviewer middleware with provided options
        /// </summary>
        public static IApplicationBuilder UseEnviewer(this IApplicationBuilder app, EnviewerOptions options)
        {
            return app.UseMiddleware<EnviewerMiddleware>(options);
        }

        /// <summary>
        /// Register the Enviewer middleware with optional setup action for DI-injected options
        /// </summary>
        public static IApplicationBuilder UseEnviewer(this IApplicationBuilder app, Action<EnviewerOptions> setupAction = null)
        {
            EnviewerOptions options;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<EnviewerOptions>>().Value;
                setupAction?.Invoke(options);
            }
            return app.UseEnviewer(options);
        }
    }
}
