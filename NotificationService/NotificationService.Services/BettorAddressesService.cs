using AutoMapper;
using Microsoft.Extensions.Logging;
using NotificationService.Domain.Directories;
using NotificationService.Models;
using NotificationService.Services.Exceptions;
using NotificationService.DataAccess.Repositories;
using System.Net;
using Azure.Core;
using Bets.Abstractions.Domain.Repositories.ModelRequests;

namespace NotificationService.Services
{
    public class BettorAddressesService
    {
        private readonly BettorAddressRepository _repository;
        private readonly ILogger<BettorAddressesService> _logger;
        private readonly IMapper _mapper;

        public BettorAddressesService(BettorAddressRepository bettorsRepository
            , ILogger<BettorAddressesService> logger
            , IMapper mapper
            )
        {
            _repository = bettorsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> AddBettorAddressesAsync(BettorAddressesRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorAddressesService][AddBettorAddressesAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var address = _mapper.Map<BettorAddresses>(request);
                await _repository.AddAsync(address);

                return address.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][AddBettorAddressesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BettorAddressResponse> GetBettorAddressesAsync(Guid id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new NotFoundException($"Адрес с идентификатором {id} не найден.");
                }

                var result = _mapper.Map<BettorAddressResponse>(response);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][GetBettorAddressesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<BettorAddressResponse>> GetListBettorAddressesAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                return _mapper.Map<List<BettorAddressResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][GetListBettorAddressesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<BettorAddressResponse>> GetListByBettorIdAsync(Guid bettorId)
        {
            try
            {
                var response = await _repository.GetListByBettorIdAsync(bettorId);

                return _mapper.Map<List<BettorAddressResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][GetListByBettorIdAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BettorAddressResponse> GetDefaultByBettorIdAsync(Guid bettorId)
        {
            try
            {
                var response = await _repository.GetByBettorIdWithMinPriorityAsync(bettorId);

                return _mapper.Map<BettorAddressResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][GetDefaultByBettorIdAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> UpdateAddressAsync(AddressUpdateRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorAddressesService][UpdateAdressAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var address = _mapper.Map<BettorAddresses>(request);

                return await _repository.UpdateAddressAsync(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][UpdateAdressAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BettorAddressResponse> UpdateBettorAddressesAsync(BettorAddressUpdateRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorAddressesService][UpdateBettorAddressesAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var response = await PrepareRecordForUpdating(request);
                await _repository.UpdateAsync(response);

                return _mapper.Map<BettorAddressResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][UpdateBettorAddressesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<BettorAddressResponse>> UpdateBettorAddressesAsync(IEnumerable<BettorAddressUpdateRequest> request)
        {
            if (request == null || !request.Any())
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorAddressesService][UpdateBettorAddressesAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var responses = new List<BettorAddresses>(request.Count());
                foreach (var req in request)
                {
                    var response = await PrepareRecordForUpdating(req);
                    responses.Add(response);
                }

                await _repository.UpdateAsync(responses);

                return _mapper.Map<List<BettorAddressResponse>>(responses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][UpdateBettorAddressesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BettorAddressResponse> SetDefaultByBettorIdAsync(BettorAddressesSetDefaultRequest request)
        {
            try
            {
                var record = await _repository.GetByIdAsync(request.Id);
                if (record == null)
                {
                    throw new NotFoundException($"[BettorAddressesService][SetDefaultByBettorIdAsync] Мессенджер с идентификатором {request.Id} не найден.");
                }

                var minPriority = await _repository.GetByBettorIdWithMinPriorityAsync(record.BettorId);

                if (minPriority == null)
                {
                    await _repository.AddAsync(record);
                }

                if (record.Id != minPriority.Id)
                {
                    record.Priority = minPriority.Priority - 1;
                    record.ModifiedBy = request.ModifiedBy;
                    var updatedCount = await _repository.UpdatePriorityAsync(record);
                    if (updatedCount != 1)
                    {
                        throw new Exception($"Priority updated for {updatedCount} records! And it should only be for one!");
                    }
                }

                return _mapper.Map<BettorAddressResponse>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][SetDefaultByBettorIdAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteBettorAddressAsync(DeleteRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorAddressesService][DeleteettorAddressAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][DeleteettorAddressAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteListBettorAddressesAsync(DeleteListRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettorAddressesService][DeleteListBettorAddressesAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteRangeAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][DeleteListBettorAddressesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        private async Task<BettorAddresses> PrepareRecordForUpdating(BettorAddressUpdateRequest request)
        {
            try
            {
                var record = await _repository.GetByIdAsync(request.Id);
                if (record == null)
                {
                    throw new NotFoundException($"[BettorAddressesService][UpdateBettorAddressesAsync] Мессенджер с идентификатором {request.Id} не найден.");
                }

                record.Priority = request.Priority;
                record.BettorId = request.BettorId;
                record.Address = request.Address;
                record.MessengerId = request.MessengerId;
                record.ModifiedBy = request.ModifiedBy;

                return record;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettorAddressesService][PrepareRecordForUpdating] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
