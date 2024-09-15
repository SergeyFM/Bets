using AutoMapper;
using Bets.Abstractions.DataAccess.EF.Repositories;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
using Microsoft.Extensions.Logging;
using NotificationService.Domain.Directories;
using NotificationService.Models;
using NotificationService.Services.Exceptions;

namespace NotificationService.Services
{
    public class MessageSourcesService
    {
        private readonly LaterDeletedEntityRepository<MessageSources> _repository;
        private readonly ILogger<MessageSourcesService> _logger;
        private readonly IMapper _mapper;

        public MessageSourcesService(LaterDeletedEntityRepository<MessageSources> repository
            , ILogger<MessageSourcesService> logger
            , IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> AddMessageSourceAsync(MessageSourcesRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[MessageSourcesService][AddMessageSourceAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var bettor = _mapper.Map<MessageSources>(request);
                await _repository.AddAsync(bettor);

                return bettor.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessageSourcesService][AddMessageSourceAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<MessageSourceResponse> GetMessageSourceAsync(Guid id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new NotFoundException($"Источник сообщений с идентификатором {id} не найден.");
                }

                return _mapper.Map<MessageSourceResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessageSourcesService][GetMessageSourceAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<MessageSourceResponse>> GetListMessageSourcesAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                return _mapper.Map<List<MessageSourceResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessageSourcesService][GetListMessageSourcesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<MessageSourceResponse> UpdateMessageSourceAsync(MessageSourceUpdateRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[MessageSourcesService][UpdateMessageSourceAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var response = await _repository.GetByIdAsync(request.Id);
                if (response == null)
                {
                    throw new NotFoundException($"Мессенджер с идентификатором {request.Id} не найден.");
                }

                response.Description = request.Description;
                response.ModifiedBy = request.ModifiedBy;

                await _repository.UpdateAsync(response);

                return _mapper.Map<MessageSourceResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessageSourcesService][UpdateMessageSourceAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteMessageSourceAsync(DeleteRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[MessageSourcesService][DeleteMessageSourceAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessageSourcesService][DeleteMessageSourceAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteListMessageSourcesAsync(DeleteListRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[MessageSourcesService][DeleteListMessageSourcesAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteRangeAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MessageSourcesService][DeleteListMessageSourcesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
