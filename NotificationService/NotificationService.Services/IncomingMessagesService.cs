using NotificationService.Models;
using NotificationService.DataAccess.Abstractions.EF.Repositories;
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
    }
}
