using Microsoft.EntityFrameworkCore;
using Bets.Abstractions.Domain.DTO;
using Bets.Abstractions.Domain.Repositories.Interfaces;

namespace Bets.Abstractions.DataAccess.EF.Repositories
{
    /// <summary>
    /// Добавляет реализации методов модификации данных в хранилище
    /// </summary>
    /// <typeparam name="T">Тип новой сущности</typeparam>
    public class ModifiableEntityRepository<T>
      : CreatedEntityRepository<T>
      , ICanUpdateEntitiesRepository<T>, ICanCreateEntitiesRepository<T>
      , IIdentifiableEntitiesRepository<T>, IRepository<T>
      where T : ModifiableEntity
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст хранилища</param>
        public ModifiableEntityRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Изменяет в хранилище информацию о сущности
        /// </summary>
        /// <param name="entity">Изменяемые данные</param>
        public virtual async Task UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _entitySet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Изменяет в хранилище информацию о нескольких сущностях
        /// </summary>
        /// <param name="entity">Изменяемые данные</param>
        public virtual async Task UpdateAsync(IEnumerable<T> entitys)
        {
            var modifiedDate = DateTime.Now;
            foreach (var entity in entitys)
            {
                entity.ModifiedDate = modifiedDate;
                _entitySet.Update(entity);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
