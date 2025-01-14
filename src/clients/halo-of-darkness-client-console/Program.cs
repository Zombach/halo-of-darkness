using HaloOfDarkness.Client.Console;

var grpc = new GrpcProvider();

var response = await grpc.Registration("Test");
Console.WriteLine(response.Message);
