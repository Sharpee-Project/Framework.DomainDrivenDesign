using Grpc.Core;
using Grpc.Core.Interceptors;
using Sharpee.Framework.App.Utility;
using Sharpee.Framework.DomainModel.Exceptions;
using System;
using System.Threading.Tasks;

namespace Framework.Application.Grpc.Interceptors;

public class GrpcExceptionInterceptor : Interceptor
{
    /// <summary>
    /// Handles exceptions.
    /// </summary>
    /// <param name="ex"></param>
    public virtual void OnException(Exception ex, ServerCallContext context)
    {
        return;
    }

    public async sealed override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest rq,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation
    )
    {
        try
        {
            return await continuation(rq, context);
        }
        catch (Exception ex)
        {
            OnException(ex, context);

            var message = ex is SharpeeException ?
                GetTheMostInternalExceptionMessage(ex) :
                SharpeeException.BASE_MESSAGE;

            var sharpeeException = ex as SharpeeException;

            var statusCode = sharpeeException is not null ?
                StatusCodeFinder.FindGrpcStatusCode(sharpeeException.ExceptionStatus) :
                StatusCodeFinder.FindGrpcStatusCode(ex);

            throw new RpcException(new Status(statusCode, message));
        }
    }

    private static string GetTheMostInternalExceptionMessage(Exception exception)
    {
        if (exception.InnerException is null)
        {
            return exception.Message;
        }

        return GetTheMostInternalExceptionMessage(exception.InnerException);
    }
}