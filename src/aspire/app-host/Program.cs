var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", "admin");
var password = builder.AddParameter("password", "admin", secret: true);
var keycloak = builder.AddKeycloak("keycloak", 8080, username, password).WithImageTag("latest");

var apiService = builder.AddProject<Projects.halo_of_darkness_server>("HaloOfDarknessServer")
    .WithReference(keycloak)
    .WaitFor(keycloak);

builder.Build().Run();
