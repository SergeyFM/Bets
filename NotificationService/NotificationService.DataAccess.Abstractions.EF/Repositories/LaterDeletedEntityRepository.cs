using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Abstractions.DTO;
using NotificationService.Domain.Abstractions.Repositories.Interfaces;
using NotificationService.Domain.Abstractions.Repositories.ModelRequests;

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
        /// <param name="entity">Удаляемая сущность</param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            _entitySet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Помечает несколько сущностей для удаления
        /// </summary>
        /// <param name="id">Идентификаторы удаляемых объектов и кем удаляются</param>
        public async Task<int> DeleteRangeAsync(DeleteListRequest request)
        {
            var deletedCount = await _entitySet.Where(x => request.Ids.Contains(x.Id) && x.DeletedDate == null)
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.DeletedDate, DateTime.Now).SetProperty(p => p.DeletedBy, request.DeletedBy));
            return deletedCount;   
        }

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public override async Task<T?> GetByIdAsync(Guid id)
        {
            return await _entitySet.Where(x => x.Id == id && x.DeletedDate == null).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить все сущности типа T
        /// </summary>
        public override async Task<List<T>> GetAllAsync()
        {
            return await _entitySet.Where(x => x.DeletedDate == null).ToListAsync();
        }

        /// <summary>
        /// Получение списка сущностей по идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы сущностей</param>
        public override async Task<List<T>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _entitySet.Where(x => ids.Contains(x.Id) && x.DeletedDate == null).ToListAsync();
        }
    }
}
