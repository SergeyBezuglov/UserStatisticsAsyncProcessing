using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Authentication.Configuration
{
    /// <summary>
    /// Опции модуля аутентификации jwt.
    /// </summary>
    public class JwtAuthenticationModuleOption: AuthenticationModuleOption
    {
        /// <summary>
        /// Название раздела.
        /// </summary>
        public const string SectionName = $"{AuthenticationModuleSettings.SectionName}:JWT";
        /// <summary>
        /// Название схемы.
        /// </summary>
        public const string SchemeName = $"JwtAuthentication";
        /// <summary>
        /// Название политики.
        /// </summary>
        public const string PolicyName = $"{SchemeName}Policy";
        /// <summary>
        /// Свойство настройки должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public JwtSettings Settings { get; set; }= new JwtSettings();
    }
}
