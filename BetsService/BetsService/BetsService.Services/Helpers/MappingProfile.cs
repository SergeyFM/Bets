using AutoMapper;
using BetsService.Domain;
using BetsService.Models;

namespace BetsService.Services.Helpers
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventRequest, Events>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<Events, EventResponse>();

            CreateMap<EventOutcomeRequest, EventOutcomes>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<EventOutcomes, EventOutcomeResponse>();
        }
    }
}
