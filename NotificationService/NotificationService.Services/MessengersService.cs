using NotificationService.Models;
using NotificationService.DataAccess.Abstractions.EF.Repositories;
using NotificationService.Domain;
using Microsoft.Extensions.Logging;
using AutoMapper;
using NotificationService.Domain.Directories;

namespace NotificationService.Services
{
    public class MessengersService
    {
        private readonly LaterDeletedEntityRepository<Messengers> _repository;
        private readonly ILogger<MessengersService> _logger;
        private readonly IMapper _mapper;

        public MessengersService(LaterDeletedEntityRepository<Messengers> messengersRepository
            , ILogger<MessengersService> logger
            , IMapper mapper)
        {
            _repository = messengersRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> AddMessengerAsync(MessengerRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[MessengersService][AddMessengerAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var messenger = _mapper.Map<Messengers>(request);
                await _repository.AddAsync(messenger);

                return messenger.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessengersService][AddMessengerAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
