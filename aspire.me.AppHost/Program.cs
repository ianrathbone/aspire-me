using DevProxy.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add an API service to the application
var apiService = builder.AddProject<Projects.aspire_me_ApiService>("apiservice")
    .WithHttpsHealthCheck("/health");

// Add Dev Proxy as a container resource
var devProxy = builder.AddDevProxyContainer("devproxy")
    // specify the Dev Proxy configuration file; relative to the config folder
    .WithConfigFile("./devproxy.json")
    // mount the local folder with PFX certificate for intercepting HTTPS traffic
    .WithCertFolder(".devproxy/cert")
    // mount the local folder with Dev Proxy configuration
    .WithConfigFolder(".devproxy/config")
    // let Dev Proxy intercept requests to the API service
    .WithUrlsToWatch(() => [$"{apiService.GetEndpoint("https").Url}/*"]);

// Add a web frontend project and configure it to use Dev Proxy
builder.AddProject<Projects.aspire_me_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    // set the HTTPS_PROXY environment variable to the Dev Proxy endpoint
    .WithEnvironment("HTTPS_PROXY", devProxy.GetEndpoint(DevProxyResource.ProxyEndpointName))
    .WithReference(apiService)
    .WaitFor(apiService)
    .WaitFor(devProxy);

builder.Build().Run();
