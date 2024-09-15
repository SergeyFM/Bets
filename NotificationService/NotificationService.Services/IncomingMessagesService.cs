using NotificationService.Models;
using Bets.Abstractions.DataAccess.EF.Repositories;
using NotificationService.Domain;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace NotificationService.Services
{
    public class IncomingMessagesService
    {
        private readonly CreatedEntityRepository<IncomingMessages> _repository;
        private readonly ILogger<IncomingMessagesService> _logger;
        private readonly IMapper _mapper;

        public IncomingMessagesService(CreatedEntityRepository<IncomingMessages> incomingMessagesRepository
            , ILogger<IncomingMessagesService> logger
            , IMapper mapper)
        {
            _repository = incomingMessagesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> AddMessageAsync(IncomingMessageRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a message without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[IncomingMessagesService][AddMessageAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var incomingMessage = _mapper.Map<IncomingMessages>(request);
                await _repository.AddAsync(incomingMessage);

                return incomingMessage.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[IncomingMessagesService][AddMessageAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<Guid>> AddRangeMessagesAsync(List<IncomingMessageRequest> request
            , CancellationToken ct)
        {
            if (request == null || !request.Any())
            {
                var msg = "attempt to transmit a message without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[IncomingMessagesService][AddRangeMessagesAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var incomingMessages = _mapper.Map<List<IncomingMessages>>(request);
                await _repository.AddRangeAsync(incomingMessages);

                return incomingMessages.Select(x => x.Id).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[IncomingMessagesService][AddRangeMessagesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<IncomingMessageResponse>> GetListMessagesAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                return _mapper.Map<List<IncomingMessageResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[IncomingMessagesService][GetListMessagesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
