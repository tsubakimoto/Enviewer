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

            var body = await response.Content.ReadAsStringAsync();
            Assert.StartsWith("<h1>Enviewer</h1>", body);
        }

        [Fact]
        async Task NotEnviewerResponseForOtherUrlRequest()
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

            var response = await host.GetTestClient().GetAsync("/");
            var body = await response.Content.ReadAsStringAsync();
            Assert.False(body.StartsWith("<h1>Enviewer</h1>"));
        }
    }
}
