using AutoMapper;
using NotificationService.Domain;
using NotificationService.Domain.Directories;
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

            CreateMap<MessengerRequest, Messengers>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<Messengers, MessengerResponse>();
            CreateMap<MessengerUpdateRequest, Messengers>();

            CreateMap<BettorRequest, Bettors>(); //Идентификатор не генерируем, потому что он должен быть такой же, как в UserService
            CreateMap<Bettors, BettorResponse>();
            CreateMap<BettorUpdateRequest, Messengers>();
        }
    }
}
