using PIMS.Domain.Common.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models
{
    /// <summary>
    /// Сущность.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class Entity<TId>:IEquatable<TId>, IEntity<TId>, IHasDomainEvents
        where TId : notnull 
    {
        /// <summary>
        /// Список событий домена только для просмотра.
        /// </summary>
        private readonly List<IDomainEvent> _domainEvents = new();
        /// <summary>
        /// События домена.
        /// </summary>
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        /// <summary>
        /// Свойство идентификатор для получение.
        /// </summary>
        public TId Id { get;  }
#pragma warning disable CS8618
        /// <summary>
        /// Защищенный конструктор сущности.
        /// </summary>
        protected Entity()
        {
           
        }
        /// <summary>
        /// Защищенный объект, который загружает идентификатор.
        /// </summary>
        protected Entity(TId id)
        {
            Id = id;
        }
#pragma warning restore CS8618
        /// <summary>
        /// Позволяет сравнивать объекты этого класса на основе их Id, возвращая true, если Id считаются равными, и false, если нет.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is null && Id is null)
            {
                return true;
            }
            if (obj is not null && Id is null)
            {
                return false;
            }
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        /// <summary>
        /// Метод выполняет сравнение текущего объекта с объектом, переданным в параметре other, используя метод Equals(object?).
        /// </summary>
        public bool Equals(TId? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }
        /// <summary>
        /// Используется для вычисления хеш-кода объекта,
        /// который позже может быть использован в хеш-таблицах и других коллекциях для быстрого поиска и сравнения объектов.
        /// </summary>
        public override int GetHashCode()
        {
            if (Id is null)
            {
                return 0;
            }
            return Id.GetHashCode();
        }

        /// <summary>
        /// Метод AddDomainEvent позволяет добавить событие в коллекцию _domainEvents, чтобы отслеживать и управлять доменными событиями в системе.
        /// </summary>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        /// <summary>
        /// Очищает события домена.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
