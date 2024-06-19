using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Authentication.Configuration
{
    /// <summary>
    /// Опция модуля аутентификации Active Directory.
    /// </summary>
    public class ActiveDirectoryAuthenticationModuleOption: AuthenticationModuleOption
    {
        /// <summary>
        /// Название раздела.
        /// </summary>
        public const string SectionName = $"{AuthenticationModuleSettings.SectionName}:AD";
        /// <summary>
        /// Определяет имя схемы аутентификации для использования указания типа аутентификации при работе с пользовательскими учетными записями в Active Directory.
        /// </summary>
        public const string SchemeName = "ActiveDirectoryAuthentication";
        /// <summary>
        /// Имя политики.
        /// </summary>
        public const string PolicyName = $"{SchemeName}Policy";
        /// <summary>
        /// Свойство Настройки типа ActiveDirectorySettings.
        /// </summary>
        /// <value>Значение по умолчанию, которое является новым экземпляром класса ActiveDirectorySetting</value>
        public ActiveDirectorySettings Settings { get; set; } = new ActiveDirectorySettings();
    }
}
