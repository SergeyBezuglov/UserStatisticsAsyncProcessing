using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Interfaces.Base
{
    /// <summary>
    /// Шаблон проектирования.
    /// </summary>
    public interface ISpecification<T>
    {
        /// <summary>
        /// <T, obj> сигнатура которых совпадает с IsSatisfiedBy
        /// </summary>
        public bool IsSatisfiedBy(T obj);
        /// <summary>
        /// Выражение.
        /// </summary>
        /// <value>Значение выражения.</value>
        Expression<Func<T, bool>> Expression { get;   }
    }
}
