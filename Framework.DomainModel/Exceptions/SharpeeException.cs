using Framework.DomainModel.Entities;
using System;
using System.Net;

namespace Sharpee.Framework.DomainModel.Exceptions;

public sealed class SharpeeException : Exception
{
    public const string BASE_MESSAGE =
        "An unhandled error occured.";

    public SharpeeException(
        string message,
        ExceptionStatus exceptionStatus,
        Exception innerException,
        string logMessage = null
    )
        : base(GetMessage(message), innerException)
    {
        ExceptionStatus = exceptionStatus;
        LogMessage = logMessage;
    }

    public SharpeeException(
        string message,
        Exception innerException,
        string logMessage = null
    )
        : base(GetMessage(message), innerException)
    {
        LogMessage = logMessage;
    }

    public SharpeeException(string message, ExceptionStatus exceptionStatus, string logMessage = null)
        : base(GetMessage(message))
    {
        ExceptionStatus = exceptionStatus;
        LogMessage = logMessage;
    }

    public SharpeeException(string message, string logMessage = null)
        : base(GetMessage(message))
    {
        LogMessage = logMessage;
    }

    public SharpeeException(ExceptionStatus exceptionStatus, string logMessage = null)
        : base(BASE_MESSAGE)
    {
        ExceptionStatus = exceptionStatus;
        LogMessage = logMessage;
    }

    public SharpeeException(string logMessage = null)
        : base(BASE_MESSAGE)
    {
        LogMessage = logMessage;
    }

    public ExceptionStatus ExceptionStatus { get; private set; } = ExceptionStatus.Internal;

    public string LogMessage { get; private set; } = BASE_MESSAGE;

    private static string GetMessage(string message)
    {
        return string.IsNullOrWhiteSpace(message) ? BASE_MESSAGE : message;
    }
}