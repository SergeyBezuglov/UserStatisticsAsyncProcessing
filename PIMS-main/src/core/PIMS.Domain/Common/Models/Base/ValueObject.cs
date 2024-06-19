using PIMS.Domain.Common.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models
{
    /// <summary>
    /// Значение объекта.
    /// </summary>
    public abstract class ValueObject:IEquatable<ValueObject>, IValueObject
    {
        /// <summary>
        /// Метод GetEqualityComponents, который возвращает перечисление (IEnumerable) объектов.
        /// </summary>
        public abstract IEnumerable<object> GetEqualityComponents();
        /// <summary>
        /// Защищенный конструктор значения объекта.
        /// </summary>
        protected ValueObject()
        {

        }
        /// <summary>
        /// Используется для сравнения объектов на равенство компонентов ValueObject и возвращаемых методом GetEqualityComponents().
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType()) return false;
            var valueObj = obj as ValueObject;
            return GetEqualityComponents().
                SequenceEqual(valueObj!.GetEqualityComponents());
        }
        public static bool operator ==(ValueObject left, ValueObject right) {  return Equals(left,right); }
        public static bool operator !=(ValueObject left, ValueObject right) { return !Equals(left, right); }
        /// <summary>
        /// Генерирует хеш-код объекта на основе хеш-кодов его компонентов, используя операцию XOR.
        /// </summary>
        public override int GetHashCode()
        {
            return GetEqualityComponents().Select(m=>m?.GetHashCode() ?? 0).
                Aggregate((x,y)=>x ^ y);
        }

        /// <summary>
        /// Метод Equals определён для сравнения объектов типа ValueObject.
        /// </summary>
        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }
    }
}
