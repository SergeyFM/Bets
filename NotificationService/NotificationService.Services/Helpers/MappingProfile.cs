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
            CreateMap<IncomingMessages, MessageForSending>();

            CreateMap<MessengerRequest, Messengers>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<Messengers, MessengerResponse>();
            CreateMap<MessengerUpdateRequest, Messengers>();

            CreateMap<BettorRequest, Bettors>(); //Идентификатор не генерируем, потому что он должен быть такой же, как в UserService
            CreateMap<Bettors, BettorResponse>();
            //CreateMap<BettorUpdateRequest, Messengers>();

            CreateMap<MessageSourcesRequest, MessageSources>(); //Идентификатор не генерируем, потому что он должен быть захардкожен в каждом сервисе - это его (сервиса) идентификатор
            CreateMap<MessageSources, MessageSourceResponse>();

            CreateMap<BettorAddressesRequest, BettorAddresses>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<BettorAddresses, BettorAddressResponse>();
            //далее здесь цель одна - не добавлять зависимость от модели в репозиторий (дата контекст) -
            //это, если кого-то смутят поля с дефолтными значениями - мы их просто не обновляем, и нам всё равно, что там
            CreateMap<AddressUpdateRequest, BettorAddresses>(); 
        }
    }
}
