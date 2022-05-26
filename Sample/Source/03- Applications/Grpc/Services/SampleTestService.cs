using Grpc.Core;

namespace Sample.Applications.Grpc.Services;

public class SampleTestService : TestService.TestServiceBase
{
    public SampleTestService()
    { }

    public override Task<TestResponse> Test(TestRequest rq, ServerCallContext context)
    {
        return base.Test(rq, context);
    }
}