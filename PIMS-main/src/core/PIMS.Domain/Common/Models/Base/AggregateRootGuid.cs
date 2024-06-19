using PIMS.Domain.Common.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models.Base
{
    /// <summary>
    /// Агрегатная корневая направляющая.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class AggregateRootGuid<TId> : AggregateRoot<TId, Guid>
         where TId : notnull, AggregateRootId<Guid>
       
    {
#pragma warning disable CS8618
        /// <summary>
        /// Защищенный объект, который загружает клиентский код из базового класса.
        /// </summary>
        protected AggregateRootGuid() : base() { }
        /// <summary>
        /// Защищенный объект, который загружает идентификатор.
        /// </summary>
        protected AggregateRootGuid(AggregateRootId<Guid> id)
        {
            Id = id;
        }
#pragma warning restore CS8618
    }
}
