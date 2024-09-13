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
    }
}
