using AutoMapper;
using IMOv2.Api.Contracts.Entities;
using IMOv2.Api.Contracts.Requests.Xxxs;
using IMOv2.Api.Contracts.Responses.Xxxs;
using IMOv2.Api.Domain;
using IMOv2.Api.Domain.Common;
using IMOv2.Api.Domain.Ids;

namespace IMOv2.Api.Mapping;

public class XxxMappingProfile : Profile
{
    public XxxMappingProfile()
    {
        CreateMap<CreateXxxRequest, Xxx>()
            .ForMember(x => x.Id, x => Guid.NewGuid())
            .ForMember(x => x.EmailAddress, x => x.MapFrom(y => EmailAddress.From(y.EmailAddress)))
            .ForMember(x => x.Username, x => x.MapFrom(y => Username.From(y.Username)))
            .ForMember(x => x.FullName, x => x.MapFrom(y => FullName.From(y.FullName)))
            .ForMember(x => x.DateOfBirth, x => x.MapFrom(y => DateOfBirth.From(DateOnly.FromDateTime(y.DateOfBirth))));

        CreateMap<UpdateXxxRequest, Xxx>()
            .ForMember(x => x.Id, x => x.MapFrom(y => XxxId.From(y.Id)))
            .ForMember(x => x.EmailAddress, x => x.MapFrom(y => EmailAddress.From(y.EmailAddress)))
            .ForMember(x => x.Username, x => x.MapFrom(y => Username.From(y.Username)))
            .ForMember(x => x.FullName, x => x.MapFrom(y => FullName.From(y.FullName)))
            .ForMember(x => x.DateOfBirth, x => x.MapFrom(y => DateOfBirth.From(DateOnly.FromDateTime(y.DateOfBirth))));
        
        CreateMap<Xxx, XxxResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id.Value))
            .ForMember(x => x.EmailAddress, x => x.MapFrom(y => y.EmailAddress.Value))
            .ForMember(x => x.Username, x => x.MapFrom(y => y.Username.Value))
            .ForMember(x => x.FullName, x => x.MapFrom(y => y.FullName.Value))
            .ForMember(x => x.DateOfBirth, x => x.MapFrom(y => y.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)));

        CreateMap<Xxx, XxxEntity>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id.Value))
            .ForMember(x => x.EmailAddress, x => x.MapFrom(y => y.EmailAddress.Value))
            .ForMember(x => x.Username, x => x.MapFrom(y => y.Username.Value))
            .ForMember(x => x.FullName, x => x.MapFrom(y => y.FullName.Value))
            .ForMember(x => x.DateOfBirth, x => x.MapFrom(y => y.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)));
        
        CreateMap<XxxEntity, Xxx>()
            .ForMember(x => x.Id, x => x.MapFrom(y => XxxId.From(y.Id)))
            .ForMember(x => x.EmailAddress, x => x.MapFrom(y => EmailAddress.From(y.EmailAddress)))
            .ForMember(x => x.Username, x => x.MapFrom(y => Username.From(y.Username)))
            .ForMember(x => x.FullName, x => x.MapFrom(y => FullName.From(y.FullName)))
            .ForMember(x => x.DateOfBirth, x => x.MapFrom(y => DateOfBirth.From(DateOnly.FromDateTime(y.DateOfBirth))));
    }
}