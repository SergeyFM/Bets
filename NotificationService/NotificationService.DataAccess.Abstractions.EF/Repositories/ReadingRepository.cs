using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Abstractions.DTO;
using NotificationService.Domain.Abstractions.Repositories.Interfaces;

namespace NotificationService.DataAccess.Abstractions.EF.Repositories
{
    /// <summary>
    /// Предоставляет реализации методов чтения данных из хранилища
    /// </summary>
    /// <typeparam name="T">Тип сущности с идентификаторами Guid</typeparam>
    public class ReadingRepository<T>
      : IIdentifiableEntitiesRepository<T>, IRepository<T>
      where T : IdentifiableEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _entitySet;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст хранилища</param>
        public ReadingRepository(DbContext context)
        {
            _dbContext = context;
            _entitySet = _dbContext.Set<T>();
        }

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _entitySet.FindAsync(id);
        }

        /// <summary>
        /// Получить все сущности типа T
        /// </summary>
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _entitySet.ToListAsync();
        }

        /// <summary>
        /// Получение списка сущностей по идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы сущностей</param>
        public virtual async Task<List<T>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _entitySet.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
