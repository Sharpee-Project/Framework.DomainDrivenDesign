using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sharpee.Framework.App.ApiResponse;
using Sharpee.Framework.App.Utility;
using Sharpee.Framework.DomainModel.Exceptions;
using System;

namespace Sharpee.Framework.App.Attributes;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public int StatusCode { get; set; }

    public override void OnException(ExceptionContext context)
    {
        var message = context.Exception is SharpeeException ex ?
            (string.IsNullOrWhiteSpace(ex.Message) ? SharpeeException.BASE_MESSAGE : ex.Message) :
            SharpeeException.BASE_MESSAGE;

        var apiError = new ApiError(message);
        var apiResult = ApiResult.From(apiError);

        context.Result = new JsonResult(apiResult);
        context.HttpContext.Response.StatusCode =
            StatusCode != default ?
            StatusCode :
            (int)StatusCodeFinder.FindHttpStatusCode(context.Exception);

        base.OnException(context);
    }
}