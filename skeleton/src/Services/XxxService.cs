using FluentValidation;
using FluentValidation.Results;
using IMOv2.Api.Contracts.Entities;
using IMOv2.Api.Database;
using IMOv2.Api.Domain;
using AutoMapper;

namespace IMOv2.Api.Services;

public class XxxService : IXxxService
{
    private readonly IRepository<XxxEntity, Guid, ImoV2DbContext> _xxxRepository;
    private readonly IMapper _mapper;

    public XxxService(  IRepository<XxxEntity, Guid, ImoV2DbContext> xxxRepository,
                        IMapper mapper)
    {
        _xxxRepository = xxxRepository;
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync(Xxx xxx)
    {
        var existingXxx = await _xxxRepository.GetAsync(xxx.Id.Value);
        if (existingXxx is not null)
        {
            var message = $"Xxx with id {xxx.Id} already exists";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Xxx), message)
            });
        }

        var xxxEntity = _mapper.Map<XxxEntity>(xxx);
        await _xxxRepository.AddAsync(xxxEntity);
        return await _xxxRepository.SaveChangesAsync() > 0;
    }

    public async Task<Xxx?> GetAsync(Guid id)
    {
        var xxxEntity = await _xxxRepository.GetAsync(id);
        return _mapper.Map<Xxx>(xxxEntity);
    }

    public async Task<bool> UpdateAsync(Xxx xxx)
    {
        var xxxEntity = _mapper.Map<XxxEntity>(xxx);
        _xxxRepository.Update(xxxEntity);
        return await _xxxRepository.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingXxx = await _xxxRepository.GetAsync(id);

        if (existingXxx is null)
        {
            var message = $"Xxx with id {id} does not exist";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Xxx), message)
            });
        }

        _xxxRepository.Delete(existingXxx);
        return await _xxxRepository.SaveChangesAsync() > 0;
    }
}
