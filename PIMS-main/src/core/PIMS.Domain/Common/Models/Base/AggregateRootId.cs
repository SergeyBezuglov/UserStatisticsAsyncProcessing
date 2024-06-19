using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models.Base
{
    /// <summary>
    /// Агрегатный корневой идентификатор.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class AggregateRootId<TId>:ValueObject
    {
        /// <summary>
        /// Свойство значение имеет отсутствующую или неполную реализацию.
        /// </summary>
        public abstract TId Value { get;protected set; }
        /// <summary>
        /// Защищенный конструктор агрегатного корневого идентификатора.
        /// </summary>
        protected AggregateRootId()
        {

        }
        /// <summary>
        /// Защищенный объект, который загружает значение.
        /// </summary>
        protected AggregateRootId(TId value)
        {
            Value = value;
        }
        /// <summary>
        /// Переопределяет метод GetEqualityComponents() и возвращает последовательность объектов, значениях которых не должно быть null.
        /// </summary>
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
        }
        /// <summary>
        /// Переопределяет метод GetHashCode() и возвращает значения хэш-кода.
        /// </summary>
        public override int GetHashCode()
        {
            return Value!.GetHashCode();
        }
    }
}
