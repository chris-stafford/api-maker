using IMOv2.Api.Domain;

namespace IMOv2.Api.Services;

public interface IXxxService
{
    Task<bool> CreateAsync(Xxx customer);

    Task<Xxx?> GetAsync(Guid id);

    Task<bool> UpdateAsync(Xxx customer);

    Task<bool> DeleteAsync(Guid id);
}
