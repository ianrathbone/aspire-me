# aspire-me

[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download)
[![Aspire](https://img.shields.io/badge/Aspire-9.4.1-green.svg)](https://learn.microsoft.com/en-us/dotnet/aspire/)

A .NET 9.0 Aspire distributed application demonstrating cloud-native, observable, and production-ready development patterns with a weather forecast API service and Blazor Server frontend.

## 🚀 Quick Start

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

### Full Aspire Orchestration with DevProxy (Requires Docker)
```bash
# Setup DevProxy certificate (one-time setup)
# See .devproxy/cert/README.md for detailed instructions

dotnet run --project aspire.me.AppHost
# Aspire Dashboard available at: http://localhost:15888
# DevProxy available at: http://localhost:8000
```

### Docker Compose with DevProxy
```bash
# Setup certificate first (see DevProxy section below)
docker-compose up
# API available at: http://localhost:5358
# Web available at: http://localhost:5186
# DevProxy available at: http://localhost:8000
```

## 🏗️ Architecture

```
┌─────────────────┐    HTTP     ┌─────────────────┐
│                 │────────────▶│                 │
│  Blazor Web     │             │  Weather API    │
│  (Port 5186)    │◀────────────│  (Port 5358)    │
│                 │   JSON      │                 │
└─────────────────┘             └─────────────────┘
         │                               │
         │          DevProxy             │
         └──────────(Port 8000)──────────┘
         │                               │
    ┌─────────────────────────────────────────────┐
    │         Service Defaults                    │
    │   • OpenTelemetry                          │
    │   • Health Checks                          │  
    │   • Service Discovery                      │
    └─────────────────────────────────────────────┘
                         │
            ┌─────────────────────────┐
            │    Aspire AppHost       │
            │   (Orchestration)       │
            └─────────────────────────┘
```

## 📁 Project Structure

| Project | Description | Technology |
|---------|-------------|------------|
| **aspire.me.AppHost** | Aspire orchestrator for distributed application management | .NET Aspire |
| **aspire.me.ServiceDefaults** | Shared configuration (telemetry, health checks, service discovery) | ASP.NET Core |
| **aspire.me.ApiService** | Weather forecast REST API | ASP.NET Core Minimal API |
| **aspire.me.Web** | Web frontend consuming the API | Blazor Server |
| **aspire.me.Tests** | Integration tests | xUnit + Aspire.Hosting.Testing |

## 🔌 API Endpoints

| Endpoint | Method | Description | Response |
|----------|--------|-------------|----------|
| `/weatherforecast` | GET | Get weather forecast data | JSON array of forecasts |
| `/health` | GET | Health check endpoint | "Healthy" status |
| `/alive` | GET | Liveness probe | Health check response |
| `/openapi` | GET | OpenAPI specification (dev only) | OpenAPI JSON |

## 🔍 DevProxy Integration

This project includes [Microsoft DevProxy](https://learn.microsoft.com/microsoft-cloud/dev/dev-proxy/) for API mocking and request interception during development.

### Features
- **API Mocking** - Mock external API responses
- **Request Interception** - Capture and analyze HTTP traffic
- **Network Simulation** - Simulate network conditions and failures
- **OpenAPI Generation** - Automatically generate API specifications

### Setup DevProxy Certificate (One-time)

1. **Get the Certificate**:
   ```bash
   # Option 1: Install DevProxy locally and generate certificate
   npm install -g @microsoft/dev-proxy
   devproxy --urls-to-watch "https://localhost:*/*"
   # This generates rootCert.pfx in your DevProxy directory
   
   # Option 2: Use existing certificate from DevProxy installation
   # Copy from %USERPROFILE%/.devproxy/ on Windows
   ```

2. **Copy Certificate**:
   ```bash
   # Copy the certificate to the project
   cp /path/to/rootCert.pfx .devproxy/cert/
   ```

3. **Trust Certificate** (Windows):
   ```bash
   # Double-click rootCert.pfx and install to:
   # Local Machine → Trusted Root Certification Authorities
   ```

### DevProxy Configuration
- **Config**: `.devproxy/config/devproxy.json`
- **Mocks**: `.devproxy/config/mocks.json`
- **Certificate**: `.devproxy/cert/rootCert.pfx` (git-ignored)

### DevProxy URLs
- **Proxy**: http://localhost:8000
- **Admin**: http://localhost:8001

## 🧪 Testing

```bash
# Build and test
dotnet build
dotnet test

# API health check
curl http://localhost:5358/health

# Get weather data
curl http://localhost:5358/weatherforecast

# Test through DevProxy (when running)
curl -x http://localhost:8000 http://localhost:5358/weatherforecast

# Web application
curl http://localhost:5186/
```

## 🔧 Development

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
4. Update DevProxy mocks in `.devproxy/config/mocks.json` if needed
5. Test locally before committing

## 🐳 Container Support

The application is containerized and ready for deployment:
- Uses .NET Aspire for orchestration
- Includes DevProxy for development and testing
- Includes OpenTelemetry for observability
- Health checks for monitoring
- Service discovery for communication

## 🛠️ Technologies Used

- **.NET 9.0** - Latest .NET framework
- **ASP.NET Core** - Web framework for API
- **Blazor Server** - Interactive web UI framework  
- **.NET Aspire** - Cloud-native development framework
- **Microsoft DevProxy** - API mocking and request interception
- **OpenTelemetry** - Observability and telemetry
- **xUnit** - Testing framework

## 📚 Learn More

- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Microsoft DevProxy Documentation](https://learn.microsoft.com/microsoft-cloud/dev/dev-proxy/)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
