﻿using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Abstractions.DTO;
using NotificationService.Domain.Abstractions.Repositories.Interfaces;

namespace NotificationService.DataAccess.Abstractions.EF.Repositories
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
    }
}
