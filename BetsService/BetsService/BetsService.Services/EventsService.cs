using BetsService.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Bets.Abstractions.DataAccess.EF.Repositories;
using BetsService.Domain;

namespace BetsService.Services
{
    public class EventsService
    {
        private readonly LaterDeletedEntityRepository<Events> _repository;
        private readonly ILogger<EventsService> _logger;
        private readonly IMapper _mapper;

        public EventsService(LaterDeletedEntityRepository<Events> eventsRepository
            , ILogger<EventsService> logger
            , IMapper mapper)
        {
            _repository = eventsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> AddEventAsync(EventRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[EventsService][AddEventAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var eventObj = _mapper.Map<Events>(request);
                await _repository.AddAsync(eventObj);

                return eventObj.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventsService][AddEventAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
