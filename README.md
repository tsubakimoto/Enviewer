# Enviewer

[![NuGet status](https://img.shields.io/nuget/v/Enviewer.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/Enviewer)

Simple environment variables viewer

## Getting Started #

1. Install the standard Nuget package into your ASP.NET Core application.

    ```
    Package Manager : Install-Package Enviewer -Version 1.1.0
    CLI : dotnet add package Enviewer --version 1.1.0
    ```

2. In the `Configure` method of `Startup.cs`, insert middleware.

    ```csharp
    app.UseEnviewer();
    ```

    By accessing `/enviewer` in your browser, you can view all the environment variables registered in `Microsoft.Extensions.Configuration.IConfiguration`.

3. If you want to customize Enviewer endpoint, insert the options.

    ```csharp
    app.UseEnviewer(options =>
    {
        options.Route = "/enviewer-sub";
    });
    ```
