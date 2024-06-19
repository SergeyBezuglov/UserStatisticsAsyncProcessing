using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Interfaces.Base
{
    /// <summary>
    /// Интерфейс сущности.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntity<TId>
        where TId:notnull
    {
        /// <summary>
        /// Получает идентификатор.
        /// </summary>
        /// <value>Значение TId.</value>
        public TId Id { get; }
    }
}
