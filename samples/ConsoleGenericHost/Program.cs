const string HostUrl = "http://localhost:5080";
const string EnviewerUrl = $"{HostUrl}/enviewer";

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(HostUrl);

var app = builder.Build();

app.UseEnviewer();
app.MapGet("/", () => Results.Text($"Console Generic Host is running. Open {EnviewerUrl}."));

Console.WriteLine("Console Generic Host demonstration");
Console.WriteLine($"Enviewer: {EnviewerUrl}");
Console.WriteLine("Press Ctrl+C to stop the host.");

app.Run();
