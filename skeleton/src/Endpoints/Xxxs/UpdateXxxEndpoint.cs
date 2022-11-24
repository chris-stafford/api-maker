using IMOv2.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using IMOv2.Api.Domain;
using IMOv2.Api.Contracts.Requests.Xxxs;
using IMOv2.Api.Contracts.Responses.Xxxs;

namespace IMOv2.Api.Endpoints.Xxxs;

[HttpPut("xxxs/{id:guid}"), AllowAnonymous]
public class UpdateXxxEndpoint : Endpoint<UpdateXxxRequest, XxxResponse>
{
    private readonly IXxxService _xxxService;
    private readonly AutoMapper.IMapper _mapper;

    public UpdateXxxEndpoint(IXxxService xxxService,
                             AutoMapper.IMapper mapper)
    {
        _xxxService = xxxService;
        _mapper = mapper;
    }

    public override async Task HandleAsync(UpdateXxxRequest req, CancellationToken ct)
    {
        var existingXxx = await _xxxService.GetAsync(req.Id);

        if (existingXxx is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var xxx = _mapper.Map<Xxx>(req);
        await _xxxService.UpdateAsync(xxx);

        var xxxResponse = _mapper.Map<XxxResponse>(xxx);
        await SendOkAsync(xxxResponse, ct);
    }
}
