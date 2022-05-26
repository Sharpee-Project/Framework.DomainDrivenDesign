using Microsoft.AspNetCore.Mvc;
using Sharpee.Framework.App.ApiResponse;

namespace Sample.Applications.RestApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class TestController : ControllerBase
{
    public TestController()
    { }

    /// <summary>
    /// Get test by id
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken ct
    )
    {
        var rs = new { Name = "Test" };
        await Task.Delay(1000, ct);
        return rs.ToActionResult();
    }
}
