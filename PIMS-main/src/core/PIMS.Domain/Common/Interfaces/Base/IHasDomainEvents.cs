using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Interfaces.Base
{
    /// <summary>
    /// Имеет интерфейс событий домена.
    /// </summary>
    public interface IHasDomainEvents
    {
        /// <summary>
        /// События домена.
        /// </summary>
        /// <value>Список доменных событий.</value>
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
        /// <summary>
        /// Очистить события домена.
        /// </summary>
        public void ClearDomainEvents();

    }
}
