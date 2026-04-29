# CSharpLab

A collection of projects built with C# and .NET — ranging from APIs to background workers to full-stack applications. This repo serves as a personal lab for exploring the .NET ecosystem: clean architecture patterns, real-time features, caching strategies, background jobs, and more.

---

## Projects

### GitDashboard

A full-stack Git repository monitoring dashboard built with **ASP.NET Core 10** and **Angular 21**.

Users can register, link their Git repositories by URL, and track activity feeds from those repositories. A background worker periodically polls repositories for updates on a cron schedule.

Tests are ommited.

**Backend**
- ASP.NET Core 10 REST API with Clean Architecture (Api / Domain / Infrastructure / Worker layers)
- PostgreSQL via Entity Framework Core, with a multi-layer cache (in-memory → Redis → DB)
- JWT authentication with refresh tokens stored in HttpOnly cookies
- Real-time features (chat, cursor tracking) via SignalR
- Background job scheduling via a .NET Worker Service + Cronos
- FluentValidation, AutoMapper, Serilog, MessagePack, Scalar/OpenAPI

**Frontend**
- Angular 21 with standalone components and the Signals API
- Angular Material for UI
- OpenAPI-generated TypeScript client (`@hey-api/openapi-ts`)
- Real-time updates via `@microsoft/signalr`
