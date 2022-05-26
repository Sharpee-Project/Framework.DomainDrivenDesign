using Framework.Application.Grpc.Interceptors;
using Grpc.Core;
using Sharpee.Framework.DomainModel.Exceptions;

namespace Sample.Applications.Grpc.Interceptors;

public sealed class SampleExceptionInterceptor
    : GrpcExceptionInterceptor
{
    private readonly ILogger<SampleExceptionInterceptor> _logger;

    public SampleExceptionInterceptor(
        ILogger<SampleExceptionInterceptor> logger
    )
    {
        _logger = logger;
    }

    public override void OnException(Exception ex, ServerCallContext context)
    {
        var sharpeeEx = ex as SharpeeException;
        var extraInformation = new
        {
            Domain = "Sample",
            context.Host,
            context.Peer,
            context.Deadline,
            context.Method,
            context.Status,
            sharpeeEx?.LogMessage,
            sharpeeEx?.ExceptionStatus,
        };

        _logger.LogError(ex, $"{ex.Message}{{Information}}", extraInformation);

        base.OnException(ex, context);
    }
}
