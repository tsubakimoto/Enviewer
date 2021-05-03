using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Enviewer.Test
{
    public class EnviewerMiddlewareTest
    {
        [Fact]
        async Task ReturnsOkForRequest()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {

                        })
                        .Configure(app =>
                        {
                            app.UseEnviewer();
                        });
                })
                .StartAsync();

            var response = await host.GetTestClient().GetAsync("/enviewer");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
