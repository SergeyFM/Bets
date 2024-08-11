using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Abstractions.DTO;
using NotificationService.Domain.Abstractions.Repositories.Interfaces;

namespace NotificationService.DataAccess.Abstractions.EF.Repositories
{
    /// <summary>
    /// Добавляет реализации методов отложенного удаления
    /// </summary>
    /// <typeparam name="T">Тип новой сущности</typeparam>
    public class LaterDeletedEntityRepository<T>
      : ModifiableEntityRepository<T>
      , ICanDeleteEntitiesRepository<T>
      , ICanUpdateEntitiesRepository<T>, ICanCreateEntitiesRepository<T>
      , IIdentifiableEntitiesRepository<T>, IRepository<T>
      where T : LaterDeletedEntity
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст хранилища</param>
        public LaterDeletedEntityRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Помечает сущность для удаления
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.DeletedDate = DateTime.Now;
                _entitySet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Помечает несколько сущностей для удаления
        /// </summary>
        /// <param name="id">Идентификаторы удаляемых объектов</param>
        public virtual async Task DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            var entities = await GetListByIdsAsync(ids);
            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    entity.DeletedDate = DateTime.Now;
                }
                _entitySet.UpdateRange(entities);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
