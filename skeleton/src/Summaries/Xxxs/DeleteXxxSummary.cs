using FastEndpoints;
using IMOv2.Api.Endpoints.Xxxs;

namespace IMOv2.Api.Summaries.Xxxs;

public class DeleteXxxSummary : Summary<DeleteXxxEndpoint>
{
    public DeleteXxxSummary()
    {
        Summary = "Deleted a xxx the system";
        Description = "Deleted a xxx the system";
        Response(204, "The xxx was deleted successfully");
        Response(404, "The xxx was not found in the system");
    }
}
