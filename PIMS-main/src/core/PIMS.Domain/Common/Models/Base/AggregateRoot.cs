using PIMS.Domain.Common.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models.Base
{
    /// <summary>
    /// Совокупный корень.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TIdType"></typeparam>
    public abstract class AggregateRoot<TId,TIdType> : Entity<TId>, IAggregateRoot<TId>
        where TId : notnull,AggregateRootId<TIdType>
       
    {
        /// <summary>
        /// Свойство идентификатор имеет ограниченный доступ к set.
        /// </summary>
        public new AggregateRootId<TIdType> Id { get; protected set; }
        /// <summary>
        /// Получение идентификатора.
        /// </summary>
        public TIdType GetId => Id.Value;

#pragma warning disable CS8618
        /// <summary>
        /// Защищенный объект, который загружает клиентский код из базового класса.
        /// </summary>
        protected AggregateRoot():base() { }
        /// <summary>
        /// Защищенный объект, который загружает идентификатор.
        /// </summary>
        protected AggregateRoot(TId id)  { 
         Id=id;
        }
        #pragma warning restore CS8618
    }
}
