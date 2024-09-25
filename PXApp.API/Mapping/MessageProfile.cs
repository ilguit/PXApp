using AutoMapper;
using PXApp.API.Entity.Message;
using PXApp.Core.Db.Entity;

namespace PXApp.API.Mapping;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<TableMessage, MessageResponse>();

        CreateMap<MessagePostBody, TableMessage>()
            .ForMember(s => s.DateCreated, o => o.Ignore())
            .ForMember(s => s.Id, o => o.Ignore());

        CreateMap<MessagePutBody, TableMessage>()
            .ForMember(s => s.DateCreated, o => o.Ignore())
            .ForMember(s => s.Id, o => o.Ignore());
    }
}