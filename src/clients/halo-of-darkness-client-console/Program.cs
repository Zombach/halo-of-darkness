using HaloOfDarkness.Client.Console;

var m = new ManualResetEventSlim(true);

m.Wait();
m.Reset();
m.Wait();

var grpc = new GrpcProvider();

var response = await grpc.Registration("Test");
Console.WriteLine(response.Message);
