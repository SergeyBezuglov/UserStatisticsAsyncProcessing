using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PIMS.Domain.Common.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Interceptors
{
    /// <summary>
    /// Перехватчик событий домена публикации.
    /// </summary>
    public class PublishDomainEventsInterceptor:SaveChangesInterceptor
    {
        /// <summary>
        /// Посредник.
        /// </summary>
        private readonly IPublisher _mediator;
        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="PublishDomainEventsInterceptor"/> .
        /// </summary>
        /// <param name="mediator">Посредник.</param>
        public PublishDomainEventsInterceptor(IPublisher mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Сохранение изменений.
        /// </summary>
        /// <param name="eventData">Данные о событии.</param>
        /// <param name="result">Результат.</param>
        /// <returns>Возвращение значения результата перехвата (InterceptionResult).</returns>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        /// <summary>
        /// Сохраняет изменения асинхронно.
        /// </summary>
        /// <param name="eventData">Данные о событии.</param>
        /// <param name="result">Результат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Возвращение значения результата перехвата (InterceptionResult).</returns>
        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await PublishDomainEvents(eventData.Context, cancellationToken);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Публикует события домена.
        /// </summary>
        /// <param name="dbContext">Контекст базы данных.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Возвращение задачи (Task).</returns>
        private async Task PublishDomainEvents(DbContext? dbContext, CancellationToken cancellationToken = default)
        {
            if (dbContext is null)
            {
                return;
            }
            var entitiesWithDomainEvents= dbContext.ChangeTracker.Entries<IHasDomainEvents>().
                Where(entry=>entry.Entity.DomainEvents.Any()).
                Select(entry=>entry.Entity).ToList();

            var domainEvents = entitiesWithDomainEvents.SelectMany(entity => entity.DomainEvents).ToList();

            entitiesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());

            foreach(var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
