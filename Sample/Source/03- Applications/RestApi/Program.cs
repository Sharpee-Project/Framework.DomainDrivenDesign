using Microsoft.AspNetCore.Mvc;
using Sample.Applications.RestApi.Attributes;
using Sample.Applications.RestApi.Swagger;
using Sample.Core.ApplicationServices;
using Sample.Infrastructure.DataAccess;
using System.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.Filters.Add<SampleExceptionFilterAttribute>())
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDataAccess();
builder.Services.AddApplicationServices();

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});
builder.Services.AddSwaggerMiddleware();

var app = builder.Build();

app.UseSwaggerMiddleware();
app.UseDataAccess();

app.Use((context, next) =>
{
    var traceId = Activity.Current?.TraceId.ToString() ?? string.Empty;
    context.Response.Headers.Add("X-Trace-Id", traceId);
    return next(context);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
