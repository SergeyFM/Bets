using Microsoft.Extensions.Logging;
using AutoMapper;
using Bets.Abstractions.DataAccess.EF.Repositories;
using BetsService.Domain;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
using BetsService.Services.Exceptions;
using BetsService.Models;

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

        public async Task<EventResponse> GetEventAsync(Guid id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new NotFoundException($"Событие с идентификатором {id} не найдено.");
                }

                return _mapper.Map<EventResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventsService][GetEventAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<EventResponse>> GetListEventsAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                return _mapper.Map<List<EventResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventsService][GetListEventsAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<EventResponse> UpdateEventAsync(EventUpdateRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[EventsService][UpdateEventAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var response = await _repository.GetByIdAsync(request.Id);
                if (response == null)
                {
                    throw new NotFoundException($"Событие с идентификатором {request.Id} не найдено.");
                }

                response.BetsEndTime = request.BetsEndTime;
                response.IsCanceled = request.IsCanceled;
                response.IsOver = request.IsOver;
                response.EventStartTime = request.EventStartTime;
                response.Description = request.Description;
                response.ModifiedBy = request.ModifiedBy;

                await _repository.UpdateAsync(response);

                return _mapper.Map<EventResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventsService][UpdateEventAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteEventAsync(DeleteRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[EventsService][DeleteEventAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventsService][DeleteEventAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteListEventsAsync(DeleteListRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[EventsService][DeleteListEventsAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteRangeAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventsService][DeleteListEventsAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
