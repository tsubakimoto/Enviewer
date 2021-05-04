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

        public EnviewerMiddleware(RequestDelegate next, EnviewerOptions options)
        {
            this.next = next;
            this.options = options ?? new EnviewerOptions();
        }

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
