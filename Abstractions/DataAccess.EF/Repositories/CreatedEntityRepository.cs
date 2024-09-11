using Microsoft.EntityFrameworkCore;
using Bets.Abstractions.Domain.DTO;
using Bets.Abstractions.Domain.Repositories.Interfaces;

namespace Bets.Abstractions.DataAccess.EF.Repositories
{
    /// <summary>
    /// Добавляет реализации методов добавления данных в хранилище
    /// </summary>
    /// <typeparam name="T">Тип новой сущности</typeparam>
    public class CreatedEntityRepository<T> 
      : ReadingRepository<T>
      , ICanCreateEntitiesRepository<T>, IIdentifiableEntitiesRepository<T>, IRepository<T>
      where T : CreatedEntity
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст хранилища</param>
        public CreatedEntityRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Добавляет новую сущность в хранилище
        /// </summary>
        /// <param name="entity">Новые данные</param>
        public virtual async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            await _entitySet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
