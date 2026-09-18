namespace Enviewer.Test;

[TestClass]
public sealed class EnviewerOptionsTest
{
    [TestMethod]
    public void SuccessForConstructor()
    {
        EnviewerOptions options = new();
        Assert.AreEqual("/enviewer", options.Route);
    }

    [TestMethod]
    [DataRow(null, "/enviewer")]
    [DataRow("", "/enviewer")]
    [DataRow(" ", "/enviewer")]
    [DataRow("/", "/")]
    [DataRow("/hoge", "/hoge")]
    public void SuccessForRouteSetter(string? route, string expected)
    {
        EnviewerOptions options = new()
        {
            Route = route
        };
        Assert.AreEqual(expected, options.Route);
    }
}