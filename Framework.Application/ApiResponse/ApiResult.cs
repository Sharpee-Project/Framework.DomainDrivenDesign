namespace Sharpee.Framework.App.ApiResponse;

public sealed class ApiResult
{
    private ApiResult()
    { }

    public object Result { get; private set; } = new object();

    public static ApiResult From(object obj)
    {
        return new ApiResult
        {
            Result = obj
        };
    }
}