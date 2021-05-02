using Enviewer;

namespace Microsoft.AspNetCore.Builder
{
    public static class EnviewerBuilderExtensions
    {
        /// <summary>
        /// Register the Enviewer middleware without options
        /// </summary>
        public static IApplicationBuilder UseEnviewer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<EnviewerMiddleware>();
        }
    }
}
