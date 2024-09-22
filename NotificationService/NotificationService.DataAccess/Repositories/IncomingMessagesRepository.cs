using Bets.Abstractions.DataAccess.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NotificationService.DataAccess.DTO;
using NotificationService.Domain;

namespace NotificationService.DataAccess.Repositories
{
    public class IncomingMessagesRepository : CreatedEntityRepository<IncomingMessages>
    {
        ///// <summary>
        ///// Конструктор
        ///// </summary>
        ///// <param name="context">Контекст хранилища</param>
        //public IncomingMessagesRepository(DbContext context) : base(context) { }


        public IncomingMessagesRepository(IDbContextFactory<DatabaseContext> contextFactory) : base(contextFactory.CreateDbContext()) { }

        /// <summary>
        /// Выбрать следующую пачку сообщений
        /// </summary>
        /// <param name="count">Кол-во сообщений в пачке</param>
        /// <returns>List of IncomingMessages</returns>
        public async Task<List<IncomingMessages>> GetNextMessagesAsync(int count)
        {
            return await _entitySet.Where(x => x.State == SendingStates.ReadyToSent 
                                            && x.ActualDate > DateTime.UtcNow)
                .OrderBy(x => x.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        /// <summary>
        /// Обновляет непосредственно статус для нескольких сообщений
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>Количество обновленных</returns>
        public async Task<int> UpdateStatesAsync(UpdateMessageStatesRequest request)
        {
            var updatedCount = await _entitySet.Where(x => request.Ids.Contains(x.Id))
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.State, request.State));
            return updatedCount;
        }
    }
}
