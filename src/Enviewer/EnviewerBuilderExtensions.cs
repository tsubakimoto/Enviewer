using Enviewer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// The Microsoft.AspNetCore.Builder.IApplicationBuilder extensions for adding Enviewer middleware support.
    /// </summary>
    public static class EnviewerBuilderExtensions
    {
        /// <summary>
        /// Register the Enviewer middleware with provided options
        /// </summary>
        /// <param name="app">The Microsoft.AspNetCore.Builder.IApplicationBuilder to add the middleware to.</param>
        /// <param name="options">The option of Enviewer.</param>
        public static IApplicationBuilder UseEnviewer(this IApplicationBuilder app, EnviewerOptions options)
        {
            return app.UseMiddleware<EnviewerMiddleware>(options);
        }

        /// <summary>
        /// Register the Enviewer middleware with optional setup action for DI-injected options
        /// </summary>
        /// <param name="app">The Microsoft.AspNetCore.Builder.IApplicationBuilder to add the middleware to.</param>
        /// <param name="setupAction">A delegate which can use an Enviewer builder to build an Enviewer.</param>
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
