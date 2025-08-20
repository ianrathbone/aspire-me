# API Service Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["aspire.me.ApiService/aspire.me.ApiService.csproj", "aspire.me.ApiService/"]
COPY ["aspire.me.ServiceDefaults/aspire.me.ServiceDefaults.csproj", "aspire.me.ServiceDefaults/"]
RUN dotnet restore "aspire.me.ApiService/aspire.me.ApiService.csproj"
COPY . .
WORKDIR "/src/aspire.me.ApiService"
RUN dotnet build "aspire.me.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "aspire.me.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspire.me.ApiService.dll"]
