using PIMS.Domain.Common.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models
{
    /// <summary>
    /// Временное значение объекта.
    /// </summary>
    public class TemporalValueObject : OrdinaryValueObject, ITemporalValueObject<Guid>
    {
        /// <summary>
        ///  Свойство даты и время может быть установлено при создании экземпляра объекта и имеет ограниченный доступ к set.
        /// </summary>
        public DateTime SnapShotDate { get; protected set; }

        /// <summary>
        /// Защищенный объект, который загружает клиентский код из базового класса.
        /// </summary>
        protected TemporalValueObject():base() { }
        /// <summary>
        /// Защищенный объект, который который загружает значение даты и время.
        /// </summary>
        protected TemporalValueObject(Guid value, DateTime snapShotDate):base(value)
        { 
            SnapShotDate = snapShotDate;
        }
        /// <summary>
        /// Защищенный объект, который который загружает значение временного значения объекта.
        /// </summary>
        protected TemporalValueObject(TemporalValueObject temporalValueObject):base(temporalValueObject.Value)
        { 
            SnapShotDate = temporalValueObject.SnapShotDate;
        }
        /// <summary>
        /// Защищенный объект, который служит для создания нового экземпляра TemporalValueObject с уникальным идентификатором типа Guid и моментом времени,
        /// который соответствует текущей дате и времени на сервере.
        /// </summary>
        protected static new TemporalValueObject CreateUniqueBase() 
        { 
            return new TemporalValueObject(Guid.NewGuid(), DateTime.UtcNow);
        }

        /// <summary>
        /// В методе GetEqualityComponents переопределена логика определения компонентов, 
        /// используемых для сравнения объектов с целью установления равенства (equality) в классе, наследуется от другого класса.
        /// </summary>
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return SnapShotDate;
        }
    }
}
