using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models.Base
{
    /// <summary>
    /// Агрегатный корень целочисленного значения.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class AggregateRootInt<TId> : AggregateRoot<TId, int>
         where TId : notnull, AggregateRootId<int>
    {
#pragma warning disable CS8618
        /// <summary>
        /// Защищенный объект, который загружает клиентский код из базового класса.
        /// </summary>
        protected AggregateRootInt() : base() { }
        /// <summary>
        /// Защищенный объект, который загружает идентификатор.
        /// </summary>
        protected AggregateRootInt(AggregateRootId<int> id)
        {
            Id = id;
        }
#pragma warning restore CS8618
    }
}
