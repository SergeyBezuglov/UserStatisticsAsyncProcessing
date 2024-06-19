using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Interfaces.Base
{
    /// <summary>
    /// Обычный интерфейс объекта значения.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOrdinaryValueObject<T>
    {
        /// <summary>
        /// Значение.
        /// </summary>
        /// <value>Значение T.</value>
        public T Value { get; }
    }
}
