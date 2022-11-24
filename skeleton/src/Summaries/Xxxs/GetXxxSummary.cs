using FastEndpoints;
using IMOv2.Api.Contracts.Responses.Xxxs;
using IMOv2.Api.Endpoints.Xxxs;

namespace IMOv2.Api.Summaries;

public class GetXxxSummary : Summary<GetXxxEndpoint>
{
    public GetXxxSummary()
    {
        Summary = "Returns a single xxx by id";
        Description = "Returns a single xxx by id";
        Response<XxxResponse>(200, "Successfully found and returned the xxx");
        Response(404, "The xxx does not exist in the system");
    }
}
