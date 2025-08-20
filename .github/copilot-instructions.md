# Aspire.me Application
Aspire.me is a .NET 9.0 Aspire distributed application consisting of a weather forecast API service and a Blazor web frontend. This is a reference implementation demonstrating .NET Aspire's capabilities for building cloud-native, observable, and production-ready distributed applications.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively
- Bootstrap, build, and test the repository:
  - `wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh && chmod +x dotnet-install.sh`
  - `./dotnet-install.sh --channel 9.0`
  - `export PATH="$HOME/.dotnet:$PATH"`
  - `dotnet restore` -- takes 40 seconds to complete. NEVER CANCEL. Set timeout to 60+ minutes.
  - `dotnet build` -- takes 18 seconds to complete. NEVER CANCEL. Set timeout to 30+ minutes.
- `dotnet test` -- takes 25+ seconds but FAILS in sandboxed environments due to Aspire orchestration requirements. NEVER CANCEL. Set timeout to 60+ minutes.
- Run individual services (recommended approach):
  - API Service: `dotnet run --project aspire.me.ApiService` (runs on http://localhost:5358)
  - Web Frontend: `dotnet run --project aspire.me.Web` (runs on http://localhost:5186)  
- Run full orchestrated application (requires Docker/Kubernetes):
  - `dotnet run --project aspire.me.AppHost` -- may fail in sandboxed environments due to DCP requirements
- Alternative Docker development:
  - `docker-compose up` -- runs both services in containers

## Validation
- Always manually validate any new code by running the individual services and testing endpoints.
- ALWAYS run through at least one complete end-to-end scenario after making changes.
- Test the API service health: `curl http://localhost:5358/health` should return "Healthy"
- Test the API service weather endpoint: `curl http://localhost:5358/weatherforecast` should return JSON weather data
- Test the Web service: `curl http://localhost:5186/` should return HTML with "aspire.me.Web" in title
- You can build and run individual services, but the full Aspire orchestration may not work in sandboxed environments.
- Always build and test your changes before committing.

## Common tasks
The following are outputs from frequently run commands. Reference them instead of viewing, searching, or running bash commands to save time.

### Repo root
```
ls -a [repo-root]
.
..
.devcontainer/
.github/
.git
.gitignore
.editorconfig
LICENSE
README.md
aspire.me.ApiService/
aspire.me.AppHost/  
aspire.me.ServiceDefaults/
aspire.me.Tests/
aspire.me.Web/
aspire.me.sln
docker-compose.yml
dotnet-install.sh
```

### Project Structure
- **aspire.me.AppHost** - Aspire orchestrator that manages the distributed application (requires DCP/Kubernetes)
- **aspire.me.ServiceDefaults** - Shared configuration for OpenTelemetry, health checks, and service discovery
- **aspire.me.ApiService** - Weather forecast REST API service (ASP.NET Core minimal API)
- **aspire.me.Web** - Blazor Server frontend that consumes the API service
- **aspire.me.Tests** - Integration tests using Aspire.Hosting.Testing (requires full orchestration)

### Technology Stack
- .NET 9.0
- ASP.NET Core (API service)
- Blazor Server (Web frontend)
- .NET Aspire 9.4.1 (distributed application framework)
- OpenTelemetry (observability)
- xUnit (testing)
- Docker & Docker Compose (containerization)

### Build Timing Expectations
- **CRITICAL**: All build operations require .NET 9.0 SDK installation first
- Restore: ~40 seconds - NEVER CANCEL, set timeout to 60+ minutes
- Build: ~18 seconds - NEVER CANCEL, set timeout to 30+ minutes  
- Test: ~25+ seconds but fails in sandbox - NEVER CANCEL, set timeout to 60+ minutes
- Individual service startup: ~1-2 seconds

### Development Workflow
1. Install .NET 9.0 SDK using the dotnet-install script
2. Always set PATH to include `$HOME/.dotnet`
3. Run `dotnet restore` first (required for all subsequent operations)
4. Run `dotnet build` to compile the solution
5. For development, run individual services instead of full orchestration:
   - Start API: `dotnet run --project aspire.me.ApiService`
   - Start Web: `dotnet run --project aspire.me.Web` 
6. Test manually using curl commands for API endpoints
7. Use `dotnet watch run --project [projectname]` for hot reload during development
8. Alternative: Use `docker-compose up` for containerized development

### API Endpoints
- GET /weatherforecast - Returns weather forecast data (JSON array)
- GET /health - Returns "Healthy" status
- GET /alive - Returns health check for liveness probe
- GET /openapi - Returns OpenAPI specification (development only)

### Web Application
- Blazor Server application on http://localhost:5186
- Uses HttpClient to call API service at "https+http://apiservice" (service discovery)
- When running individually, configure API base URL if needed

### Version Information
- .NET Aspire: 9.4.1 (latest stable)
- Aspire.AppHost.Sdk: 9.4.1
- Aspire.Hosting.AppHost: 9.4.1
- Microsoft.Extensions.ServiceDiscovery: 9.4.1
- Aspire.Hosting.Testing: 9.4.1

### Containerization
- Each service has its own Dockerfile for production deployment
- docker-compose.yml provided for local development
- Health checks included for monitoring
- Proper networking configuration between services

### Limitations in Sandboxed Environments
- Full Aspire orchestration (`dotnet run --project aspire.me.AppHost`) requires DCP (Distributed Configuration Protocol) or Kubernetes
- Integration tests fail because they require full orchestration
- Service discovery between services may not work without orchestration
- Individual services work perfectly for development and testing
- Docker Compose provides an alternative for local development without full Aspire orchestration
