using NotificationService.Models;
using NotificationService.DataAccess.Abstractions.EF.Repositories;
using NotificationService.Domain;
using Microsoft.Extensions.Logging;
using AutoMapper;
using NotificationService.Domain.Directories;
using NotificationService.Services.Exceptions;
using NotificationService.Domain.Abstractions.Repositories.ModelRequests;

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

        public async Task<MessengerResponse> GetMessengerAsync(Guid id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new NotFoundException($"Мессенджер с идентификатором {id} не найден.");
                }

                return _mapper.Map<MessengerResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessengersService][GetMessengerAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<MessengerResponse>> GetListMessengersAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                return _mapper.Map<List<MessengerResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessengersService][GetListMessengersAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<MessengerResponse> UpdateMessengerAsync(UpdateMessengerRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[MessengersService][UpdateMessengerAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var response = await _repository.GetByIdAsync(request.Id);
                if (response == null)
                {
                    throw new NotFoundException($"Мессенджер с идентификатором {request.Id} не найден.");
                }

                response.Name = request.Name;
                response.ModifiedBy = request.ModifiedBy;

                await _repository.UpdateAsync(response);

                return _mapper.Map<MessengerResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessengersService][UpdateMessengerAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task DeleteMessengerAsync(DeleteMessengerRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[MessengersService][DeleteMessengerAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var response = await _repository.GetByIdAsync(request.Id);
                if (response == null)
                {
                    throw new NotFoundException($"Мессенджер с идентификатором {request.Id} не найден.");
                }

                response.DeletedBy = request.DeletedBy;

                await _repository.DeleteAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessengersService][DeleteMessengerAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteListMessengersAsync(DeleteListRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[MessengersService][DeleteListMessengersAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteRangeAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessengersService][DeleteListMessengersAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
