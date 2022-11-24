using ValueOf;

namespace IMOv2.Api.Domain.Ids;

public class XxxId : ValueOf<Guid, XxxId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Xxx Id cannot be empty", nameof(XxxId));
        }
    }
}
