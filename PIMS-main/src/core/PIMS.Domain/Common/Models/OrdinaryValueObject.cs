using PIMS.Domain.Common.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models
{
    /// <summary>
    /// Обычное значение объекта.
    /// </summary>
    public class OrdinaryValueObject : ValueObject, IOrdinaryValueObject<Guid>
    {
        /// <summary>
        /// Свойство значение может быть установлено при создании экземпляра объекта и имеет ограниченный доступ к set.
        /// </summary>
        public Guid Value { get; protected set; }
        /// <summary>
        /// Защищенный объект, который загружает клиентский код из базового класса.
        /// </summary>
        protected OrdinaryValueObject() : base()
        {

        }
        /// <summary>
        /// Защищенный объект, который который загружает значение.
        protected OrdinaryValueObject(Guid value)
        {
            Value = value;
        }
        /// <summary>
        /// Защищенный объект, который который загружает значение обычного значения объекта.
        /// </summary>
        protected OrdinaryValueObject(OrdinaryValueObject ordinaryValueObject)
        {
            Value = ordinaryValueObject.Value;
        }
        /// <summary>
        /// Защищенный объект, который служит для создания нового экземпляра OrdinaryValueObject с уникальным идентификатором типа Guid
        /// </summary>
        protected static OrdinaryValueObject CreateUniqueBase() 
        {
            return (new OrdinaryValueObject(Guid.NewGuid()));
        }
        /// <summary>
        /// Переопределяет метод GetEqualityComponents() и возвращает последовательность объектов.
        /// Компоненты равенства.
        /// </summary>
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
