using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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

        public EnviewerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, IConfiguration configuration)
        {
            if (httpContext.Request.Path == "/enviewer")
            {
                var configurations = configuration.AsEnumerable();
                var content = new StringBuilder();
                foreach (var c in configurations)
                {
                    content.AppendLine($"{c.Key} -> {c.Value}");
                }
                await httpContext.Response.WriteAsync(content.ToString());
                return;
            }

            await next(httpContext);
        }
    }
}
