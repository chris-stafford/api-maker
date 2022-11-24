using IMOv2.Api.Contracts.Responses;
using FastEndpoints;
using IMOv2.Api.Contracts.Responses.Xxxs;
using IMOv2.Api.Endpoints.Xxxs;

namespace IMOv2.Api.Summaries;

public class UpdateXxxSummary : Summary<UpdateXxxEndpoint>
{
    public UpdateXxxSummary()
    {
        Summary = "Updates an existing xx in the system";
        Description = "Updates an existing xx in the system";
        Response<XxxResponse>(201, "Xxx was successfully updated");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
