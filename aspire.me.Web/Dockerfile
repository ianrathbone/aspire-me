# Web Service Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["aspire.me.Web/aspire.me.Web.csproj", "aspire.me.Web/"]
COPY ["aspire.me.ServiceDefaults/aspire.me.ServiceDefaults.csproj", "aspire.me.ServiceDefaults/"]
RUN dotnet restore "aspire.me.Web/aspire.me.Web.csproj"
COPY . .
WORKDIR "/src/aspire.me.Web"
RUN dotnet build "aspire.me.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "aspire.me.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspire.me.Web.dll"]
