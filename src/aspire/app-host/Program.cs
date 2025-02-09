using System.Globalization;

var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameterFromConfiguration("Username", "KeycloakOptions:Username", secret: true);
var password = builder.AddParameterFromConfiguration("Password", "KeycloakOptions:Password", secret: true);
var portResource = builder.AddParameterFromConfiguration("Port", "KeycloakOptions:Port");
var port = int.Parse(portResource.Resource.Value, CultureInfo.CurrentCulture);

var keycloak = builder.AddKeycloak("keycloak", port, username, password)
    .WithImageTag("latest");

var apiService = builder.AddProject<Projects.halo_of_darkness_server>("HaloOfDarknessServer")
    .WithReference(keycloak)
    .WaitFor(keycloak);

builder.Build().Run();
