# Enviewer samples

This directory contains examples of using Enviewer with different hosting models.

## ASP.NET Core

- **MinimalApiApp** — Registers `app.UseEnviewer()` in an ASP.NET Core Minimal API application.
- **RazorPagesApp** — Registers `app.UseEnviewer()` in an ASP.NET Core Razor Pages application.

## Generic Host

- **ConsoleGenericHost** — References `..\..\src\Enviewer\Enviewer.csproj` and registers `app.UseEnviewer()` in an ASP.NET Core Generic Host application. After starting the application, open `http://localhost:5080/enviewer`.

> All samples display configuration values. Do not expose them in production environments.
