using IMOv2.Api.Contracts.Responses;
using FastEndpoints;
using IMOv2.Api.Endpoints.Xxxs;
using IMOv2.Api.Contracts.Responses.Xxxs;

namespace IMOv2.Api.Summaries.Xxxs;

public class CreateXxxSummary : Summary<CreateXxxEndpoint>
{
    public CreateXxxSummary()
    {
        Summary = "Creates a new xxx in the system";
        Description = "Creates a new xxx in the system";
        Response<XxxResponse>(201, "Xxx was successfully created");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
