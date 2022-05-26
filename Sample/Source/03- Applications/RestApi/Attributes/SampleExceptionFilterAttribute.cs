using Microsoft.AspNetCore.Mvc.Filters;
using Sharpee.Framework.App.Attributes;
using Sharpee.Framework.DomainModel.Exceptions;

namespace Sample.Applications.RestApi.Attributes;

public sealed class SampleExceptionFilterAttribute
    : ApiExceptionFilterAttribute
{
    private readonly ILogger<SampleExceptionFilterAttribute> _logger;

    public SampleExceptionFilterAttribute(
        ILogger<SampleExceptionFilterAttribute> logger
    )
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        var sharpeeEx = context.Exception as SharpeeException;
        var extraInformation = new
        {
            Domain = "Sample",
            Host = new
            {
                context.HttpContext.Request.Host.Host,
                context.HttpContext.Request.Host.Port,
            },
            Remote = new
            {
                IP = context.HttpContext?.Connection.RemoteIpAddress?.ToString(),
                Port = context.HttpContext?.Connection.RemotePort,
            },
            sharpeeEx?.LogMessage,
            sharpeeEx?.ExceptionStatus,
        };

        _logger.LogError(context.Exception, $"{context.Exception.Message}{{Information}}", extraInformation);

        base.OnException(context);
    }
}
