using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PIMS.Domain.Common.Authentication.Configuration.Enums;

namespace PIMS.Domain.Common.Authentication.Configuration
{
    /// <summary>
    /// Опции модуля аутентификации.
    /// </summary>
    public class AuthenticationModuleOption 
    {
        /// <summary>
        /// Свойство Type типа AuthenticationProviders должно быть передано или установлено при инициализации в значение AuthenticationProviders.NotSet
        /// </summary>
        /// <value>Значение поставщик аутентификации (AuthenticationProviders).</value>
        public AuthenticationProviders Type { get; set; } = AuthenticationProviders.NotSet;

        /// <summary>
        /// Свойство имя должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
