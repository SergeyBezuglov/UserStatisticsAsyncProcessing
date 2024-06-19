using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Authentication.Configuration
{
    /// <summary>
    /// Настройки Active Directory.
    /// </summary>
    public class ActiveDirectorySettings
    {
        /// <summary>
        /// Свойство хост можно установить значение только во время инициализации объекта.
        /// </summary>
        public string Host { get; init; } =string.Empty;
        /// <summary>
        /// Свойство домен можно установить значение только во время инициализации объекта.
        /// </summary>
        public string? Domain { get; init; }
        /// <summary>
        /// Свойство имя пользователя домена доступа можно установить значение только во время инициализации объекта.
        /// </summary>
        public string? AccessDomainUserName { get; init; }
        /// <summary>
        /// Свойство пароль пользователя домена доступа можно установить значение только во время инициализации объекта.
        /// </summary>
        public string? AccessDomainUserPassword { get; init; }
        /// <summary>
        /// Свойство разрешение вложенного домена доступа можно установить значение только во время инициализации объекта.
        /// </summary>
        public string? AllowNestedDomain { get; init; }
    }
}
