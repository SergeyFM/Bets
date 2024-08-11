﻿using NotificationService.Domain.Abstractions.DTO;

namespace NotificationService.Domain.Abstractions.Repositories.Interfaces
{
    /// <summary>
    /// Содержит методы, которые реализуют репозитории с возможностью удаления сущностей по идентификатору
    /// </summary>
    /// <typeparam name="T">Тип сущности с идентификаторами Guid</typeparam>
    public interface ICanDeleteEntitiesRepository<T> : IIdentifiableEntitiesRepository<T>, IRepository<T> 
        where T : IdentifiableEntity
    {
        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        /// <remarks>
        /// Возможна реализация с отложенным удалением
        /// </remarks>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Удаляет несколько объект
        /// </summary>
        /// <param name="id">Идентификаторы удаляемых объектов</param>
        /// <remarks>
        /// Возможна реализация с отложенным удалением
        /// </remarks>
        Task DeleteRangeAsync(IEnumerable<Guid> ids);
    }
}
