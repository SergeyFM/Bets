using AutoMapper;
using NotificationService.Domain;
using NotificationService.Models;

namespace NotificationService.Services.Helpers
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IncomingMessageRequest, IncomingMessages>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<IncomingMessages, IncomingMessageResponse>();
        }
    }
}
