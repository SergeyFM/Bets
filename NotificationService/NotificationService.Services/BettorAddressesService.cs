using AutoMapper;
using Bets.Abstractions.DataAccess.EF.Repositories;
using Microsoft.Extensions.Logging;
using NotificationService.Domain.Directories;
using NotificationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Services
{
    public class BettorAddressesService
    {
        private readonly LaterDeletedEntityRepository<BettorAddresses> _repository;
        private readonly ILogger<BettorAddressesService> _logger;
        private readonly IMapper _mapper;

        public BettorAddressesService(LaterDeletedEntityRepository<BettorAddresses> bettorsRepository
            , ILogger<BettorAddressesService> logger
            , IMapper mapper)
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
    }
}
