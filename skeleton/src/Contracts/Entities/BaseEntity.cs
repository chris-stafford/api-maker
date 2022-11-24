namespace IMOv2.Api.Contracts.Entities;
public abstract class BaseEntity<T>
{
    public T Id { get; init; } = default!;
}