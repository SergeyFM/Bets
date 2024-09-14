using NotificationService.Models;
using Bets.Abstractions.DataAccess.EF.Repositories;
using Microsoft.Extensions.Logging;
using AutoMapper;
using NotificationService.Domain.Directories;
using NotificationService.Services.Exceptions;
using Bets.Abstractions.Domain.Repositories.ModelRequests;

namespace NotificationService.Services
{
    public class BettorsService
    {
        private readonly LaterDeletedEntityRepository<Bettors> _repository;
        private readonly ILogger<BettorsService> _logger;
        private readonly IMapper _mapper;

        public BettorsService(LaterDeletedEntityRepository<Bettors> bettorsRepository
            , ILogger<BettorsService> logger
            , IMapper mapper)
        {
            _repository = bettorsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> AddBettorAsync(BettorRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorsService][AddBettorAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var bettor = _mapper.Map<Bettors>(request);
                await _repository.AddAsync(bettor);

                return bettor.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorsService][AddBettorAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BettorResponse> GetBettorAsync(Guid id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new NotFoundException($"Игрок с идентификатором {id} не найден.");
                }

                return _mapper.Map<BettorResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorsService][GetBettorAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<BettorResponse>> GetListBettorsAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                return _mapper.Map<List<BettorResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorsService][GetBettorAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BettorResponse> UpdateBettorAsync(BettorUpdateRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorsService][UpdateBettorAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var response = await _repository.GetByIdAsync(request.Id);
                if (response == null)
                {
                    throw new NotFoundException($"[BettorsService][UpdateBettorAsync] Мессенджер с идентификатором {request.Id} не найден.");
                }

                response.Nickname = request.Nickname;
                response.ModifiedBy = request.ModifiedBy;

                await _repository.UpdateAsync(response);

                return _mapper.Map<BettorResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorsService][UpdateBettorAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteBettorAsync(DeleteRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorsService][DeleteBettorAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorsService][DeleteBettorAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteListBettorsAsync(DeleteListRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorsService][DeleteListBettorsAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteRangeAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorsService][DeleteListBettorsAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
