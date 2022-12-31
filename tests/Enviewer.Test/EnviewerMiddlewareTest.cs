namespace Enviewer.Test;

public class EnviewerMiddlewareTest
{
    [Fact]
    async Task ReturnsOkForRequestWithoutOptions()
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(builder =>
            {
                builder
                    .UseTestServer()
                    .ConfigureServices(services => { })
                    .Configure(app => app.UseEnviewer());
            })
            .StartAsync();

        var response = await host.GetTestClient().GetAsync("/enviewer");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.StartsWith("<h1>Enviewer</h1>", body);
    }

    [Theory]
    [InlineData(null, "/enviewer")]
    [InlineData("", "/enviewer")]
    [InlineData(" ", "/enviewer")]
    [InlineData("/enviewer", "/enviewer")]
    [InlineData("/test", "/test")]
    async Task ReturnsOkForRequestWithOptions(string route, string expectedRoute)
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(builder =>
            {
                builder
                    .UseTestServer()
                    .ConfigureServices(services => { })
                    .Configure(app =>
                    {
                        app.UseEnviewer(new EnviewerOptions
                        {
                            Route = route
                        });
                    });
            })
            .StartAsync();

        var response = await host.GetTestClient().GetAsync(expectedRoute);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.StartsWith("<h1>Enviewer</h1>", body);
    }

    [Theory]
    [InlineData(null, "/enviewer")]
    [InlineData("", "/enviewer")]
    [InlineData(" ", "/enviewer")]
    [InlineData("/enviewer", "/enviewer")]
    [InlineData("/test", "/test")]
    async Task ReturnsOkForRequestWithSetupAction(string route, string expectedRoute)
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(builder =>
            {
                builder
                    .UseTestServer()
                    .ConfigureServices(services => { })
                    .Configure(app =>
                        app.UseEnviewer(options => options.Route = route));
            })
            .StartAsync();

        var response = await host.GetTestClient().GetAsync(expectedRoute);
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
                    .ConfigureServices(services => { })
                    .Configure(app => app.UseEnviewer());
            })
            .StartAsync();

        var response = await host.GetTestClient().GetAsync("/");
        var body = await response.Content.ReadAsStringAsync();
        Assert.False(body.StartsWith("<h1>Enviewer</h1>"));
    }
}
