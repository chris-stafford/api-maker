using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using IMOv2.Api.Services;
using IMOv2.Api.Contracts.Requests.Xxxs;
using IMOv2.Api.Contracts.Responses.Xxxs;

namespace IMOv2.Api.Endpoints.Xxxs;

[HttpGet("xxxs/{id:guid}"), AllowAnonymous]
public class GetXxxEndpoint : Endpoint<GetXxxRequest, XxxResponse>
{
    private readonly IXxxService _xxxService;
    private readonly AutoMapper.IMapper _mapper;

    public GetXxxEndpoint(  IXxxService xxxService,
                            AutoMapper.IMapper mapper)
    {
        _xxxService = xxxService;
        _mapper = mapper;
    }

    public override async Task HandleAsync(GetXxxRequest req, CancellationToken ct)
    {
        var xxx = await _xxxService.GetAsync(req.Id);

        if (xxx is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var xxxResponse = _mapper.Map<XxxResponse>(xxx);
        await SendOkAsync(xxxResponse, ct);
    }
}
