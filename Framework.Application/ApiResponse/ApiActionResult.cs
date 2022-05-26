using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Sharpee.Framework.App.ApiResponse;

public static partial class ApiActionResult
{
    public static IActionResult ToActionResult(
        this object obj,
        HttpStatusCode statusCode = HttpStatusCode.OK
    )
    {
        var apiResult = ApiResult.From(obj);
        var result = new ObjectResult(apiResult)
        {
            StatusCode = (int)statusCode
        };

        return result;
    }

    public static IActionResult ToActionResult(
        this object obj,
        string locationForCreatedResult
    )
    {
        var apiResult = ApiResult.From(obj);
        var result = new CreatedResult(locationForCreatedResult, apiResult);

        return result;
    }
}