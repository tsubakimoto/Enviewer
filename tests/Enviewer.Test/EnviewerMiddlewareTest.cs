namespace Enviewer.Test;

[TestClass]
public sealed class EnviewerMiddlewareTest
{
    [TestMethod]
    public async Task ReturnsOkForRequestWithoutOptions()
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
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        StringAssert.StartsWith(body, "<h1>Enviewer</h1>");
    }

    [TestMethod]
    [DataRow(null, "/enviewer")]
    [DataRow("", "/enviewer")]
    [DataRow(" ", "/enviewer")]
    [DataRow("/enviewer", "/enviewer")]
    [DataRow("/test", "/test")]
    public async Task ReturnsOkForRequestWithOptions(string? route, string expectedRoute)
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
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        StringAssert.StartsWith(body, "<h1>Enviewer</h1>");
    }

    [TestMethod]
    [DataRow(null, "/enviewer")]
    [DataRow("", "/enviewer")]
    [DataRow(" ", "/enviewer")]
    [DataRow("/enviewer", "/enviewer")]
    [DataRow("/test", "/test")]
    public async Task ReturnsOkForRequestWithSetupAction(string? route, string expectedRoute)
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
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        StringAssert.StartsWith(body, "<h1>Enviewer</h1>");
    }

    [TestMethod]
    public async Task NotEnviewerResponseForOtherUrlRequest()
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
        Assert.IsFalse(body.StartsWith("<h1>Enviewer</h1>", StringComparison.Ordinal));
    }
}
