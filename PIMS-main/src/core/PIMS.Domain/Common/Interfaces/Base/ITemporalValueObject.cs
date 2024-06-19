using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Interfaces.Base
{
    /// <summary>
    /// Интерфейс объекта временных значений.
    /// </summary>
    public interface ITemporalValueObject<T>:IOrdinaryValueObject<T>
    {
        /// <summary>
        /// Дата снимка.
        /// </summary>
        /// <value>Значение даты и время.</value>
        public DateTime SnapShotDate { get; }
    }
}
