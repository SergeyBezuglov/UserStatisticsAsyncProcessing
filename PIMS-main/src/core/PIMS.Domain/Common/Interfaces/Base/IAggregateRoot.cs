using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Interfaces.Base
{
    /// <summary>
    /// Агрегатный корневой интерфейс.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IAggregateRoot<TId>:IEntity<TId>
        where TId : notnull
    { 
    }
}
