using Sample.Applications.Grpc.Interceptors;
using Sample.Applications.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
{
    {
        options.Interceptors.Add<SampleExceptionInterceptor>();
        options.EnableDetailedErrors = true;
    }
});

var app = builder.Build();

app.MapGrpcService<SampleTestService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();
