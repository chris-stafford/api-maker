using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using IMOv2.Api.Services;
using IMOv2.Api.Contracts.Requests.Xxxs;
using IMOv2.Api.Contracts.Responses.Xxxs;
using IMOv2.Api.Domain;

namespace IMOv2.Api.Endpoints.Xxxs;

[HttpPost("xxxs"), AllowAnonymous]
public class CreateXxxEndpoint : Endpoint<CreateXxxRequest, XxxResponse>
{
    private readonly IXxxService _xxxService;
    private readonly AutoMapper.IMapper _mapper;

    public CreateXxxEndpoint(   IXxxService xxxService,
                                AutoMapper.IMapper mapper)
    {
        _xxxService = xxxService;
        _mapper = mapper;
    }

    public override async Task HandleAsync(CreateXxxRequest req, CancellationToken ct)
    {
        var xxx = _mapper.Map<Xxx>(req);

        await _xxxService.CreateAsync(xxx);

        var xxxResponse = _mapper.Map<XxxResponse>(xxx);
        
        await SendCreatedAtAsync<GetXxxEndpoint>(
            new { Id = xxx.Id.Value }, xxxResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}
