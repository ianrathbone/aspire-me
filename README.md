# aspire-me

[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download)
[![Aspire](https://img.shields.io/badge/Aspire-9.4.1-green.svg)](https://learn.microsoft.com/en-us/dotnet/aspire/)

A .NET 9.0 Aspire distributed application demonstrating cloud-native, observable, and production-ready development patterns with a weather forecast API service and Blazor Server frontend.

## ?? Quick Start

### Prerequisites
- .NET 9.0 SDK
- Docker Desktop (for full Aspire orchestration)

### Individual Services (Recommended for Development)
```bash
# Clone and setup
git clone https://github.com/ianrathbone/aspire-me.git
cd aspire-me
dotnet restore
dotnet build

# Start API service (Terminal 1)
dotnet run --project aspire.me.ApiService
# API available at: http://localhost:5358

# Start Web frontend (Terminal 2)  
dotnet run --project aspire.me.Web
# Web app available at: http://localhost:5186
```

### Full Aspire Orchestration (Requires Docker)
```bash
dotnet run --project aspire.me.AppHost
# Aspire Dashboard available at: http://localhost:15888
```

## ??? Architecture

```
???????????????????    HTTP     ???????????????????
?                 ???????????????                 ?
?  Blazor Web     ?             ?  Weather API    ?
?  (Port 5186)    ???????????????  (Port 5358)    ?
?                 ?   JSON      ?                 ?
???????????????????             ???????????????????
         ?                               ?
         ?                               ?
    ???????????????????????????????????????????????
    ?         Service Defaults                    ?
    ?   • OpenTelemetry                          ?
    ?   • Health Checks                          ?  
    ?   • Service Discovery                      ?
    ???????????????????????????????????????????????
                         ?
            ???????????????????????????
            ?    Aspire AppHost       ?
            ?   (Orchestration)       ?
            ???????????????????????????
```

## ?? Project Structure

| Project | Description | Technology |
|---------|-------------|------------|
| **aspire.me.AppHost** | Aspire orchestrator for distributed application management | .NET Aspire |
| **aspire.me.ServiceDefaults** | Shared configuration (telemetry, health checks, service discovery) | ASP.NET Core |
| **aspire.me.ApiService** | Weather forecast REST API | ASP.NET Core Minimal API |
| **aspire.me.Web** | Web frontend consuming the API | Blazor Server |
| **aspire.me.Tests** | Integration tests | xUnit + Aspire.Hosting.Testing |

## ?? API Endpoints

| Endpoint | Method | Description | Response |
|----------|--------|-------------|----------|
| `/weatherforecast` | GET | Get weather forecast data | JSON array of forecasts |
| `/health` | GET | Health check endpoint | "Healthy" status |
| `/alive` | GET | Liveness probe | Health check response |
| `/openapi` | GET | OpenAPI specification (dev only) | OpenAPI JSON |

## ?? Testing

```bash
# Build and test
dotnet build
dotnet test

# API health check
curl http://localhost:5358/health

# Get weather data
curl http://localhost:5358/weatherforecast

# Web application
curl http://localhost:5186/
```

## ?? Development

### Hot Reload Development
```bash
# API with hot reload
dotnet watch run --project aspire.me.ApiService

# Web with hot reload  
dotnet watch run --project aspire.me.Web
```

### Adding New Features
1. Update the API service in `aspire.me.ApiService/Program.cs`
2. Update the Blazor frontend in `aspire.me.Web/Components/Pages/`
3. Add integration tests in `aspire.me.Tests/`
4. Test locally before committing

## ?? Container Support

The application is containerized and ready for deployment:
- Uses .NET Aspire for orchestration
- Includes OpenTelemetry for observability
- Health checks for monitoring
- Service discovery for communication

## ??? Technologies Used

- **.NET 9.0** - Latest .NET framework
- **ASP.NET Core** - Web framework for API
- **Blazor Server** - Interactive web UI framework  
- **.NET Aspire** - Cloud-native development framework
- **OpenTelemetry** - Observability and telemetry
- **xUnit** - Testing framework

## ?? Learn More

- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)

## ?? Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## ?? License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.