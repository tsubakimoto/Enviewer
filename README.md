# Enviewer

[![NuGet status](https://img.shields.io/nuget/v/Enviewer.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/Enviewer)

Simple environment variables viewer

## Getting Started #

1. Install the standard Nuget package into your ASP.NET Core application.

    ```
    Package Manager : Install-Package Enviewer -Version 0.1.0
    CLI : dotnet add package Enviewer --version 0.1.0
    ```

2. In the `Configure` method of `Startup.cs`, insert middleware.

    ```csharp
    app.UseEnviewer();
    ```

    By accessing `/enviewer` in your browser, you can view all the environment variables registered in `Microsoft.Extensions.Configuration.IConfiguration`.
