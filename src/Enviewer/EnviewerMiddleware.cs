using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enviewer
{
    /// <summary>
    /// The viewable middleware of configurations.
    /// </summary>
    public class EnviewerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly EnviewerOptions options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"><see cref="RequestDelegate"/></param>
        /// <param name="options">Options of Enviewer.</param>
        public EnviewerMiddleware(RequestDelegate next, EnviewerOptions options)
        {
            this.next = next;
            this.options = options ?? new EnviewerOptions();
        }

        /// <summary>
        /// Processing of middleware.
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/></param>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, IConfiguration configuration)
        {
            if (httpContext.Request.Path == options.Route)
            {
                var configurations = configuration.AsEnumerable().OrderBy(c => c.Key);
                var content = new StringBuilder();
                content.Append("<h1>Enviewer</h1>");
                content.Append("<dl>");
                foreach (var c in configurations)
                {
                    content.Append($"<dt>{c.Key}</dt><dd>{c.Value}</dd>");
                }
                content.Append("</dl>");
                await httpContext.Response.WriteAsync(content.ToString());
                return;
            }

            await next(httpContext);
        }
    }
}
