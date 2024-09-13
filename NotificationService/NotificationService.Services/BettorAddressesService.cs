using AutoMapper;
//using Bets.Abstractions.DataAccess.EF.Repositories;
using Microsoft.Extensions.Logging;
using NotificationService.Domain.Directories;
using NotificationService.Models;
using NotificationService.Services.Exceptions;
using NotificationService.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                _logger.LogError(ex, $"[BettorsService][GetBettorAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
