using Bets.Abstractions.DataAccess.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Directories;

namespace NotificationService.DataAccess.Repositories
{
    public class BettorAddressRepository : LaterDeletedEntityRepository<BettorAddresses>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст хранилища</param>
        public BettorAddressRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public override async Task<BettorAddresses?> GetByIdAsync(Guid id)
        {
            return await _entitySet.Where(x => x.Id == id && x.DeletedDate == null)
                .Include("Bettor")
                .Include("Messenger")
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить все адреса
        /// </summary>
        public override async Task<List<BettorAddresses>> GetAllAsync()
        {
            return await _entitySet.Where(x => x.DeletedDate == null)
                .Include("Bettor")
                .Include("Messenger")
                .OrderBy(x => x.Bettor.Nickname)
                .ThenBy(i => i.Priority)
                .ToListAsync();
        }

        /// <summary>
        /// Получить все адреса конкретного игрока
        /// </summary>
        /// <param name="bettorId">Идентификатор игрока</param>
        public async Task<List<BettorAddresses>> GetListByBettorIdAsync(Guid bettorId)
        {
            return await _entitySet.Where(x => x.BettorId == bettorId && x.DeletedDate == null)
                .Include("Bettor")
                .Include("Messenger")
                .OrderBy(x => x.Priority)
                .ToListAsync();
        }

        /// <summary>
        /// Получение адреса конкретного игрока с минимальным приоритетом
        /// </summary>
        /// <param name="bettorId">Идентификатор игрока</param>
        public async Task<BettorAddresses?> GetByBettorIdWithMinPriorityAsync(Guid bettorId)
        {
            return await _entitySet.Where(x => x.BettorId == bettorId && x.DeletedDate == null)
                .Include("Bettor")
                .Include("Messenger")
                .OrderBy(x => x.Priority)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Обновляет непосредственно адрес
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>Количество обновленных</returns>
        public async Task<int> UpdateAddressAsync(BettorAddresses request)
        {
            var updatedCount = await _entitySet.Where(x => x.Id == request.Id && x.DeletedDate == null)
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.ModifiedDate, DateTime.Now)
                                        .SetProperty(p => p.ModifiedBy, request.ModifiedBy)
                                        .SetProperty(p => p.Address, request.Address));
            return updatedCount;
        }

        /// <summary>
        /// Обновляет непосредственно приоритет
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>Количество обновленных</returns>
        public async Task<int> UpdatePriorityAsync(BettorAddresses request)
        {
            var updatedCount = await _entitySet.Where(x => x.Id == request.Id && x.DeletedDate == null)
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.ModifiedDate, DateTime.Now)
                                        .SetProperty(p => p.ModifiedBy, request.ModifiedBy)
                                        .SetProperty(p => p.Priority, request.Priority));
            return updatedCount;
        }
    }
}
