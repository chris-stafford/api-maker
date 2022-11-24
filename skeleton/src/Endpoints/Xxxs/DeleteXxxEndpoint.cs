using IMOv2.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using IMOv2.Api.Contracts.Requests.Xxxs;

namespace IMOv2.Api.Endpoints.Xxxs;

[HttpDelete("xxxs/{id:guid}"), AllowAnonymous]
public class DeleteXxxEndpoint : Endpoint<DeleteXxxRequest>
{
    private readonly IXxxService _xxxService;

    public DeleteXxxEndpoint(IXxxService xxxService)
    {
        _xxxService = xxxService;
    }

    public override async Task HandleAsync(DeleteXxxRequest req, CancellationToken ct)
    {
        var deleted = await _xxxService.DeleteAsync(req.Id);
        if (!deleted)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}
