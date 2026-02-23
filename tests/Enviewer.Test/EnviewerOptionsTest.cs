namespace Enviewer.Test;

public class EnviewerOptionsTest
{
    [Fact]
    void SuccessForConstructor()
    {
        EnviewerOptions options = new ();
        Assert.Equal("/enviewer", options.Route);
    }

    [Theory]
    [InlineData(null, "/enviewer")]
    [InlineData("", "/enviewer")]
    [InlineData(" ", "/enviewer")]
    [InlineData("/", "/")]
    [InlineData("/hoge", "/hoge")]
    void SuccessForRouteSetter(string? route, string expected)
    {
        EnviewerOptions options = new()
        {
            Route = route
        };
        Assert.Equal(expected, options.Route);
    }
}