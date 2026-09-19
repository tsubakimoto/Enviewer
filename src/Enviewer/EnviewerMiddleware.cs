using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

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
                content.Append("<style>table{border-collapse:collapse;width:100%;}th,td{border:1px solid #d0d7de;padding:8px 12px;text-align:left;vertical-align:top;}th{background-color:#f6f8fa;}code{background-color:#f6f8fa;padding:2px 4px;white-space:pre-wrap;word-break:break-word;}button.copy-value{margin-left:8px;border:1px solid #d0d7de;border-radius:4px;background:#fff;cursor:pointer;padding:2px 6px;}button.copy-value:hover{background:#f3f4f6;}</style>");
                content.Append("<table><thead><tr><th>Key</th><th>Value</th></tr></thead><tbody>");
                foreach (var c in configurations)
                {
                    if (string.IsNullOrWhiteSpace(c.Key))
                        continue;
                    content.Append($"<tr><th>{c.Key}</th><td><code>{c.Value}</code><button type=\"button\" class=\"copy-value\" title=\"Copy value\" aria-label=\"Copy value\" onclick=\"copyValue(this)\">&#x1F4CB;</button></td></tr>");
                }
                content.Append("</tbody></table><script>function copyValue(button){navigator.clipboard.writeText(button.parentElement.querySelector('code').textContent);}</script>");
                await httpContext.Response.WriteAsync(content.ToString());
                return;
            }

            await next(httpContext);
        }
    }
}
